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
  query: any = {};

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
    this.query = {};
    this.onFilterChange();
  }

  deleteVehicle(id) {
    this.vehicleService.deleteVehicle(id)
      .subscribe(data => {
        this.deleteOnClient(id);
      });
  }

  sortBy(columnName) {
    if(this.query.sortBy === columnName) {
      this.query.isSortAscending = !this.query.isSortAscending;
    } else {
      this.query.sortBy = columnName;
      this.query.isSortAscending = !this.query.isSortAscending;
    }

    this.populateVehicles();
  }

  private populateVehicles() {
    this.vehicleService.getVehicles(this.query)
      .subscribe(data => this.vehicles = data as Vehicle[]);
  }

  private deleteOnClient(id) {
    let index = this.vehicles
      .findIndex(v => v.id === id);
    this.vehicles.splice(index, 1);
  }
}
