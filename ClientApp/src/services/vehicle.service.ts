import { SaveVehicle } from './../app/models/SaveVehicle';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class VehicleService {
  private readonly vehiclesBaseUrl = '/api/vehicles';
  private readonly makesBaseUrl = '/api/makes';
  private readonly featuresBaseUrl = '/api/features';

  constructor(private http: HttpClient) { }

  getMakes() {
    return this.http.get(this.makesBaseUrl);
  }

  getFeatures() {
    return this.http.get(this.featuresBaseUrl);
  }

  createVehicle(vehicle) {
    return this.http.post(this.vehiclesBaseUrl, vehicle);
  }

  getVehicle(id) {
    return this.http.get(this.vehiclesBaseUrl + id);
  }

  updateVehicle(vehicle: SaveVehicle) {
    return this.http.put(this.vehiclesBaseUrl + vehicle.id, vehicle);
  }

  deleteVehicle(id: number) {
    return this.http.delete(this.vehiclesBaseUrl + id);
  }

  getVehicles() {
    return this.http.get(this.vehiclesBaseUrl);
  }
}
