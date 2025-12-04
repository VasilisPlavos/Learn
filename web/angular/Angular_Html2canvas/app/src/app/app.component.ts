import { Component, OnInit } from '@angular/core';
import html2canvas from 'html2canvas';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  name = '';
  value = '';
  imageUrl = '';
  title = 'app';

  ngOnInit() {}

  update() {
    var element = document.querySelector('#capture') as HTMLElement;
    html2canvas(element).then((canvas) => {
      this.imageUrl = canvas.toDataURL();
    });
  }
}
