import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListaParametroPageComponent } from './pages/registros-page/lista-parametro-page.component';

const routes: Routes = [
  {
    path: 'parametro',
    children: [
      {
        path: '',
        component: ListaParametroPageComponent,
        data: { title: 'Parametro' },
      },
    ]
  }
];


@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ParametroRoutingModule { }
