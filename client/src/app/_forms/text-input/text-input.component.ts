import { Component, Input, OnInit, Self } from '@angular/core';
import { ControlValueAccessor, FormControl, NgControl } from '@angular/forms';

@Component({
  selector: 'app-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['./text-input.component.css']
})
export class TextInputComponent implements ControlValueAccessor {
@Input() label ='';
@Input() type = 'text';

  //Why we use Self
  //When inject something into constructor , it's going to check to see it's been use recently,
  //if it has , it's going to reuse it what it's in memoty,
  //We want to make sure ngControl is unique to the inputs that we're updating in the DOM
  //and the way that we ensure that is using @Self decorator
  constructor(@Self() public ngControl: NgControl) {
      this.ngControl.valueAccessor = this;
   }
  writeValue(obj: any): void {
    
  }

  registerOnChange(fn: any): void {

  }

  registerOnTouched(fn: any): void {

  }

  get control(): FormControl{
    return this.ngControl.control as FormControl
  }
}
