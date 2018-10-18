import { Component } from '@angular/core';
import { HttpRequest, HttpClient, HttpEventType } from '@angular/common/http';

@Component({
    selector: 'app-config-component',
    templateUrl: './config.component.html'
})
export class ConfigComponent {

    public message: string;

    constructor(private http: HttpClient) { }

    applyMask() {

        const configReq = new HttpRequest('POST', `api/config`, null, {
            reportProgress: true,
        });

        this.http.request(configReq).subscribe(event => {
            if (event.type == HttpEventType.Response)
                this.message = event.body.toString();
            else
                this.message = "Aguarde enquanto ajustamos as máscaras de telefones e celulares dos integrantes :)"
        });
    }
}
