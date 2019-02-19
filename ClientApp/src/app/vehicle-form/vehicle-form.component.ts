import { VehicleService } from '../../services/vehicle.service';
import { Component, OnInit} from '@angular/core';
import { Toasts } from '../app-toasts';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import 'rxjs/add/observable/forkJoin';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
// TODO: Specify Types
// TODO: TSLint
export class VehicleFormComponent implements OnInit {
  private _emailPattern = '[a-zA-Z_]+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}';
  private _phonePattern = '[0-9]{10}';
  makes: any[];
  vehicle: any = {
    features: [],
    contact: {}
  };
  models: any[];
  features: any[];
  isRouteParams: boolean;

  constructor(
    private vehicleService : VehicleService,
    private toasts: Toasts,
    private route: ActivatedRoute,
    private router: Router) {
      this.isRouteParams = Object.keys(this.route.snapshot.params).length === 0 ? false : true;
      if(this.isRouteParams)
      {
        route.params.subscribe(
          p => 
          {
            this.vehicle.id = +p['id'];
          },
        )
      }
    }

  ngOnInit() {
    let isEdit = !this.router.url.endsWith('new');

    let requests: any[] = [
      this.vehicleService.getMakes(),
      this.vehicleService.getFeatures()];
    if(this.isRouteParams) {
      requests.push(this.vehicleService.getVehicle(this.vehicle.id));
    }
    
    Observable.forkJoin(requests)
      .subscribe(data => {
        this.makes = data[0] as any;
        this.features = data[1] as any;
        if(this.isRouteParams) {
          this.vehicle = data[2] as any;
        }
      },
    error =>{
      if(error.status === 404){
          this.router.navigate(['**']);
          return;
        }
    })
  }

  // TODO: Perfomance -> Get by id endpoint or loading all with navigation property
  onMakeChange(){
    var selectedMake = this.makes.find(m => m.id == this.vehicle.makeId);
    // TODO: Models DropDownList -> disabled or *ngIf  
    this.models = selectedMake ? selectedMake.models : [];
    delete this.vehicle.modelId;
  }

  // TODO: event type for Intellicence
  onFeatureToggle(featureId, $event){
    if($event.target.checked){
      this.vehicle.features.push(featureId);
    }
    else{
      var index = this.vehicle.features.indexOf(featureId);
      this.vehicle.features.splice(index, 1);
    }
  }

  submit(){
    this.vehicleService.createVehicle(this.vehicle)
      .subscribe( 
        x => 
        {
          let message = "Succefully sent:)";
          this.toasts.displaySuccessToast(message);
        },
        e =>
        {
          if(e.status === 400)
          {
              var featuresErrors = e.error.Features as string[];
              this.toasts.displayErrorToast(featuresErrors[0]);
          }
          else
          {
            throw e;
          }
        }
        );
  }

  public get emailPattern() : string {
    return this._emailPattern;
  }

  public get phonePattern() : string {
    return this._phonePattern;
  }
}


