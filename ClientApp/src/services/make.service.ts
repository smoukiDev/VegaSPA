import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class VihicleService {

  constructor(private http: HttpClient) { }

  getMakes(){
    return this.http.get('/api/makes');
  }

  getFeatures(){
    return this.http.get('/api/features');
  }
}
