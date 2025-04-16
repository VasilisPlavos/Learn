import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {
  myForm!: FormGroup;

  ngOnInit(): void {
    this.myForm = new FormGroup({
      firstName: new FormControl('Joe'),
      lastName: new FormControl('Doe')
    })


    this.myForm.setValue({ firstName: 'J2', lastName: 'D' })
    this.myForm.get('firstName')?.setValue('J3')
    
    const firstName = this.myForm.get('firstName') as FormControl
    firstName.setValue('J4')

    // this.myForm.setValue requires all properties (firstName && lastName)
    // this.myForm.setValue({ firstName: 'J5' })

    // this.myForm.patchValue does not require all properties
    this.myForm.patchValue({ firstName: 'J6' })
  }
}
