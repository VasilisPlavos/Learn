import { Component, OnInit } from '@angular/core';
import {AbstractControl, ValidatorFn, FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';
import { debounceTime } from "rxjs/operators";
import { Customer } from '../customer';

@Component({
  selector: 'app-reactive',
  templateUrl: './reactive.component.html',
  styleUrls: ['./reactive.component.css'],
})
export class ReactiveComponent implements OnInit {
  customerForm!: FormGroup;
  customer = new Customer();
  emailMessage!: string;

  constructor(private fb: FormBuilder) {}

  private validationMessages : any = {
    required: 'Please enter your email address.',
    email: 'Please enter a valid email address.'
  };

  ngOnInit(): void {
    this.customerForm = this.fb.group({
      firstName: ['', [Validators.required, Validators.minLength(3)]],
      lastName: ['', [Validators.required, Validators.maxLength(50)]],
      emailGroup: this.fb.group({
        email: ['', [Validators.required, Validators.email]],
        confirmEmail: ['', [Validators.required]],
        }, { validator: emailMatcher }),
      phone: '',
      notification: 'email',
      rating: [null, ratingRange(1,5)],
      sendCatalog: [true],

      addresses: this.fb.array([this.buildAddress()])
    });

    this.customerForm.get('notification')?.valueChanges.subscribe(
      v => this.setNotification(v)
    );

    const emailControl = this.customerForm.get('emailGroup.email');
    emailControl?.valueChanges
      .pipe(debounceTime(500))
      .subscribe(() => this.setMessage(emailControl));

      console.log(this.customerForm.get('addresses'))
  }

  buildAddress() : FormGroup {
    return this.fb.group({
      addressType: 'home',
      street1: '',
      street2: '',
      city: '',
      state: '',
      zip: '' 
    })
  }

  addAddress(): void {
    this.addresses.push(this.buildAddress());
  }

  get addresses(): FormArray{
    return <FormArray>this.customerForm.get('addresses');
  }

  setMessage(c: AbstractControl): void {
    this.emailMessage = '';
    if ((c.touched || c.dirty) && c.errors) {
      this.emailMessage = Object.keys(c.errors).map(
        key => this.validationMessages[key]).join(' ');      
    }
  }


  populateData(): void {
    this.customerForm.setValue({
      firstName: 'Jack',
      lastName: 'Spar',
      email: 'js@ma.il',
      sendCatalog: false,
    });
  }

  setNotification(notifyVia: string): void {
    const phoneControl = this.customerForm.get('phone');
    if (notifyVia == 'text') {
      phoneControl?.setValidators(Validators.required);
    } else {
      phoneControl?.clearValidators();
    }

    phoneControl?.updateValueAndValidity();
  }

  save(): void {
    console.log(this.customerForm);
    console.log('Saved: ' + JSON.stringify(this.customerForm?.value));
  }
}

function emailMatcher(c:AbstractControl):{ [key: string] : boolean } | null {
  const emailControl = c.get('email');
  const confirmEmailControl = c.get('confirmEmail');

  if (emailControl?.pristine || confirmEmailControl?.pristine){ return null; }
  if (emailControl?.value === confirmEmailControl?.value) { return null; }

  return { match: true };
}

function ratingRange(min: number, max: number): ValidatorFn 
{
  return (c: AbstractControl): { [key: string]: boolean } | null =>
  {
    var v = c.value;
    if (v != null && (isNaN(v) || v < min || v > max)) {
      return { range: true };
    }
    return null;
  }
}