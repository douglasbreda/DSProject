import { IIntegrant } from './../Models/integrant.interface';
import { Injectable, Inject } from '@angular/core';
import 'rxjs/add/operator/map';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { map } from 'rxjs/operator/map';

@Injectable()
export class IntegrantService {
  constructor(private http: HttpClient) { }

    //get
  getIntegrants()  {
    return this.http.get('api/Integrant/');
    
    }
}