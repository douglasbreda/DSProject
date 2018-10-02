import { Injectable, Inject } from '@angular/core';
import 'rxjs/add/operator/map';
import { IFunction } from '../Models/function.interface';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { map } from 'rxjs/operator/map';

@Injectable()
export class FunctionService {
  constructor(private http: HttpClient) { }

    //get
  getFunctions(value: string)  {
    console.log(value);
    return this.http.get('api/Function/' + value);
    
    }
}
