import { Component, OnInit, OnDestroy } from '@angular/core';
import { FunctionService } from "../Services/function.service";
import { IFunction } from "../Models/function.interface";
import { FormBuilder, FormControl } from '@angular/forms';

@Component({
  selector: 'app-converter',
  templateUrl: './converter.component.html',
  styleUrls: ['./converter.component.css'],
})
export class ConverterComponent implements OnInit {

  functions: IFunction[] = [];
  textValue = new FormControl('');

  constructor(private functionService: FunctionService) {

  }

  /*
   * Retorna todas as funções que foram realizadas pela API
   */
  private getFunctions() {
    this.functionService.getFunctions(this.textValue.value.toString()).subscribe((data: Array<IFunction>) => {
      this.functions = data;
      console.log(data);
    });
  }

  ngOnInit(): void {

  }

  verifyButton() {
    this.getFunctions();
    console.log("O valor do botão é " + this.textValue.value)
  }

  ///Modifica as tags que vem com o caracter de escape do C#
  replaceHtml(value: string) {
    let s : string;
    s = value.replace("\\", "");
    console.log(s);
    return 
  }
}
