import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';

import { throwError, Observable, of, map, tap, concatMap, mergeMap, switchMap, shareReplay, catchError } from 'rxjs';
import { Supplier } from './supplier';

@Injectable({
  providedIn: 'root'
})
export class SupplierService {
  suppliersUrl = 'api/suppliers';

  constructor(private http: HttpClient) { }

  suppliers$ = this.http.get<Supplier[]>(this.suppliersUrl)
    .pipe(
      tap(data => console.log('suppliers', JSON.stringify(data))),
      shareReplay(1),
      catchError(this.handleError)
    );

  supplierWithMap$ = of(1, 5, 8)
    .pipe(
      tap(id => console.log('mapping id', id)),
      map(id => this.http.get<Supplier>(`${this.suppliersUrl}/${id}`)),
      tap(observable => 
        observable.subscribe(supplier => console.log('mapped', supplier)))
      );

  supplierWithConcatMap$ = of(1,5,8)
    .pipe(
      tap(id => console.log('concat mapping id', id)),
      concatMap(id => this.http.get<Supplier>(`${this.suppliersUrl}/${id}`)),
      tap(supplier => console.log('concat mapped', supplier))
      );

  supplierWithMergeMap$ = of(1,5,8)
    .pipe(
      tap(id => console.log('merge mapping id', id)),
      mergeMap(id => this.http.get<Supplier>(`${this.suppliersUrl}/${id}`)),
      tap(supplier => console.log('merge mapped', supplier))
      )

  supplierWithSwitchMap$ = of(1,5,8)
    .pipe(
      tap(id => console.log('switch mapping id', id)),
      switchMap(id => this.http.get<Supplier>(`${this.suppliersUrl}/${id}`)),
      tap(supplier => console.log('switch mapped', supplier))
    )

  private handleError(err: HttpErrorResponse): Observable<never> {
    // in a real world app, we may send the server to some remote logging infrastructure
    // instead of just logging it to the console
    let errorMessage: string;
    if (err.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      errorMessage = `An error occurred: ${err.error.message}`;
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      errorMessage = `Backend returned code ${err.status}: ${err.message}`;
    }
    console.error(err);
    return throwError(() => errorMessage);
  }

}
