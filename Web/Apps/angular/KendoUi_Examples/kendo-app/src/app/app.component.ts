import { Component, OnInit } from '@angular/core';
import { from, map, of, take, tap } from 'rxjs';

@Component({
  selector: 'pm-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  pageTitle = 'Acme Product Management';

  ngOnInit(){

    of(8,9,10,11)
    .pipe(
      map(x => x*2),
      tap(i => console.log(i)),
      map(x => `item: ${x}`),
      // take(3)
      ).subscribe(console.log);

    // from([1,2,3,4]).subscribe({
    //   next: (i) => console.log(`item: ${i}`),
    //   error: (e) => console.error(`error: ${e}`),
    //   complete: () => console.info('complete') 
    // });

    // of(5,6,7).subscribe({ next: i => console.log(i)});
  }
}
