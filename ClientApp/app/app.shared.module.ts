import { PhotoService } from './services/photo.service';
import { AppErrorHandler } from './app.error-handler';
import { ErrorHandler } from '@angular/core';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule, BrowserXhr } from '@angular/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';
import { VehicleFormComponent } from './components/vehicle-form/vehicle-form.component';

import { VehicleService } from './services/vehicle.service';
import { AlertComponent } from './components/Alert/alert/alert.component';
import { AlertService } from './services/alert.service';
import { VehicheListComponent } from './components/vehiche-list/vehiche-list.component';
import { PaginationComponent } from './Shared/pagination.component';
import { ViewVehicleComponent } from './components/view-vehicle/view-vehicle.component';
import { ProgressService, BrowserXhrWithProgress } from './services/progress.service';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        FetchDataComponent,
        HomeComponent,
        VehicleFormComponent,
        AlertComponent,
        VehicheListComponent,
        PaginationComponent,
        ViewVehicleComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,       
        RouterModule.forRoot([
            { path: '', redirectTo: 'vehicles', pathMatch: 'full' },
            { path: 'vehicles', component: VehicheListComponent },
            { path: 'vehicles/new', component: VehicleFormComponent },
            { path: 'vehicles/edit/:id', component: VehicleFormComponent },
            { path: 'vehicles/:id', component: ViewVehicleComponent },
            { path: 'home', component: HomeComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: '**', redirectTo: 'vehicles' }
        ])
    ],
    providers:[
        {provide: ErrorHandler, useClass: AppErrorHandler},
        {provide: BrowserXhr, useClass: BrowserXhrWithProgress},
        VehicleService,
        AlertService,
        PhotoService,
        ProgressService 
    ]
})
export class AppModuleShared {
}


