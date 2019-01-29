import { MakeService } from './make.service';
import { TestBed } from '@angular/core/testing';


describe('MakeService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: MakeService = TestBed.get(MakeService);
    expect(service).toBeTruthy();
  });
});
