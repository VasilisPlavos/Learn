import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  templateUrl: './edit2.component.html',
  styleUrls: ['./edit2.component.css'],
})
export class Edit2Component implements OnInit {
  myForm!: FormGroup;
  constructor(private fb: FormBuilder) {}

  ngOnInit(): void {
    this.myForm = this.fb.group({
      firstName: [null, [Validators.required, Validators.minLength(3)]],
      lastName: [null, Validators.required],
      colors: this.fb.array([
        this.colorItem('Red'),
        this.colorItem('Green'),
        this.colorItem('Blue'),
      ]),
    });

    console.log(this.myForm);
  }

  get fName(){
    console.log(1)
    console.log(this.myForm.get('firstName'))
    return this.myForm.get('firstName')
  }

  get colors() : FormArray
  { return this.myForm.get('colors') as FormArray;  }
  
  get colorsControls() : FormGroup[]
  { return this.colors.controls as FormGroup[]; }

  addColor() : void
  { this.colors.push(this.colorItem('')); }

  removeColor(index : number){
    this.colors.removeAt(index);
  }

  private colorItem(color: string) 
  { return this.fb.group({ name: color }); }
}
