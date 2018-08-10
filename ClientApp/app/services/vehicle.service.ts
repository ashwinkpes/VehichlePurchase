import { SaveVehicle } from './../Models/vehichle';
import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable()
export class VehicleService {

  
  constructor(private http:Http) { }
   
  getMakes()
  {
    return this.http.get('/api/makes').map(res=>res.json());
  }

  getFeatures()
  {
    return this.http.get('/api/features').map(res=>res.json());
  }

  create(vehicle: SaveVehicle)
  {
    return this.http.post('/api/vehicles',vehicle).map(res=>res.json());
  }

  getVehichle(id:number)
  {
    return this.http.get('/api/vehicles/GetById/'+id).map(res=>res.json());
  }

  update(vehichle: SaveVehicle)
  {
    return this.http.put('/api/vehicles/'+ vehichle.id,vehichle).map(res=>res.json());
  }

  delete(id: number)
  {
    return this.http.delete('/api/vehicles/'+ id).map(res=>res.json());
  }

  getAllVehichles(filter:any)
  {
    return this.http.get('/api/vehicles/' + '?' + this.toQueryString(filter)).map(res=>res.json());
  }

  toQueryString(obj:any) {
    var parts = [];
    for (var property in obj) {
      var value = obj[property];
      if (value != null && value != undefined) 
        parts.push(encodeURIComponent(property)  +'=' + encodeURIComponent(value));
    }

    return parts.join('&');
  }
}
