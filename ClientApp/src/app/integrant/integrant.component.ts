import { HttpClient, HttpRequest, HttpEventType } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-integrant-component',
  templateUrl: './integrant.component.html'
})
export class IntegrantComponent {

  public progress: number;
  public message: string;
  constructor(private http: HttpClient) { }

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
}