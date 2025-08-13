import { Component, Inject, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ErrorCodigo, Flags, Numeracion } from 'app/core/resource/dictionary.constants';
import { DictionaryErrors, DictionaryWarning } from 'app/core/resource/dictionaryError.constants';
import { ResponseDTO } from 'app/core/models/generic/response-dto.model';
import { ToolService } from 'app/core/services/tool/tool.service';
import { SecurityService } from 'app/core/auth/auth.service';
import { FuseValidators } from '@fuse/validators';
import { DecodedToken } from 'app/core/models/auth/response/decode-token-dto.model';
import { ParametroService } from 'app/core/services/parametro/parametro.service';
import { ParametroDetalleDTO } from 'app/core/models/parametro/response/parametro-detalle-dto.model';
import { ActualizarParametroDetalleRequest } from 'app/core/models/parametro/request/actualizar-parametro-detalle-request.model';
import { KeyParams, TipoInput } from 'app/core/resource/parameters.constants';

@Component({
  selector: 'app-modifica-parametro-page',
  templateUrl: './modifica-parametro-page.component.html',
  styleUrl: './modifica-parametro-page.component.scss'
})
export class ModificaParametroPageComponent implements OnInit {

  actualizaParametroDetalleForm: UntypedFormGroup;
  isCallingService: boolean = Flags.False;

  private userInfoLogueado: DecodedToken = this.obtenerInfouserInfoLogueado();
  public parametroDetalle: ParametroDetalleDTO;
  public nombreArchivo: string = '';
  public foto: string = '';
  public selectedTipoCampo: string = '';
  public selectedFiles: FileList;

  tipos: string[] = ['URL', 'IMAGEN'];

  constructor(
    public matDialogRef: MatDialogRef<ModificaParametroPageComponent>,
    @Inject(MAT_DIALOG_DATA)
    public paramsForms: any,
    private _formBuilder: UntypedFormBuilder,
    private _parametroService: ParametroService,
    private _toolService: ToolService,
    private _securityService: SecurityService
  ) {
  }

  ngOnInit(): void {

    this.parametroDetalle = this.paramsForms.parametroDetalle;
    this.selectedTipoCampo = this.parametroDetalle.tipoCampo;

    if (this.parametroDetalle.tipoCampo == TipoInput.IMAGEN) {
      this.foto = this.parametroDetalle.svalor2;
    }

    this.actualizaParametroDetalleForm = this._formBuilder.group({
      nombreParametro: [{ value: this.parametroDetalle.nombre, disabled: true }],
      tipoCampo: [this.parametroDetalle.tipoCampo, [Validators.required, Validators.minLength(Numeracion.Dos), Validators.maxLength(Numeracion.Cincuenta)]],
      svalor2: [this.parametroDetalle.svalor2],
    });

  }

  UpdateAsync() {

    if (this.actualizaParametroDetalleForm.invalid) { return; }

    const destinationTimeZoneId = this._toolService.getTimeZone();
    const idUsuario = this.userInfoLogueado.idUsuario;
    const idParametroDetalle = this.parametroDetalle.id;
    const parakey = this.parametroDetalle.paraKey;
    const txtTipoCampo = this.actualizaParametroDetalleForm.value.tipoCampo;
    const txtSvalor1 = this.nombreArchivo;
    let txtSvalor2 = this.actualizaParametroDetalleForm.value.svalor2;

    if (txtTipoCampo == TipoInput.IMAGEN) {
      txtSvalor2 = this.foto;
    }

    if (FuseValidators.isEmptyInputValue(destinationTimeZoneId)) {
      this._toolService.showWarning(DictionaryWarning.InvalidLocalizacion, DictionaryWarning.Tittle);
      return;
    }

    if (FuseValidators.isEmptyInputValue(idUsuario)) {
      this._toolService.showWarning(DictionaryWarning.InvalidUIdUsuario, DictionaryWarning.Tittle);
      return;
    }

    const request = new ActualizarParametroDetalleRequest();

    request.destinationTimeZoneIdActualizacion = destinationTimeZoneId;
    request.id = idParametroDetalle;
    request.idUsuario = idUsuario;
    request.paraKey = parakey;
    request.tipoCampo = txtTipoCampo;
    request.svalor1 = txtSvalor1;
    request.svalor2 = txtSvalor2;
    this.actualizaParametroDetalleForm.disable();
    this.isCallingService = Flags.True;

    this._parametroService.UpdateDetalleParametroAsync(request).subscribe((response: ResponseDTO) => {

      if (response.success) {
        this._toolService.showSuccess(response.message, response.titleMessage);
        this.matDialogRef.close(response);
        this.isCallingService = Flags.False;
        if (parakey == KeyParams.LOGO_STM) {
          this._parametroService.clearLogos();
        }
        return;
      }

      if (response.code == ErrorCodigo.Advertencia) {
        this._toolService.showWarning(response.message, response.titleMessage);
        this.actualizaParametroDetalleForm.enable();
        this.isCallingService = Flags.False;
        return;
      }

      this._toolService.showError(DictionaryErrors.Transaction, DictionaryErrors.Tittle);
      this.actualizaParametroDetalleForm.enable();
      this.isCallingService = Flags.False;

    }, err => {
      this._toolService.showError(DictionaryErrors.Transaction, DictionaryErrors.Tittle);
      this.actualizaParametroDetalleForm.enable();
      this.isCallingService = Flags.False;
      console.log(err);
    });
  }

  onTipoChange(event: any) {
    this.selectedTipoCampo = event.value;
  }

  onFileSelected(event: any) {

    if (this.excedeTamanioFiles(event.target.files)) {
      event.target.value = ''
      this.selectedFiles = null;
      this._toolService.showWarning(DictionaryWarning.ExcedeTamanioArchivos, DictionaryWarning.Tittle)
      return;
    }

    const files = event.target.files;

    if (files.length > Numeracion.Cero) {
      const file = files[0];
      const reader = new FileReader();
      reader.onload = (e: any) => {
        this.selectedFiles = e.target.result;
        const base64String = e.target.result as string;
        this.foto = base64String.split(',')[1];
        this.nombreArchivo = file.name;
      };
      reader.readAsDataURL(file);
    }
  }

  eliminarFoto() {
    this.selectedFiles = null;
    this.nombreArchivo = null;
    this.foto = null;
  }

  excedeTamanioFiles(selectedFiles: FileList): boolean {

    let totalSizeInBytes = Numeracion.Cero;

    for (let i = Numeracion.Cero; i < selectedFiles.length; i++) {
      const file = selectedFiles[i];
      totalSizeInBytes += file.size;
    }

    const totalSizeInKB = totalSizeInBytes / 1024;

    const maxSizeInKB = 250;

    if (totalSizeInKB > maxSizeInKB) {
      return Flags.True;
    }

    return Flags.False;
  }

  compareObjects(o1: any, o2: any): boolean {
    return o1.id === o2.id;
  }

  cerrarVentanaEmergente() {
    this.matDialogRef.close();
  }

  isMobilSize(): boolean {
    return this._toolService.isMobilSize();
  }

  obtenerInfouserInfoLogueado(): DecodedToken {
    return this._securityService.getDecodetoken();
  }

}
