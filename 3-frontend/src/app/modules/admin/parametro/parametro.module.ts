import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'app/shared/shared.module';
import { MAT_DATE_LOCALE } from '@angular/material/core';
import { ToolService } from 'app/core/services/tool/tool.service';
import { ParametroRoutingModule } from './parametro-routing.module';
import { ListaParametroPageComponent } from './pages/registros-page/lista-parametro-page.component';
import { ModificaParametroPageComponent } from './pages/registros-page/modals/modifica-parametro-page/modifica-parametro-page.component';

const BASE_MODULES = [CommonModule, SharedModule, ParametroRoutingModule];

const BASE_COMPONENTS = [
  ListaParametroPageComponent,
];

const ACTUALIZACION = [
  ModificaParametroPageComponent
]

const PROVIDERS = [
  {
    provide: MAT_DATE_LOCALE,
    useFactory: (localeService: ToolService) => localeService.getuserInfoLogueadoCultureInfo(),
    deps: [ToolService]
  }
]

@NgModule({
  declarations: [BASE_COMPONENTS, ACTUALIZACION],
  imports: [BASE_MODULES],
  exports: [BASE_MODULES],
  providers: [PROVIDERS]
})
export class ParametroModule { }
