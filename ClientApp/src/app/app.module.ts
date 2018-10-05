import { LinkComponent } from './link/link.component';
import { IntegrantComponent } from './integrant/integrant.component';

import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { ConverterComponent } from './converter/converter.component';
import { FunctionService } from './Services/function.service';
import { IntegrantService } from './Services/integrant.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ConverterComponent,
    IntegrantComponent,
    LinkComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'converter', component: ConverterComponent },
      { path: 'integrant', component: IntegrantComponent },
      { path: 'link', component: LinkComponent }
      // { path: 'fetch-data', component: FetchDataComponent },
    ])
  ],
  providers: [FunctionService, IntegrantService],
  bootstrap: [AppComponent, ConverterComponent, IntegrantComponent]
})
export class AppModule { }
