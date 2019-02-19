import { Component, OnInit} from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import 'rxjs/add/observable/forkJoin';
import { VehicleService } from '../../services/vehicle.service';
import { Toasts } from '../app-toasts';
import { version } from 'punycode';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})

export class VehicleFormComponent implements OnInit {
  private readonly _emailPattern: string;
  private readonly _phonePattern: string;
  private makes: any[];
  private features: any[];
  private models: any[];
  // TODO: Need interface
  private vehicle: any = {
    features: [],
    contact: {}
  };

  constructor(
    private vehicleService: VehicleService,
    private toasts: Toasts,
    private route: ActivatedRoute,
    private router: Router ) {
      this._emailPattern = '[a-zA-Z_]+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}';
      this._phonePattern = '[0-9]{10}';

      route.params.subscribe(p => this.vehicle.id = +p['id']);
    }

  ngOnInit() {
    let sources = [
      this.vehicleService.getMakes(),
      this.vehicleService.getFeatures(),
    ];

    if (this.vehicle.id) {
      sources.push(this.vehicleService.getVehicle(this.vehicle.id));
    }

    Observable.forkJoin(sources).subscribe(
    data => {
      this.makes = data[0] as any;
      this.features = data[1] as any;
      if (this.vehicle.id) {
        this.vehicle = data[2] as any;
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
    let selectedMake = this.makes.find(m => m.id == this.vehicle.makeId);
    // TODO: Disabled model dropdown depend on make drop down
    this.models = selectedMake ? selectedMake.models : [];
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
    this.vehicleService.createVehicle(this.vehicle)
      .subscribe(
        x => {
          let message = 'Succefully sent:)';
          this.toasts.displaySuccessToast(message);
        },
        e => {
          if (e.status === 400) {
            let featuresErrors = e.error.Features as string[];
            this.toasts.displayErrorToast(featuresErrors[0]);
          } else {
            throw e;
          }
        });
  }

  public get emailPattern(): string {
    return this._emailPattern;
  }

  public get phonePattern(): string {
    return this._phonePattern;
  }
}


