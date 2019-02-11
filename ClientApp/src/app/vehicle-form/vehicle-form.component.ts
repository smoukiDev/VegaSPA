import { VihicleService } from './../../services/make.service';
import { Component, OnInit } from '@angular/core';


@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
// TODO: Specify Types
// TODO: TSLint
export class VehicleFormComponent implements OnInit {
  makes: any[];
  // TODO: Fix naming
  vihicle: any = {
    features: [],
    contact: {}
  };
  models: any[];
  features: any[];
  constructor(private service : VihicleService) { }

  ngOnInit() {
    this.service.getMakes()
      .subscribe(makes => this.makes = makes as any);
    
    // TODO: Issue -> Features loading delays on frontend
    this.service.getFeatures()
      .subscribe(features => this.features = features as any);
  }

  // TODO: Perfomance -> Get by id endpoint or loading all with navigation property
  onMakeChange(){
    var selectedMake = this.makes.find(m => m.id == this.vihicle.makeId);
    // TODO: Models DropDownList -> disabled or *ngIf  
    this.models = selectedMake ? selectedMake.models : [];
    delete this.vihicle.modelId;
  }

  // TODO: event type for Intellicence
  onFeatureToggle(featureId, $event){
    if($event.target.checked){
      this.vihicle.features.push(featureId);
    }
    else{
      var index = this.vihicle.features.indexOf(featureId);
      this.vihicle.features.splice(index, 1);
    }
  }

  submit(){
    this.service.createVehicle(this.vihicle)
      .subscribe(x => console.log(x));
  }
}
