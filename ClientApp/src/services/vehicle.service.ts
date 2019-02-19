import { SaveVehicle } from './../app/models/SaveVehicle';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class VehicleService {

  constructor(private http: HttpClient) { }

  getMakes() {
    return this.http.get('/api/makes');
  }

  getFeatures() {
    return this.http.get('/api/features');
  }

  createVehicle(vehicle) {
    return this.http.post('/api/vehicles', vehicle);
  }

  getVehicle(id){
    return this.http.get('/api/vehicles/' + id);
  }

  updateVehicle(vehicle: SaveVehicle) {
    return this.http.put('/api/vehicles/' + vehicle.id, vehicle);
  }
}
