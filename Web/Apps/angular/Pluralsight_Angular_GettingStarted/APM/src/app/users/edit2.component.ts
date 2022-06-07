import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup } from '@angular/forms';

@Component({
  templateUrl: './edit2.component.html',
  styleUrls: ['./edit2.component.css'],
})
export class Edit2Component implements OnInit {
  myForm!: FormGroup;
  constructor(private fb: FormBuilder) {}

  ngOnInit(): void {
    this.myForm = this.fb.group({
      firstName: ['Joe'],
      lastName: ['Doe'],
      colors: this.fb.array([
        this.colorItem('Red'),
        this.colorItem('Green'),
        this.colorItem('Blue'),
      ]),
    });

    console.log(this.colorsControls);
  }

  get colorsControls() {
    var colorsArray = this.myForm.get('colors') as FormArray;
    return colorsArray.controls as FormGroup[];
  }

  private colorItem(color: string) {
    return this.fb.group({ name: color });
  }
}
