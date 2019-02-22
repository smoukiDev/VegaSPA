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

  private populateVehicles() {
    this.vehicleService.getVehicles(this.filter)
      .subscribe(data => this.vehicles = data as Vehicle[]);
  }
}
