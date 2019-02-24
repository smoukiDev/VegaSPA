import { Component, OnInit} from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { forkJoin } from 'rxjs';
import * as _ from 'underscore';
import { VehicleService } from '../../services/vehicle.service';
import { Toasts } from '../app-toasts';
import { Vehicle } from './../models/Vehicle';
import { SaveVehicle } from './../models/SaveVehicle';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})

export class VehicleFormComponent implements OnInit {
  private readonly _emailPattern: string;
  private readonly _phonePattern: string;
  isCollapsed = true;
  makes: any[];
  features: any[];
  models: any[];
  vehicle: SaveVehicle = {
    id: 0,
    makeId: null,
    modelId: null,
    isRegistered: false,
    features: [],
    contact: {
      name: '',
      phone: '',
      email: null
    }
  };

  constructor(
    private vehicleService: VehicleService,
    private toasts: Toasts,
    private route: ActivatedRoute,
    private router: Router ) {
      this._emailPattern = '[a-zA-Z_]+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}';
      this._phonePattern = '[0-9]{10}';
      route.params.subscribe(p => {
        // TODO: Find permanent solution to fix routes mess
        if (this.router.url != '/vehicles/new' && !Number(p.id)) {
          this.router.navigate(['**']);
        }

        if (Number(p.id)) {
          this.vehicle.id = +p['id']
        }
      });
    }

  ngOnInit() {
    let sources = [
      this.vehicleService.getMakes(),
      this.vehicleService.getFeatures(),
    ];

    if (this.vehicle.id) {
      sources.push(this.vehicleService.getVehicle(this.vehicle.id));
    }

    forkJoin(sources).subscribe(
    data => {
      this.makes = data[0] as any;
      this.features = data[1] as any;
      if (this.vehicle.id) {
        this.setVehicle(data[2] as Vehicle);
        this.populateModels();
      }
    },
    error => {
      if (error.status === 404) {
        this.router.navigate(['**']);
        return;
      }
    });
  }

  onMakeChange() {
    this.populateModels();
    delete this.vehicle.modelId;
  }

  onFeatureToggle(featureId, $event) {
    if ($event.target.checked) {
      this.vehicle.features.push(featureId);
    } else {
      let index = this.vehicle.features.indexOf(featureId);
      this.vehicle.features.splice(index, 1);
    }
  }

  submit() {
    if (this.vehicle.id) {
      this.updateVehicle();
    } else {
      this.createVehicle();
    }
  }

  delete() {
    this.deleteVehicle();
  }

  get emailPattern(): string {
    return this._emailPattern;
  }

  get phonePattern(): string {
    return this._phonePattern;
  }

  goBack() {
    this.router.navigate(['/vehicles']);
  }

  private setVehicle(vehicle: Vehicle) {
    this.vehicle.id = vehicle.id;
    this.vehicle.makeId = vehicle.make.id;
    this.vehicle.modelId = vehicle.model.id;
    this.vehicle.isRegistered = vehicle.isRegistered;
    this.vehicle.contact = vehicle.contact;
    this.vehicle.features = _.pluck(vehicle.features, 'id');
  }

  collapseFeatures() {
    this.isCollapsed = this.isCollapsed ? false : true;
  }

  private populateModels() {
    let selectedMake = this.makes.find(m => m.id == this.vehicle.makeId);
    // TODO: Disabled model dropdown depend on make drop down
    this.models = selectedMake ? selectedMake.models : [];
  }

  private createVehicle() {
    this.vehicleService.createVehicle(this.vehicle)
      .subscribe(x => {
        let message = 'Succefully sent:)';
        this.toasts.displaySuccessToast(message);
      }, e => {
        if (e.status === 400) {
          let featuresErrors = e.error.Features as string[];
          this.toasts.displayErrorToast(featuresErrors[0]);
        } else {
          throw e;
        }
      });
  }

  private updateVehicle() {
    this.vehicleService.updateVehicle(this.vehicle)
      .subscribe(data => {
        let message = 'Successfully update:)';
        this.toasts.displaySuccessToast(message);
      });
  }

  private deleteVehicle() {
    if (confirm('Are you sure?')) {
      this.vehicleService.deleteVehicle(this.vehicle.id)
        .subscribe( data => {
          let message = 'Successfully deleted:)';
          this.toasts.displaySuccessToast(message);
          this.router.navigate(['/home']);
        });
    }
  }
}


