import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subject } from 'rxjs';
import { MatDialog } from '@angular/material/dialog';
import { DictionaryErrors } from 'app/core/resource/dictionaryError.constants';
import { DictionaryInfo, Flags, ImagenesUrl, Numeracion } from 'app/core/resource/dictionary.constants';
import { SecurityService } from 'app/core/auth/auth.service';
import { ToolService } from 'app/core/services/tool/tool.service';
import { MatPaginator, MatPaginatorIntl } from '@angular/material/paginator';
import { UntypedFormBuilder, UntypedFormGroup } from '@angular/forms';
import { DecodedToken } from 'app/core/models/auth/response/decode-token-dto.model';
import { TipoDocumentoDTO } from 'app/core/models/parametro/tipo-documento-dto.model';
import { SpanishParametroPaginatorService } from 'app/core/services/paginator/parametro/registros/spanish-lista-clientes-paginator.service';
import { ParametroDTO } from 'app/core/models/parametro/response/parametro-dto.model';
import { ParametroService } from 'app/core/services/parametro/parametro.service';
import { ParametroDetalleDTO } from 'app/core/models/parametro/response/parametro-detalle-dto.model';
import { ModificaParametroPageComponent } from './modals/modifica-parametro-page/modifica-parametro-page.component';

@Component({
    selector: 'app-lista-parametro-page',
    templateUrl: './lista-parametro-page.component.html',
    styleUrl: './lista-parametro-page.component.scss',
    providers: [
        {
            provide: MatPaginatorIntl,
            useClass: SpanishParametroPaginatorService,
        }
    ],
})

export class ListaParametroPageComponent implements OnInit, AfterViewInit, OnDestroy {

    private decodeToken: DecodedToken;

    public disabledAcciones: boolean = Flags.False;
    public disabledBuscar: boolean = Flags.False;
    public disabledExportar: boolean = Flags.False;

    public skeleton: boolean = Flags.False;
    public skeletonNumber: number = Numeracion.Ocho;

    public textoResultadoTable: string = "";
    public imgNoDataUltimosRegistros: string = ImagenesUrl.noDataUltimosRegistros;

    @ViewChild('parametroTable', { read: MatSort })
    private parametroTableMatSort: MatSort;

    @ViewChild('parametroDetalleTable', { read: MatSort })
    private parametroDetalleTableMatSort: MatSort;

    public minDate: Date = this._toolService.getMinDateFIlter();
    public maxDate: Date = this._toolService.getMaxDateFIlter();

    filtroClienteForm: UntypedFormGroup;

    @ViewChild(MatPaginator) private _paginator: MatPaginator;
    @ViewChild(MatPaginator) private _paginatorDetalle: MatPaginator;

    public pageSlice: MatTableDataSource<ParametroDTO> = new MatTableDataSource();
    public allParametroDataSource: MatTableDataSource<ParametroDTO> = new MatTableDataSource();
    public pageDetalleSlice: MatTableDataSource<ParametroDetalleDTO> = new MatTableDataSource();
    public allParametroDetalleDataSource: MatTableDataSource<ParametroDetalleDTO> = new MatTableDataSource();

    public allTiposDocumentos: TipoDocumentoDTO[];
    public parametroTableColumns: string[] = ['nombre', 'descripcion', 'fechaRegistro', 'acciones'];
    public parametroDetalleTableColumns: string[] = ['nombre', 'descripcion', 'fechaRegistro', 'acciones'];
    private _unsubscribeAll: Subject<any> = new Subject<any>();

    constructor(
        private _formBuilder: UntypedFormBuilder,
        private _securityService: SecurityService,
        private _parametroService: ParametroService,
        private _matDialog: MatDialog,
        private _toolService: ToolService,) {
    }

    ngAfterViewInit() {
        this.allParametroDataSource.sort = this.parametroTableMatSort;
        this.allParametroDataSource.paginator = this._paginator;
        this.allParametroDetalleDataSource.sort = this.parametroDetalleTableMatSort;
        this.allParametroDataSource.paginator = this._paginatorDetalle;
    }

    ngOnDestroy() {
        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
    }

    ngOnInit() {
        this.formFiltros();
        this.showSkeleton();
        this.GetAllByFilterAsync();
    }

    formFiltros() {
        this.decodeToken = this.obtenerInfouserInfoLogueado();
        this.filtroClienteForm = this._formBuilder.group({
            tiposDocumento: [''],
            numeroDocumento: [''],
            generos: [''],
            nombres: [''],
            apellidos: [''],
            celular: [''],
            direccion: [''],
            correoElectronico: [''],
            fechaRegistroInicio: [this._toolService.getStartDateOfYear()],
            fechaRegistroFin: [this._toolService.getEndDateOfYear()],
        });
    }

    GetAllByFilterAsync() {

        this.disabledBuscar = Flags.Deshabilitar;

        this._parametroService.GetAllParametroAsync().subscribe((response: ParametroDTO[]) => {
            this.allParametroDataSource.data = response;
            this.pageSlice.data = [];
            if (this.allParametroDataSource.data.length > Numeracion.Cero) {
                this.disabledExportar = Flags.False;
                this.setPageSlice(this.allParametroDataSource.data);
                this.disabledBuscar = Flags.False;
                this.hideSkeleton();
                return;
            }
            this.textoResultadoTable = DictionaryInfo.NoDataResult;
            this.disabledBuscar = Flags.False;
            this.disabledExportar = Flags.True;
            this.hideSkeleton();

        }, err => {
            this.hideSkeleton();
            this._toolService.showError(DictionaryErrors.Transaction, DictionaryErrors.Tittle);
            this.disabledBuscar = Flags.False;
            console.log(err);
        });
    }

    onVerParametroDetalle(parametro: ParametroDTO) {
        const idUsuario = this.decodeToken.idUsuario;
        this.GetAllDetalleByIdAsync(parametro.id, idUsuario);
    }

    GetAllDetalleByIdAsync(idParametro: number, idUsuarioGuid: string) {
        this._parametroService.GetAllDetalleByIdAsync(idParametro, idUsuarioGuid).subscribe((response: ParametroDetalleDTO[]) => {
            this.allParametroDetalleDataSource.data = response;
            this.pageDetalleSlice.data = [];
            if (this.allParametroDetalleDataSource.data.length > Numeracion.Cero) {
                this.disabledExportar = Flags.False;
                this.setPageSliceDetalle(this.allParametroDetalleDataSource.data);
                this.disabledBuscar = Flags.False;
                this.hideSkeleton();
                return;
            }
            this.textoResultadoTable = DictionaryInfo.NoDataResult;
            this.disabledBuscar = Flags.False;
            this.disabledExportar = Flags.True;
            this.hideSkeleton();

        }, err => {
            this.hideSkeleton();
            this._toolService.showError(DictionaryErrors.Transaction, DictionaryErrors.Tittle);
            this.disabledBuscar = Flags.False;
            console.log(err);
        });
    }
 
    onShowFormModificaDetalleParametro(parametroDetalle: ParametroDetalleDTO) {
        const dialogRef = this._matDialog.open(ModificaParametroPageComponent, {
            data: {
                parametroDetalle: parametroDetalle,
            }
        });
        dialogRef.afterClosed()
            .subscribe((response) => {
                if (response) {
                    if (response.success == Flags.SuccessTransaction) {
                        this.getSkeletonCount();
                        this.showSkeleton();
                        const idUsuario = this.decodeToken.idUsuario;
                        this.GetAllDetalleByIdAsync(parametroDetalle.idParametro, idUsuario);
                    }
                }
            });
    }

    obtenerInfouserInfoLogueado(): DecodedToken {
        return this._securityService.getDecodetoken();
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
        if (endIndex > this.allParametroDataSource.data.length) {
            endIndex = this.allParametroDataSource.data.length;
        }

        this.pageSlice.data = this.allParametroDataSource.data.slice(startIndex, endIndex);
    }

    setPageSlice(data) {
        this.pageSlice.data = data.slice(Numeracion.Cero, Numeracion.Diez);
        if (this._paginator) {
            this._paginator.pageIndex = Numeracion.Cero;
            this._paginator.pageSize = Numeracion.Diez;
        }
    }

    setPageSliceDetalle(data) {
        this.pageDetalleSlice.data = data.slice(Numeracion.Cero, Numeracion.Diez);
        if (this._paginatorDetalle) {
            this._paginatorDetalle.pageIndex = Numeracion.Cero;
            this._paginatorDetalle.pageSize = Numeracion.Diez;
        }
    }

    isMobilSize(): boolean {
        return this._toolService.isMobilSize();
    }

    getSkeletonCount() {
        this.skeletonNumber = this.allParametroDataSource.data.length == Numeracion.Cero ? Numeracion.Ocho : this.allParametroDataSource.data.length + Numeracion.Uno
    }

    showSkeleton() {
        this.skeleton = Flags.Show
    }

    hideSkeleton() {
        this.skeleton = Flags.Hide
    }

}
