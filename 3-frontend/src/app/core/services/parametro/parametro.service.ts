import { Injectable } from '@angular/core';
import { ApiService } from '../http/api.service';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import * as Url from '../../constants/api.constants';
import { ParametroDTO } from 'app/core/models/parametro/response/parametro-dto.model';
import { ActualizarParametroDetalleRequest } from 'app/core/models/parametro/request/actualizar-parametro-detalle-request.model';
import { ResponseDTO } from 'app/core/models/generic/response-dto.model';
import { ParametroDetalleDTO } from 'app/core/models/parametro/response/parametro-detalle-dto.model';
import { CacheKeys, KeyParams, SubKeyParams } from 'app/core/resource/parameters.constants';
import { ToolService } from '../tool/tool.service';
import { DictionaryWarning } from 'app/core/resource/dictionaryError.constants';

@Injectable({
  providedIn: 'root'
})
export class ParametroService {

  private logoPcpSubject = new BehaviorSubject<string | null>(null);
  private logoHmeSubject = new BehaviorSubject<string | null>(null);
  private readonly SISTEMA_LOGOS_KEY = CacheKeys.SISTEMA_LOGOS;

  public logoPcp$ = this.logoPcpSubject.asObservable();
  public logoHme$ = this.logoHmeSubject.asObservable();

  constructor(private apiService: ApiService,
    private _toolService: ToolService
  ) { }

  initloadLogoSystem(): void {

    const imgElement = document.getElementById('splashLogo');
 
    const cached = localStorage.getItem(this.SISTEMA_LOGOS_KEY);

    if (cached) {
      const parsed = JSON.parse(cached);
      this.logoPcpSubject.next(parsed.LOGO_PCP);
      this.logoHmeSubject.next(parsed.LOGO_HME);
      imgElement.setAttribute('src', parsed.LOGO_PCP);
      return;
    }

    const keyParam = KeyParams.LOGO_STM;

    this.apiService.query(Url.Parametro.GetAllDetalleByKeyAsync, { keyParam })
      .subscribe((response: ParametroDetalleDTO[]) => {

        const logoPcpObj = response.find(r => r.subParaKey === SubKeyParams.LOGO_PCP);
        const logoHmeObj = response.find(r => r.subParaKey === SubKeyParams.LOGO_HME);

        if (!logoPcpObj || !logoHmeObj) {
          this._toolService.showWarning(DictionaryWarning.LogosNoCargados, DictionaryWarning.Tittle); return;
        }

        const logoPcpUrl = logoPcpObj?.svalor2 ?? null;
        const logoHmeUrl = logoHmeObj?.svalor2 ?? null;

        this.logoPcpSubject.next(logoPcpUrl);
        this.logoHmeSubject.next(logoHmeUrl);

        const toCache = {
          LOGO_PCP: logoPcpUrl,
          LOGO_HME: logoHmeUrl
        };
        localStorage.setItem(this.SISTEMA_LOGOS_KEY, JSON.stringify(toCache));
      });
  }

  clearLogos(): void {
    this.logoPcpSubject.next(null);
    this.logoHmeSubject.next(null);
    localStorage.removeItem(this.SISTEMA_LOGOS_KEY);
    this.initloadLogoSystem();
  }

  GetValueBySubKeyAsync(key: string, subKey: string): Observable<ResponseDTO> {
    return this.apiService.query(Url.Parametro.GetValueBySubKeyAsync, { key, subKey })
      .pipe(tap(data => data));
  }

  GetAllParametroAsync(): Observable<ParametroDTO[]> {
    return this.apiService.query(Url.Parametro.GetAllAsync, {})
      .pipe(tap(data => data));
  }

  GetAllDetalleByIdAsync(idParametro: number, idUsuarioGuid: string): Observable<ParametroDTO[]> {
    return this.apiService.query(Url.Parametro.GetAllDetalleByIdAsync, { idParametro, idUsuarioGuid })
      .pipe(tap(data => data));
  }

  UpdateDetalleParametroAsync(request: ActualizarParametroDetalleRequest): Observable<ResponseDTO> {
    return this.apiService.post(Url.Parametro.UpdateDetalleParametroAsync, request)
      .pipe(tap(data => data));
  }

}
