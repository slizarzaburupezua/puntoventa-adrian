import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { forkJoin, Subject } from 'rxjs';
import { DictionaryErrors, DictionaryWarning } from 'app/core/resource/dictionaryError.constants';
import { Flags, ImagenesUrl, Numeracion } from 'app/core/resource/dictionary.constants';
import { InventarioService } from 'app/core/services/inventario/inventario.service';
import { SecurityService } from 'app/core/auth/auth.service';
import { ToolService } from 'app/core/services/tool/tool.service';
import { DecodedToken } from 'app/core/models/auth/response/decode-token-dto.model';
import { ProductoDTO } from 'app/core/models/inventario/producto/response/producto-dto.model';
import { MonedaDTO } from 'app/core/models/parametro/moneda-dto.model';
import { ObtenerProductoPorCodigoRequest } from 'app/core/models/inventario/producto/request/obtener-producto-por-codigo-request.model';
import { ProductoSeleccionadosVenta } from 'app/core/models/inventario/producto/models/producto-seleccionados-venta.model';
import { ClienteDTO } from 'app/core/models/clientes/response/cliente-dto.model';
import { ClienteService } from 'app/core/services/cliente/cliente.service';
import { ObtenerClientePorNumDocumentoCorreoRequest } from 'app/core/models/clientes/request/obtener-cliente-por-num-documento-correo-request.model';
import { RegistrarVentaRequest } from 'app/core/models/venta/request/registrar-venta-request.model';
import { FuseValidators } from '@fuse/validators';
import { ResponseDTO } from 'app/core/models/generic/response-dto.model';
import { RegistrarDetalleVentaRequest } from 'app/core/models/venta/request/registrar-detalle-venta-request.model';
import sweetAlert from 'app/core/util/sweetAlert';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { FuseConfirmationService } from '@fuse/services/confirmation';
import { VentaService } from 'app/core/services/venta/venta.service';
import { MatButtonToggleChange } from '@angular/material/button-toggle';
import { CategoriaConConteoDTO } from 'app/core/models/inventario/producto/response/categoria-con-conteo-dto.model';
import { CategoriaDTO } from 'app/core/models/inventario/categoria/response/categoria-dto.model';
import { MarcaDTO } from 'app/core/models/inventario/marca/response/marca-dto.model';
import { MatDialog } from '@angular/material/dialog';
import { RegistroProductoPageComponent } from 'app/modules/admin/inventario/pages/producto-page/modals/registro-producto-page/registro-producto-page.component';
import { MatPaginatorIntl, PageEvent } from '@angular/material/paginator';
import { SpanishVentaProductoPaginatorService } from 'app/core/services/paginator/inventario/venta/spanish-lista-venta-producto-paginator.service';
import { MatPaginator } from '@angular/material/paginator';
import { RegistroClientePageComponent } from 'app/modules/admin/clientes/pages/registros-page/modals/registro-cliente-page/registro-cliente-page.component';
import { TipoDocumentoDTO } from 'app/core/models/parametro/tipo-documento-dto.model';
import { GeneroDTO } from 'app/core/models/parametro/genero-dto.model';
import { ParametroGeneralService } from 'app/core/services/parametro/parametro-general.service';

@Component({
    selector: 'app-registro-ventas-page',
    templateUrl: './registro-ventas-page.component.html',
    styleUrl: './registro-ventas-page.component.scss',
    providers: [
        {
            provide: MatPaginatorIntl,
            useClass: SpanishVentaProductoPaginatorService,
        }
    ],
})

export class RegistroVentasPageComponent implements OnInit, OnDestroy {

    private configForm: UntypedFormGroup;
    public clienteData: ClienteDTO;

    public isCallingInsertService: boolean = Flags.False;
    public isCallingSelecClienteService: boolean = Flags.False;
    public hayCliente: boolean = Flags.False;
    public isbuscandoProductosDisabled: boolean = Flags.False;

    public filtroRegistroVentasForm: UntypedFormGroup;
    public parametroBusquedaProducto: string = '';
    public parametroBusquedaCliente: string = '';
    public notaAdicional: string;
    public fechaVenta: Date;

    private _unsubscribeAll: Subject<any> = new Subject<any>();
    public imgNoData: string = ImagenesUrl.noDataGraficos;
    public flgEnviarComprobante: boolean;
    public disabledSearch: boolean = Flags.False;

    public allCategoriaSource: CategoriaDTO[];
    public allMarcaSource: MarcaDTO[];
    public allCategoriesDataSource: CategoriaConConteoDTO[];
    public allProductByCategoryDataSource: ProductoDTO[];

    public allProductoDataSource: MatTableDataSource<ProductoDTO> = new MatTableDataSource();
    public lstProductosSeleccionados: MatTableDataSource<ProductoSeleccionadosVenta> = new MatTableDataSource();

    private decodeToken: DecodedToken = this.obtenerInfouserInfoLogueado();
    public monedaInfo: MonedaDTO = this.obtenerInfoMoneda();
    private userInfoLogueado: DecodedToken = this.obtenerInfouserInfoLogueado();

    public productoTableColumns: string[] = ['foto', 'nombre', 'stock', 'precioVenta', 'cantidad', 'total', 'acciones'];
    public allTiposDocumentos: TipoDocumentoDTO[];
    public allGeneros: GeneroDTO[];

    get totalVenta(): number {
        return this.lstProductosSeleccionados.data.reduce((total, producto) => {
            return total + (producto.precioVenta * producto.cantidad);
        }, Numeracion.Cero);
    }

    pageSize = Numeracion.Diez;
    currentPage = Numeracion.Cero;
    startIndex = Numeracion.Cero;
    endIndex = this.pageSize;

    @ViewChild(MatPaginator) paginator!: MatPaginator;

    public skeleton: boolean = Flags.False;
    public skeletonNumber: number = Numeracion.Ocho;

    constructor(
        private _fuseConfirmationService: FuseConfirmationService,
        private _formBuilder: UntypedFormBuilder,
        private _securityService: SecurityService,
        private _inventarioService: InventarioService,
        private _parametrosGeneralesService: ParametroGeneralService,
        private _clienteService: ClienteService,
        private _ventaService: VentaService,
        private _toolService: ToolService,
        private _matDialog: MatDialog,
    ) {
    }

    ngOnInit(): void {
        this.lstProductosSeleccionados.data = [];
        this.allProductoDataSource.data = [];
        this.formFiltros();
        this.GetCategoriesWithProductsCountAsync();
        this.GetAllProductsByCategoryAsync(Numeracion.Todos);
    }

    GetCategoriesWithProductsCountAsync() {
        this._inventarioService.GetCategoriesWithProductsCountAsync().subscribe((response: CategoriaConConteoDTO[]) => {
            this.allCategoriesDataSource = response;
        }, err => {
            this._toolService.showError(DictionaryErrors.Transaction, DictionaryErrors.Tittle);
            console.log(err);
        });
    }

    onPageChange(event: PageEvent) {
        this.pageSize = event.pageSize;
        this.currentPage = event.pageIndex;
        this.startIndex = this.currentPage * this.pageSize;
        this.endIndex = this.startIndex + this.pageSize;
    }

    formFiltros() {
        this.filtroRegistroVentasForm = this._formBuilder.group({
            fechaRegistroVenta: [this._toolService.getDateTimeNow(), [Validators.required]],
        });
    }

    aumentarCantidad(producto: ProductoSeleccionadosVenta): void {
        if (producto.cantidad < producto.stock) {
            producto.cantidad++;

        } else {
            this._toolService.showWarning(DictionaryWarning.StockLimite, DictionaryErrors.Tittle);
        }
    }

    onSelectCategoryProduct(category: MatButtonToggleChange): void {
        this.GetAllProductsByCategoryAsync(category.value.idCategoria);
    }

    GetAllProductsByCategoryAsync(idCategory: number) {
        this.showSkeleton()
        this._inventarioService.GetAllProductsByCategoryAsync(idCategory).subscribe((response: ProductoDTO[]) => {
            this.allProductByCategoryDataSource = response;
            this.startIndex = Numeracion.Cero;
            this.endIndex = this.pageSize;
            if (this.paginator) {
                this.paginator.firstPage();
            }
            this.hideSkeleton();
        }, err => {
            this.hideSkeleton();
            this._toolService.showError(DictionaryErrors.Transaction, DictionaryErrors.Tittle);
            console.log(err);
        });
    }

    InsertAsync() {

        const destinationTimeZoneId = this._toolService.getTimeZone();
        const idUsuario = this.userInfoLogueado.idUsuario;
        const idCliente = this.clienteData?.id;
        const txtFechaRegistroVenta = this.filtroRegistroVentasForm.get('fechaRegistroVenta').value;

        const lstDetalleVenta: RegistrarDetalleVentaRequest[] = this.lstProductosSeleccionados.data.map(producto => ({
            idProducto: producto?.id,
            urlFotoProducto: producto?.urlFoto,
            nombreProducto: producto?.nombre,
            colorProducto: producto?.color,
            nombreCategoria: producto?.categoria.nombre,
            colorCategoria: producto?.categoria.color,
            nombreMarca: producto?.marca.nombre,
            colorMarca: producto?.marca.color,
            cantidad: producto?.cantidad,
            nombreMedida: producto?.categoria?.medida?.nombre,
            precioCompra: producto?.precioCompra,
            precioVenta: producto?.precioVenta,
        }));

        if (this.lstProductosSeleccionados.data.length == Numeracion.Cero) {
            this._toolService.showWarning(DictionaryWarning.NohayClienteIngresadoOProductosSeleccionados, DictionaryErrors.Tittle);
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

        if (FuseValidators.isEmptyInputValue(txtFechaRegistroVenta)) {
            this._toolService.showWarning(DictionaryWarning.InvalidFechaRegistroVenta, DictionaryWarning.Tittle);
            return;
        }

        this.configForm = this._formBuilder.group({
            title: 'Confirmación de la Venta',
            message: '¿Conforme con los datos ingresados?',
            icon: this._formBuilder.group({
                show: Flags.True,
                name: 'heroicons_outline:exclamation-triangle',
                color: 'primary',
            }),
            actions: this._formBuilder.group({
                confirm: this._formBuilder.group({
                    show: Flags.True,
                    label: 'Si',
                    color: 'primary',
                }),
                cancel: this._formBuilder.group({
                    show: Flags.True,
                    label: 'No',
                }),
            }),
            dismissible: Flags.True,
        });

        const dialogRef = this._fuseConfirmationService.open(this.configForm.value);

        dialogRef.afterClosed().subscribe((result) => {

            if (result != "confirmed") { return; }

            const request = new RegistrarVentaRequest();

            request.destinationTimeZoneIdRegistro = destinationTimeZoneId;
            request.idUsuario = idUsuario;
            request.idCliente = idCliente;
            request.lstDetalleVenta = lstDetalleVenta;
            request.notaAdicional = this.notaAdicional;
            request.fechaRegistroVenta = txtFechaRegistroVenta;
            request.flgEnviarComprobante = this.flgEnviarComprobante;
            this.isCallingInsertService = Flags.Show;

            this._ventaService.InsertAsync(request).subscribe((response: ResponseDTO) => {

                if (response.success) {
                    this.clienteData = new ClienteDTO();
                    this.hayCliente = Flags.False;
                    this.lstProductosSeleccionados.data = [];
                    this.isCallingInsertService = Flags.Hide;
                    this.notaAdicional = "";
                    window.open(response.value, '_blank');
                    sweetAlert.success(response.titleMessage, response.message);
                    return;
                }

                this._toolService.showError(DictionaryErrors.Transaction, DictionaryErrors.Tittle);
                this.isbuscandoProductosDisabled = Flags.False;
                this.isCallingInsertService = Flags.Hide;

            }, err => {
                this._toolService.showError(DictionaryErrors.Transaction, DictionaryErrors.Tittle);
                console.log(err);
                this.isbuscandoProductosDisabled = Flags.False;
                this.isCallingInsertService = Flags.Hide;
                this.clienteData = new ClienteDTO();
                this.hayCliente = Flags.False;
                this.isCallingInsertService = Flags.Hide;
                this.notaAdicional = "";
            });

        });
    }

    buscarCliente() {

        if (!this.parametroBusquedaCliente && this.parametroBusquedaCliente.trim() == '') {
            return;
        }

        const request = new ObtenerClientePorNumDocumentoCorreoRequest();
        request.idUsuario = this.decodeToken.idUsuario;
        request.parametro = this.parametroBusquedaCliente;

        this.isCallingSelecClienteService = Flags.True;

        this._clienteService.GetByNumDocumentoCorreoAsync(request).subscribe((clienteResponse: ClienteDTO) => {

            this.isCallingSelecClienteService = Flags.False;

            if (clienteResponse) {
                this.clienteData = clienteResponse;
                this.hayCliente = Flags.True;
                this.parametroBusquedaCliente = '';
                return;
            }

            this.hayCliente = Flags.False;
            this.askInsertCliente();

        }, err => {
            this._toolService.showError(DictionaryErrors.Transaction, DictionaryErrors.Tittle);
            console.log(err);
            this.isCallingSelecClienteService = Flags.False;

        });
    }

    onEnviarComprobanteChange(event: Event) {
        const isChecked = (event.target as HTMLInputElement).checked;
        if (isChecked) {
            const cliente = this.clienteData;

            if (!this.hayCliente || !cliente || !cliente.correoElectronico || cliente.correoElectronico.trim() === '') {
                this._toolService.showWarning(
                    'Debe seleccionar un cliente con correo válido para enviar el comprobante.',
                    'Advertencia'
                );
                (event.target as HTMLInputElement).checked = Flags.False;
                this.flgEnviarComprobante = Flags.False;
                return;
            }
        }

        this.flgEnviarComprobante = isChecked;
    }

    quitarCliente(): void {
        this.clienteData = null;
        this.hayCliente = Flags.False;
        this.flgEnviarComprobante = Flags.False;
    }

    getFilterComboConsulta() {
        forkJoin({
            dataCategorias: this._inventarioService.GetAllCategoriasForComboBoxAsync(),
            dataMarcas: this._inventarioService.GetAllMarcasForComboBoxAsync(),
        }).subscribe({
            next: (response) => {

                this.allCategoriaSource = response.dataCategorias;
                this.allMarcaSource = response.dataMarcas;

                const dialogRef = this._matDialog.open(RegistroProductoPageComponent, {
                    data: {
                        lstCategoria: this.allCategoriaSource,
                        lstMarca: this.allMarcaSource
                    }
                });
                dialogRef.afterClosed()
                    .subscribe((response) => {
                        if (response) {
                            if (response.success == Flags.SuccessTransaction) {
                                this.GetCategoriesWithProductsCountAsync();
                            }
                        }
                        this.disabledSearch = Flags.False;
                    });
            },
            error: (err) => {
                this._toolService.showError(DictionaryErrors.Transaction, DictionaryErrors.Tittle);
                console.log(err);
            },
        });
    }

    onShowFormRegistrarCliente() {
        forkJoin({
            dataTipoDocumento: this._parametrosGeneralesService.GetAllTipoDocumentoAsync(),
            dataGenero: this._parametrosGeneralesService.GetAllGeneroAsync(),
        }).subscribe({
            next: (response) => {

                const dialogRef = this._matDialog.open(RegistroClientePageComponent, {
                    data: {
                        lstTipoDocumento: response.dataTipoDocumento,
                        lstGenero: response.dataGenero,

                    }
                });
                dialogRef.afterClosed()
                    .subscribe((response) => {
                        if (response) {
                            if (response.success == Flags.SuccessTransaction) {
                                this.buscarCliente();
                            }
                        }
                    });
            },
            error: (err) => {
                this._toolService.showError(DictionaryErrors.Transaction, DictionaryErrors.Tittle);
                console.log(err);
            },
        });
    }

    disminuirCantidad(producto: ProductoSeleccionadosVenta): void {
        if (producto.cantidad > 1) {
            producto.cantidad--;
            this.lstProductosSeleccionados.data = [...this.lstProductosSeleccionados.data];
        }
    }

    quitarProducto(producto: ProductoSeleccionadosVenta): void {
        this.lstProductosSeleccionados.data = this.lstProductosSeleccionados.data.filter(p => p.id !== producto.id);
    }

    calcularTotalProducto(producto: ProductoSeleccionadosVenta): number {
        return producto.cantidad * producto.precioVenta;
    }

    filtrarProductos(event: Event) {

        const inputElement = event.target as HTMLInputElement;
        const trimmedInput = inputElement.value.trim();

        if (trimmedInput === '') {
            return;
        }

        const input = (event.target as HTMLInputElement).value;
        if (input.length >= Numeracion.Cuatro) {
            const request = new ObtenerProductoPorCodigoRequest();
            request.idUsuario = this.decodeToken.idUsuario;
            request.parametro = input;
            this.disabledSearch = Flags.True;
            this._inventarioService.GetProductoByCodeAsync(request).subscribe((produsctosResponse: ProductoDTO[]) => {
                if (produsctosResponse.length > Numeracion.Cero) {
                    this.allProductoDataSource.data = produsctosResponse;
                    this.disabledSearch = Flags.False;
                    return;
                }
                this.askInsertProduct();
            }, err => {
                this._toolService.showError(DictionaryErrors.Transaction, DictionaryErrors.Tittle);
                console.log(err);
            });
            return;
        }
        this.allProductoDataSource.data = [];
    }

    askInsertProduct() {
        this.configForm = this._formBuilder.group({
            title: 'Producto no encontrado',
            message: 'El producto que busca no existe en el sistema. ¿Desea registrarlo ahora?',
            icon: this._formBuilder.group({
                show: Flags.True,
                name: 'heroicons_outline:information-circle',
                color: 'primary',
            }),
            actions: this._formBuilder.group({
                confirm: this._formBuilder.group({
                    show: Flags.True,
                    label: 'Registrar producto',
                    color: 'accent',
                }),
                cancel: this._formBuilder.group({
                    show: Flags.True,
                    label: 'Cancelar',
                }),
            }),
            dismissible: Flags.True,
        });

        const dialogRef = this._fuseConfirmationService.open(this.configForm.value);

        dialogRef.afterClosed().subscribe((result) => {
            if (result != "confirmed") { this.disabledSearch = Flags.False; return; }
            this.getFilterComboConsulta();
        });
    }

    askInsertCliente() {
        this.clienteData = new ClienteDTO();
        this.configForm = this._formBuilder.group({
            title: 'Cliente no encontrado',
            message: 'El cliente que busca no existe en el sistema. ¿Desea registrarlo ahora?',
            icon: this._formBuilder.group({
                show: Flags.True,
                name: 'heroicons_outline:information-circle',
                color: 'primary',
            }),
            actions: this._formBuilder.group({
                confirm: this._formBuilder.group({
                    show: Flags.True,
                    label: 'Registrar cliente',
                    color: 'accent',
                }),
                cancel: this._formBuilder.group({
                    show: Flags.True,
                    label: 'Cancelar',
                }),
            }),
            dismissible: Flags.True,
        });

        const dialogRef = this._fuseConfirmationService.open(this.configForm.value);

        dialogRef.afterClosed().subscribe((result) => {
            if (result != "confirmed") { this.disabledSearch = Flags.False; return; }
            this.onShowFormRegistrarCliente();
        });
    }

    keyupEnterProductos(event: Event) {

        const inputElement = event.target as HTMLInputElement;
        const trimmedInput = inputElement.value.trim();

        if (trimmedInput === '') {
            return;
        }

        const request = new ObtenerProductoPorCodigoRequest();
        request.idUsuario = this.decodeToken.idUsuario;
        request.parametro = trimmedInput;

        this._inventarioService.GetProductoByCodeAsync(request).subscribe((produsctosResponse: ProductoDTO[]) => {
            if (produsctosResponse.length > Numeracion.Cero) {
                this.allProductoDataSource.data = produsctosResponse;
                return;
            }
            this.askInsertProduct();
        }, err => {
            this._toolService.showError(DictionaryErrors.Transaction, DictionaryErrors.Tittle);
            console.log(err);
        });
        return;
    }

    seleccionarProducto(producto: ProductoDTO) {

        if (producto.stock <= Numeracion.Cero) {
            this._toolService.showWarning(DictionaryWarning.StockInsuficiente, DictionaryErrors.Tittle);
            return;
        }

        const index = this.lstProductosSeleccionados.data.findIndex(p => p.codigo === producto.codigo);

        if (index === -1) {
            producto.cantidad = Numeracion.Uno;
            this.lstProductosSeleccionados.data = [...this.lstProductosSeleccionados.data, producto];
        } else {
            const cantidadActual = this.lstProductosSeleccionados.data[index].cantidad;

            if (cantidadActual >= producto.stock) {
                this._toolService.showWarning(DictionaryWarning.StockLimite, DictionaryErrors.Tittle);
                return;
            }

            this.lstProductosSeleccionados.data[index].cantidad++;
        }

        this.lstProductosSeleccionados._updateChangeSubscription();
        this.parametroBusquedaProducto = "";
        this.allProductoDataSource.data = [];
    }

    ngOnDestroy(): void {
        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
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

    getSkeletonCount() {
        this.skeletonNumber = this.allProductByCategoryDataSource?.length == Numeracion.Cero ? Numeracion.Ocho : this.allProductByCategoryDataSource?.length + Numeracion.Uno
    }

    showSkeleton() {
        this.skeleton = Flags.Show
    }

    hideSkeleton() {
        this.skeleton = Flags.Hide
    }

}
