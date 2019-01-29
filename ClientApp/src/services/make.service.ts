import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import 'rxjs/add/operator/map'

// @Injectable({
//   providedIn: 'root'
// })
@Injectable()
export class MakeService {

  constructor(private http: HttpClient) { }

  // TODO: rxjs & observables
  getMakes(){
    return this.http.get('/api/makes');
  }
}
