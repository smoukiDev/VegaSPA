import { VihicleService } from './make.service';
import { TestBed } from '@angular/core/testing';


describe('MakeService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: VihicleService = TestBed.get(VihicleService);
    expect(service).toBeTruthy();
  });
});
