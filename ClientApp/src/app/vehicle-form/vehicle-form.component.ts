import { FeatureService } from './../../services/feature.service';
import { MakeService } from './../../services/make.service';
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
  constructor(private makeService : MakeService, private featureService: FeatureService) { }

  ngOnInit() {
    this.makeService.getMakes()
    .subscribe(makes => this.makes = makes as any);
    
    this.featureService.getFeatures()
    .subscribe(features => this.features = features as any);
  }

  // TODO: It could be implemented using GET Request by Id
  //       But model in this case probably won't contain collection property.
  onMakeChange(){
    var selectedMake = this.makes.find(m => m.id == this.vihicle.make);
    // TODO: Disabled or *ngIf  
    this.models = selectedMake ? selectedMake.models : [];
  }
}
