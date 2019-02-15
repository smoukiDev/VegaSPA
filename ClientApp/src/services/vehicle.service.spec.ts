import { VehicleService } from './vehicle.service';
import { TestBed } from '@angular/core/testing';


describe('MakeService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: VehicleService = TestBed.get(VehicleService);
    expect(service).toBeTruthy();
  });
});
