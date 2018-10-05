import { IntegrantService } from './../Services/integrant.service';
import { IIntegrant } from './../Models/integrant.interface';
import { HttpClient, HttpRequest, HttpEventType } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-integrant-component',
  templateUrl: './integrant.component.html',
  styleUrls: ['./integrant.component.css']
})
export class IntegrantComponent {

  public progress: number;
  public message: string;
  integrants: IIntegrant[] = [];
  displayedColumns: string[] = ['id', 'registrationDate', 'name',
    'datOfBirth', 'cellPhone', 'email']

  constructor(private http: HttpClient, private integrantService: IntegrantService) { }

  upload(files) {
    if (files.length == 0)
      return;

    const formData = new FormData();

    for (let file of files)
      formData.append(file.name, file);

    const uploadReq = new HttpRequest('POST', `api/upload`, formData, {
      reportProgress: true,

    });

    this.http.request(uploadReq).subscribe(event => {
      if (event.type == HttpEventType.UploadProgress)
        this.progress = Math.round(100 * event.loaded / event.total);
      else if (event.type == HttpEventType.Response)
        this.message = event.body.toString();
      else
        this.message = "Aguarde enquanto cadastramos os integrantes :)"
    });
  }

  //retorna todos os integrantes cadastrados
  getIntegrants() {
    this.integrantService.getIntegrants().subscribe((data: Array<IIntegrant>) => {
      this.integrants = data;
    });
  }
}