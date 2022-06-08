import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Customer } from '../customer';

@Component({
  selector: 'app-reactive',
  templateUrl: './reactive.component.html',
  styleUrls: ['./reactive.component.css']
})
export class ReactiveComponent implements OnInit {
  customer = new Customer();

  constructor() { }

  ngOnInit(): void {
  }

  save(customerForm: NgForm): void {
    console.log(customerForm.form);
    console.log('Saved: ' + JSON.stringify(customerForm.value));
  }
}
