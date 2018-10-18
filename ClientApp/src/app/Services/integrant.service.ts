import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class IntegrantService {
  constructor(private http: HttpClient) { }

    //get
  getIntegrants()  {
    return this.http.get('api/Integrant/');
    }
}