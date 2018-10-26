import { Component, OnInit, OnDestroy } from '@angular/core';
import { FunctionService } from "../Services/function.service";
import { IFunction } from "../Models/function.interface";
import { FormBuilder, FormControl } from '@angular/forms';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';

@Component({
    selector: 'app-converter',
    templateUrl: './converter.component.html',
    styleUrls: ['./converter.component.css'],
})
export class ConverterComponent implements OnInit {

    functions: IFunction[] = [];
    textValue = new FormControl('');
    textCifraCesar = new FormControl('');

    constructor(private functionService: FunctionService, private _sanitizer: DomSanitizer) {

    }

    /*
     * Retorna todas as funções que foram realizadas pela API
     */
    private getFunctions() {
        let userValue: string = this.textValue.value.toString().replace("/", "__");

        if (userValue.length > 0) {
            this.functionService.getFunctions(userValue, this.textCifraCesar.value.toString()).subscribe((data: Array<IFunction>) => {
                this.functions = data;
            });
        }else
            alert('Não foi informado nenhum valor.')
    }

    ngOnInit(): void {

    }

    verifyButton() {
        this.getFunctions();
    }

    //Recebe por parâmetro uma chave contendo um "id" de busca de html e um valor qualquer que pode ser utilizado para montar o html
    //Ex: colorBox;#000000
    getHtml(parameters: string) {
        var params = parameters.split(";");
        if (params.length > 0) {
            return this.getTag(params[0], params[1]);
        }
    }

    //Retorna uma tag de acordo com uma chave
    private getTag(key: string, value: string) {
        switch (key) {
            case "colorBox":
                return this._sanitizer.bypassSecurityTrustHtml("<div style=\"width:30px;height:30px;border:1px solid #000;background-color:" + value + "\">");
            default:
                return this._sanitizer.bypassSecurityTrustHtml(key);
        }
    }
}

