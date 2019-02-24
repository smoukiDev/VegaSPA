import { KeyValuePair } from './../models/KeyValuePair';
import { VehicleService } from './../../services/vehicle.service';
import { Vehicle } from './../models/Vehicle';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-vehicle-list',
  templateUrl: './vehicle-list.component.html',
  styleUrls: ['./vehicle-list.component.css']
})
export class VehicleListComponent implements OnInit {
  vehicles : Vehicle[];
  makes: KeyValuePair[];
  filter: any = {};

  constructor(private vehicleService: VehicleService) { }

  ngOnInit() {
    this.vehicleService.getMakes()
      .subscribe(data => this.makes = data as KeyValuePair[]);

    this.populateVehicles();
  }

  onFilterChange() {
    this.populateVehicles();
  }

  resetFilter() {
    this.filter = {};
    this.onFilterChange();
  }

  deleteVehicle(id) {
    // Server-side delete
    this.vehicleService.deleteVehicle(id)
      .subscribe(data => {});

    // Client-side delete
    let index = this.vehicles
      .findIndex(v => v.id === id);
    this.vehicles.splice(index, 1);
  }

  private populateVehicles() {
    this.vehicleService.getVehicles(this.filter)
      .subscribe(data => this.vehicles = data as Vehicle[]);
  }
}
