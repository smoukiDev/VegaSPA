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
  vehicles : Vehicle[];
  makes: KeyValuePair[];

  constructor(private vehicleService: VehicleService) { }

  ngOnInit() {
    let source = [
      this.vehicleService.getVehicles(),
      this.vehicleService.getMakes()
    ]

    forkJoin(source).subscribe(data => {
      this.vehicles = data[0] as Vehicle[];
      this.makes = data[1] as KeyValuePair[];
    })
  }
}
