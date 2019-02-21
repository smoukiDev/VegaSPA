import { KeyValuePair } from './../models/KeyValuePair';
import { VehicleService } from './../../services/vehicle.service';
import { Vehicle } from './../models/Vehicle';
import { Component, OnInit } from '@angular/core';
import { forkJoin } from "rxjs";

@Component({
  selector: 'app-vehicle-list',
  templateUrl: './vehicle-list.component.html',
  styleUrls: ['./vehicle-list.component.css']
})
export class VehicleListComponent implements OnInit {
  allVehicles: Vehicle[];
  vehicles : Vehicle[];
  makes: KeyValuePair[];
  filter: any = {};

  constructor(private vehicleService: VehicleService) { }

  ngOnInit() {
    let source = [
      this.vehicleService.getVehicles(),
      this.vehicleService.getMakes()
    ]

    forkJoin(source).subscribe(data => {
      this.allVehicles = data[0] as Vehicle[];
      this.vehicles = this.allVehicles;
      this.makes = data[1] as KeyValuePair[];
    })
  }

  onFilterChange() {
    let vehicles = this.allVehicles;

    if (this.filter.makeId) {
      vehicles = vehicles
        .filter(v => v.make.id == this.filter.makeId);
    }

    this.vehicles = vehicles;
  }

  resetFilter() {
    this.filter = {};
    this.onFilterChange();
  }
}
