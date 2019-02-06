import { VihicleService } from './../../services/make.service';
import { Component, OnInit } from '@angular/core';


@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {
  makes: any[];
  vihicle: any = {};
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
    var selectedMake = this.makes.find(m => m.id == this.vihicle.make);
    // TODO: Models DropDownList -> disabled or *ngIf  
    this.models = selectedMake ? selectedMake.models : [];
  }
}
