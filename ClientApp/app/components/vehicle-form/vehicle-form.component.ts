import { SaveVehicle, Vehicle } from './../../Models/vehichle';
import { Observable } from 'rxjs';
import { VehicleService } from './../../services/vehicle.service';
import { Component, OnInit } from '@angular/core';
import { AlertService } from '../../services/alert.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {
  makes: any[];
  models: any[];
  features: any[];

  vehicle: SaveVehicle = {
    id: 0,
    makeId: 0,
    modelId: 0,
    isRegistered: false,
    features: [],
    contact: {
      name: '',
      email: '',
      phone: '',
    }
  };
  constructor(
    private vehicleService: VehicleService,
    private alertService: AlertService,
    private route: ActivatedRoute,
    private router: Router) {

    this.features = [];
    this.models = [];
    this.makes = [];

    route.params.subscribe(p => {
      this.vehicle.id = +p['id'] || 0;

    })
  }

  ngOnInit() {

    var sources = [
      this.vehicleService.getMakes(),
      this.vehicleService.getFeatures(),
    ];
  
    if (this.vehicle.id) {
      sources.push(this.vehicleService.getVehichle(this.vehicle.id));
    }

    Observable.forkJoin(sources).subscribe(data => {
      this.makes = data[0];
      this.features = data[1];
     
      if (this.vehicle.id) {
        this.setVehicle(data[2]);
        this.populateModels();
      }

    },
      err => {
        if (err.status == 404) {
          this.router.navigate(['/home']);
        }
      }
    );
  }

  private setVehicle(v: Vehicle) {
    this.vehicle.id = v.id;
    this.vehicle.makeId = v.make.id;
    this.vehicle.modelId = v.model.id;
    this.vehicle.isRegistered = v.isRegistered;
    this.vehicle.contact = v.contact;
    for (var key in v.features) {
      if (v.features.hasOwnProperty(key)) {
        this.vehicle.features.push(v.features[key].id)
      }
    }
  }

  onMakeChange() {
    this.populateModels();
    delete this.vehicle.modelId;
  }

  private populateModels() {
    var selectedMake = this.makes.find(m => m.id == this.vehicle.makeId);
    this.models = selectedMake ? selectedMake.models : [];
  }

  onFeautureChanged(featureId: any, $event: any) {
    if ($event.target.checked) {
      this.vehicle.features.push(featureId);
    } else {
      var index = this.vehicle.features.indexOf(featureId);
      this.vehicle.features.splice(index, 1)
    }
  }

  Submit() {
    if (this.vehicle.id) {
      this.vehicleService.update(this.vehicle).subscribe(res => {
        this.alertService.success("vehicle details updated successfully!!!");
        setTimeout(() => {
          this.clearToastNotification();
        }, 2000)
      })
    }
    else {
      this.vehicle.id = 0;
      this.vehicleService.create(this.vehicle).subscribe
        (
        x => {
          this.alertService.success("vehicle details saved successfully!!!");
          setTimeout(() => {
            this.clearToastNotification();
          }, 2000)
        }
        );
    }

  }

  clearToastNotification() {
    this.alertService.clear();
  }

  Delete()
  {
    this.vehicleService.delete(this.vehicle.id).subscribe( res => {
      this.alertService.success("vehicle details deleted successfully!!!");
      setTimeout(() => {
        this.clearToastNotification();
        this.router.navigate(['/vehicles']);
      }, 2000)
    });
  }

}
