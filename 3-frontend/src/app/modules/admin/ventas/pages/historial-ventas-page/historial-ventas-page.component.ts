import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { forkJoin, Subject } from 'rxjs';
import { DictionaryErrors, DictionaryWarning } from 'app/core/resource/dictionaryError.constants';
import { ArchivoExcel, DictionaryInfo, ErrorCodigo, Flags, ImagenesUrl, Numeracion } from 'app/core/resource/dictionary.constants';
import { SecurityService } from 'app/core/auth/auth.service';
import { ToolService } from 'app/core/services/tool/tool.service';
import { MatPaginator, MatPaginatorIntl } from '@angular/material/paginator';
import { UntypedFormBuilder, UntypedFormGroup } from '@angular/forms';
import { MatDrawer } from '@angular/material/sidenav';
import { DecodedToken } from 'app/core/models/auth/response/decode-token-dto.model';
import * as XLSX from 'xlsx-js-style';
import { SpanishHistorialVentasPaginatorService } from 'app/core/services/paginator/ventas/historial/spanish-historial-ventas-paginator.service';
import { VentaDTO } from 'app/core/models/venta/response/venta-dto.model';
import { ObtenerVentaRequest } from 'app/core/models/venta/request/obtener-venta-request.model';
import { MatSelect } from '@angular/material/select';
import { MonedaDTO } from 'app/core/models/parametro/moneda-dto.model';
import { UsuarioDTO } from 'app/core/models/usuario/response/usuario-dto.model';
import { FuseConfirmationService } from '@fuse/services/confirmation';
import { FuseValidators } from '@fuse/validators';
import { AnularVentaRequest } from 'app/core/models/venta/request/anular-venta-request.model';
import { ResponseDTO } from 'app/core/models/generic/response-dto.model';
import { ObtenerDetalleVentaRequest } from 'app/core/models/venta/request/obtener-detalle-venta-request.model';
import { DetalleVentaDTO } from 'app/core/models/venta/response/detalle-venta-dto.model';
import { MatDialog } from '@angular/material/dialog';
import { DetalleVentaPageComponent } from './modals/detalle-producto-page/detalle-venta-page.component';
import { VentaService } from 'app/core/services/venta/venta.service';
import { DetalleVentaService } from 'app/core/services/detalleventa/detalleventa.service';

@Component({
    selector: 'app-historial-ventas-page',
    templateUrl: './historial-ventas-page.component.html',
    styleUrl: './historial-ventas-page.component.scss',
    providers: [
        {
            provide: MatPaginatorIntl,
            useClass: SpanishHistorialVentasPaginatorService,
        }
    ],
})

export class HistorialVentasPageComponent implements OnInit, AfterViewInit, OnDestroy {

    @ViewChild('selectUsuarioItem') selectUsuarioItem: MatSelect;

    private decodeToken: DecodedToken = this.obtenerInfouserInfoLogueado();
    public monedaInfo: MonedaDTO = this.obtenerInfoMoneda();
    private configForm: UntypedFormGroup;

    public isExportingReport: boolean = Flags.False;

    public disabledAcciones: boolean = Flags.False;
    public disabledBuscar: boolean = Flags.False;
    public disabledExportar: boolean = Flags.False;

    public skeleton: boolean = Flags.False;
    public skeletonNumber: number = Numeracion.Ocho;

    public textoResultadoTable: string = "";

    public imgNoDataUltimosRegistros: string = ImagenesUrl.noDataUltimosRegistros;

    @ViewChild(MatSort) set matSort(sort: MatSort) {
        this.pageSlice.sort = sort;
    }

    @ViewChild('matDrawer')
    private matDrawer: MatDrawer;

    filtroVentaForm: UntypedFormGroup;

    public minDate: Date = this._toolService.getMinDateFIlter();
    public maxDate: Date = this._toolService.getMaxDateFIlter();

    @ViewChild(MatPaginator) private _paginator: MatPaginator;

    public pageSlice: MatTableDataSource<VentaDTO> = new MatTableDataSource();
    public allUsuarioSource: UsuarioDTO[];
    public allHistorialVentaDataSource: MatTableDataSource<VentaDTO> = new MatTableDataSource();
    public historialVentaTableColumns: string[] = ['numeroVenta', 'nombreCliente', 'correoUsuario', 'fechaVenta', 'totalVenta', 'estado', 'acciones'];
    private _unsubscribeAll: Subject<any> = new Subject<any>();

    constructor(
        private _matDialog: MatDialog,
        private _fuseConfirmationService: FuseConfirmationService,
        private _formBuilder: UntypedFormBuilder,
        private _securityService: SecurityService,
        private _ventaService: VentaService,
        private _detalleventaService: DetalleVentaService,
        private _toolService: ToolService) {
    }

    ngAfterViewInit() {
        this.pageSlice.sortingDataAccessor = this.sortingActivoData;
        this.pageSlice.paginator = this._paginator;
    }

    ngOnDestroy() {
        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
    }

    ngOnInit() {
        this.formFiltros();
        this.showSkeleton();
        this.getFilterComboConsulta();
    }

    formFiltros() {

        this.decodeToken = this.obtenerInfouserInfoLogueado();
        this.filtroVentaForm = this._formBuilder.group({
            usuarios: [''],
            numeroVenta: [''],
            fechaVentaInicio: [this._toolService.getStartDateOfYear()],
            fechaVentaFin: [this._toolService.getEndDateOfYear()],
        });
    }

    getFilterComboConsulta() {

        const ventaRequest = this.obtenerRequest();

        forkJoin({
            dataUsuarios: this._ventaService.GetAllUsuariosAsync(),
            dataVentas: this._ventaService.GetAllByFilterAsync(ventaRequest),

        }).subscribe({
            next: (response) => {
                this.allUsuarioSource = response.dataUsuarios;
                this.allHistorialVentaDataSource.data = response.dataVentas;
                this.pageSlice.data = [];

                this.filtroVentaForm.get('usuarios')?.setValue(this.allUsuarioSource);

                if (this.allHistorialVentaDataSource.data.length > Numeracion.Cero) {
                    this.disabledExportar = Flags.False;
                    this.setPageSlice(this.allHistorialVentaDataSource.data);
                    this.disabledBuscar = Flags.False;
                    this.hideSkeleton();
                    return;
                }
                this.textoResultadoTable = DictionaryInfo.NoDataResult;
                this.disabledBuscar = Flags.False;
                this.disabledExportar = Flags.True;
                this.hideSkeleton();
            },
            error: (err) => {
                this.hideSkeleton();
                this._toolService.showError(DictionaryErrors.Transaction, DictionaryErrors.Tittle);
                this.disabledBuscar = Flags.False;
                this.disabledExportar = Flags.True;
                console.log(err);
            },
        });
    }

    GetAllHistorialVentasByFilterAsync(disabledBuscar: boolean, hideFilter: boolean) {

        const request = this.obtenerRequest();
        this.disabledBuscar = disabledBuscar;

        this._ventaService.GetAllByFilterAsync(request).subscribe((response: VentaDTO[]) => {
            this.allHistorialVentaDataSource.data = response;
            this.pageSlice.data = [];
            if (this.allHistorialVentaDataSource.data.length > Numeracion.Cero) {
                this.disabledExportar = Flags.False;
                this.setPageSlice(this.allHistorialVentaDataSource.data);
                this.disabledBuscar = Flags.False;
                this.hideSkeleton();
                if (hideFilter) {
                    this.closedDrawer();
                }
                return;
            }
            this.textoResultadoTable = DictionaryInfo.NoDataResult;
            this.disabledBuscar = Flags.False;
            this.disabledExportar = Flags.True;
            this.hideSkeleton();
            if (hideFilter) {
                this.closedDrawer();
            }
        }, err => {
            this.hideSkeleton();
            this._toolService.showError(DictionaryErrors.Transaction, DictionaryErrors.Tittle);
            this.disabledBuscar = Flags.False;
            this.disabledExportar = Flags.True;
            console.log(err);
            if (hideFilter) {
                this.closedDrawer();
            }
        });
    }

    showFormDetalleVentaDialog(venta: VentaDTO) {

        const request = new ObtenerDetalleVentaRequest()
        const destinationTimeZoneId = this._toolService.getTimeZone()
        const idUsuario = this.decodeToken.idUsuario;
        const idVenta = venta.idVenta;

        if (FuseValidators.isEmptyInputValue(idVenta)) {
            this._toolService.showWarning(DictionaryWarning.InvalidVenta, DictionaryWarning.Tittle);
            return;
        }

        if (FuseValidators.isEmptyInputValue(destinationTimeZoneId)) {
            this._toolService.showWarning(DictionaryWarning.InvalidLocalizacion, DictionaryWarning.Tittle);
            return;
        }

        if (FuseValidators.isEmptyInputValue(idUsuario)) {
            this._toolService.showWarning(DictionaryWarning.InvalidUIdUsuario, DictionaryWarning.Tittle);
            return;
        }

        this.disabledAcciones = Flags.True;

        request.destinationTimeZoneId = destinationTimeZoneId;
        request.idUsuario = idUsuario;
        request.idVenta = idVenta;

        this._detalleventaService.GetDetalleAsync(request).subscribe((response: DetalleVentaDTO[]) => {
            this.disabledAcciones = Flags.False;

            this._matDialog.open(DetalleVentaPageComponent, {
                autoFocus: Flags.False,
                data: {
                    lstDetalleVenta: response,
                    monedaInfo: this.monedaInfo,
                    numeroVenta: venta.numeroVenta
                }
            });

        }, err => {
            this.hideSkeleton();
            this._toolService.showError(DictionaryErrors.Transaction, DictionaryErrors.Tittle);
            this.disabledBuscar = Flags.False;

            this.disabledAcciones = Flags.False;
        });

    }

    AnulaAsync(venta: VentaDTO) {
        this.configForm = this._formBuilder.group({
            title: 'Anular Venta',
            message: '¿Seguro que quieres anular la venta ' + venta.numeroVenta + '?.',
            icon: this._formBuilder.group({
                show: true,
                name: 'heroicons_outline:exclamation-triangle',
                color: 'warn',
            }),
            actions: this._formBuilder.group({
                confirm: this._formBuilder.group({
                    show: true,
                    label: 'Borrar',
                    color: 'warn',
                }),
                cancel: this._formBuilder.group({
                    show: true,
                    label: 'Cancelar',
                }),
            }),
            dismissible: true,
        });

        const dialogRef = this._fuseConfirmationService.open(this.configForm.value);

        dialogRef.afterClosed().subscribe((result) => {

            if (result !== "confirmed") return;

            const secondConfigForm = this._formBuilder.group({
                title: 'Recuperar Stock',
                message: '¿Deseas recuperar el stock de los productos de esta venta?',
                icon: this._formBuilder.group({
                    show: true,
                    name: 'heroicons_outline:information-circle',
                    color: 'primary',
                }),
                actions: this._formBuilder.group({
                    confirm: this._formBuilder.group({
                        show: true,
                        label: 'Sí, recuperar',
                        color: 'primary',
                    }),
                    cancel: this._formBuilder.group({
                        show: true,
                        label: 'No',
                    }),
                }),
                dismissible: true,
            });

            const secondDialogRef = this._fuseConfirmationService.open(secondConfigForm.value);

            secondDialogRef.afterClosed().subscribe((secondResult) => {

                const recuperarStock = (secondResult === 'confirmed');

                const destinationTimeZoneId = this._toolService.getTimeZone();
                const idUsuario = this.decodeToken.idUsuario;
                const idVenta = venta.idVenta;

                if (FuseValidators.isEmptyInputValue(idVenta) ||
                    FuseValidators.isEmptyInputValue(destinationTimeZoneId) ||
                    FuseValidators.isEmptyInputValue(idUsuario)) {
                    this._toolService.showWarning(DictionaryWarning.InvalidVenta, DictionaryWarning.Tittle);
                    return;
                }

                const request = new AnularVentaRequest();
                request.destinationTimeZoneIdActualizacion = destinationTimeZoneId;
                request.id = idVenta;
                request.idUsuario = idUsuario;
                request.recuperarStock = recuperarStock;

                this.disabledAcciones = Flags.True;

                this._ventaService.AnulaAsync(request).subscribe((response: ResponseDTO) => {
                    if (response.code == ErrorCodigo.Advertencia) {
                        this._toolService.showWarning(response.message, response.titleMessage);
                        this.disabledAcciones = Flags.False;
                        this.hideSkeleton();
                        return;
                    }

                    if (response.success) {
                        this._toolService.showSuccess(response.message, response.titleMessage);
                        this.setPageSlice(this.allHistorialVentaDataSource.data);
                        this.disabledAcciones = Flags.False;
                        this.getSkeletonCount();
                        this.showSkeleton();
                        this.GetAllHistorialVentasByFilterAsync(Flags.True, Flags.True);
                        return;
                    }
                    this._toolService.showError(DictionaryErrors.Transaction, DictionaryErrors.Tittle);
                    this.disabledAcciones = Flags.False;
                }, err => {
                    this.hideSkeleton();
                    this.disabledAcciones = Flags.False;
                    this._toolService.showError(DictionaryErrors.Transaction, DictionaryErrors.Tittle);
                    console.error(err);
                });
            });
        });
    }

    btnBuscar() {
        this.getSkeletonCount();
        this.showSkeleton();
        this.GetAllHistorialVentasByFilterAsync(Flags.True, Flags.True);
    }

    onExportar() {
        if (this.filtroVentaForm.invalid) { return; }
        const request = new ObtenerDetalleVentaRequest()

        const destinationTimeZoneId = this._toolService.getTimeZone();


        if (FuseValidators.isEmptyInputValue(destinationTimeZoneId)) {
            this._toolService.showWarning(DictionaryWarning.InvalidLocalizacion, DictionaryWarning.Tittle);
            return;
        }

        request.destinationTimeZoneId = destinationTimeZoneId;
        request.idUsuario = this.decodeToken.idUsuario;
        request.lstUsuario = [];
        request.numeroVenta = this.filtroVentaForm.get('numeroVenta').value == "" ? null : this.filtroVentaForm.get('numeroVenta').value;
        request.fechaVentaInicio = this.filtroVentaForm.get('fechaVentaInicio').value == "" ? null : this.filtroVentaForm.get('fechaVentaInicio').value;
        request.fechaVentaFin = this.filtroVentaForm.get('fechaVentaFin').value == "" ? null : this.filtroVentaForm.get('fechaVentaFin').value;
        this.isExportingReport = Flags.True
        this._detalleventaService.GetDetalleAsync(request).subscribe((response: DetalleVentaDTO[]) => {
            this.isExportingReport = Flags.False
            this.exportar(response);

        }, err => {
            this.isExportingReport = Flags.False
            this.hideSkeleton();
            this._toolService.showError(DictionaryErrors.Transaction, DictionaryErrors.Tittle);
        });
 
    }

    exportar(detalleVenta: DetalleVentaDTO[]) {
         const fileName = ArchivoExcel.NombreExcelExportarVentas;
        const nameSheet = ArchivoExcel.NombreHojaExcelVentas;

        const reportData: any[] = detalleVenta.map(element => ({
            "ID Producto": element.idProducto,
            "ID Venta": element.idVenta,
            "Número Venta": element.numeroVenta,
            "ID Cliente": element.idCliente,
            "Nombre Cliente": element.nombreCliente,
            "Dirección": element.direccion,
            "URL Boleta/Factura": element.urlBoletaFactura,
            "Fecha Venta": this._toolService.formatoFechaHora(element.fechaVenta),
            // "URL Foto Producto": element.urlFotoProducto,  // <-- eliminado
            "Nombre Producto": element.nombreProducto,
            "Nombre Categoría": element.nombreCategoria,
            "Nombre Marca": element.nombreMarca,
            "Cantidad": element.cantidad,
            "Precio Producto": element.precioProducto,
            "Precio Total": element.precioTotal,
            "Nota Adicional": element.notaAdicional,
        }));

        const headers = [
            "ID Producto", "ID Venta", "Número Venta", "ID Cliente", "Nombre Cliente", "Dirección",
            "URL Boleta/Factura", "Fecha Venta",
            // "URL Foto Producto",  // <-- eliminado
            "Nombre Producto",
            "Nombre Categoría", "Nombre Marca",
            "Cantidad", "Precio Producto", "Precio Total", "Nota Adicional"
        ];

        const ws: XLSX.WorkSheet = XLSX.utils.json_to_sheet(reportData, { header: headers });

        // Ajustar ancho de columnas basado en contenido
        const columnWidths = reportData.reduce((widths, row) => {
            Object.keys(row).forEach((key, i) => {
                const valueLength = row[key] ? row[key].toString().length : 20;
                widths[i] = Math.max(widths[i] || 20, valueLength);
            });
            return widths;
        }, []);

        ws['!cols'] = columnWidths.map(w => ({ width: w + 4 }));

        // Estilo de cabecera
        const headerCells = headers.map((_, i) => String.fromCharCode(65 + i) + '1');
        headerCells.forEach(cell => {
            if (ws[cell]) {
                ws[cell].s = {
                    fill: { fgColor: { rgb: "4f46e5" } },
                    font: { bold: true, color: { rgb: "ffffff" } },
                    alignment: { horizontal: "center", vertical: "center" },
                    border: {
                        bottom: { style: "thin", color: { rgb: "000000" } },
                    }
                };
            }
        });

        // Alinear datos en el centro
        Object.keys(ws).forEach(cell => {
            const row = parseInt(cell.substring(1), 10);
            if (!isNaN(row) && row > 1) {
                if (ws[cell]) {
                    ws[cell].s = {
                        alignment: { horizontal: "center", vertical: "center" }
                    };
                }
            }
        });

        const wb: XLSX.WorkBook = XLSX.utils.book_new();
        XLSX.utils.book_append_sheet(wb, ws, nameSheet);
        XLSX.writeFile(wb, fileName);
    }
 
    verComprobante(venta: VentaDTO): void {
        if (venta) { window.open(venta.urlBoletaFactura, '_blank'); }
    }

    obtenerRequest(): ObtenerVentaRequest {

        const request = new ObtenerVentaRequest();

        request.destinationTimeZoneId = this._toolService.getTimeZone();
        request.idUsuario = this.decodeToken.idUsuario;
        request.lstUsuario = [];
        request.numeroVenta = this.filtroVentaForm.get('numeroVenta').value == "" ? null : this.filtroVentaForm.get('numeroVenta').value;
        request.fechaVentaInicio = this.filtroVentaForm.get('fechaVentaInicio').value == "" ? null : this.filtroVentaForm.get('fechaVentaInicio').value;
        request.fechaVentaFin = this.filtroVentaForm.get('fechaVentaFin').value == "" ? null : this.filtroVentaForm.get('fechaVentaFin').value;
        request.montoVentaInicio = null;
        request.montoVentaFin = null;

        if (this.selectUsuarioItem) {
            const selectUsuario = this.selectUsuarioItem.options.filter(x => x.selected == true && x.value != 0)
            selectUsuario.forEach(element => {
                request.lstUsuario?.push(element.value.correoElectronico);
            });
        }

        return request;
    }

    obtenerInfouserInfoLogueado(): DecodedToken {
        return this._securityService.getDecodetoken();
    }

    obtenerInfoMoneda(): MonedaDTO {
        return this._securityService.getMonedaStorage();
    }

    trackByFn(index: number, item: any): any {
        return item.id || index;
    }

    sortingActivoData = (data: any, sortHeaderId: string) => {
        return this._toolService.sortingActivoData(data, sortHeaderId);
    };

    onPageChange(event: any): void {
        const startIndex = event.pageIndex * event.pageSize;
        let endIndex = startIndex + event.pageSize;
        if (endIndex > this.allHistorialVentaDataSource.data.length) {
            endIndex = this.allHistorialVentaDataSource.data.length;
        }

        this.pageSlice.data = this.allHistorialVentaDataSource.data.slice(startIndex, endIndex);
    }

    setPageSlice(data) {
        this.pageSlice.data = data.slice(Numeracion.Cero, Numeracion.Diez);
        if (this._paginator) {
            this._paginator.pageIndex = Numeracion.Cero;
            this._paginator.pageSize = Numeracion.Diez;
        }
    }

    isMobilSize(): boolean {
        return this._toolService.isMobilSize();
    }

    closedDrawer() {
        this.matDrawer.close();
    }

    getSkeletonCount() {
        this.skeletonNumber = this.allHistorialVentaDataSource.data.length == Numeracion.Cero ? Numeracion.Ocho : this.allHistorialVentaDataSource.data.length + Numeracion.Uno
    }

    showSkeleton() {
        this.skeleton = Flags.Show
    }

    hideSkeleton() {
        this.skeleton = Flags.Hide
    }

}
