import { Component, Input, OnChanges, SimpleChanges } from "@angular/core";

@Component({
    selector: 'pm-star',
    templateUrl: './star.component.html',
    styleUrls: ['./star.component.css']
})

export class StarComponent implements OnChanges {
    @Input() rating: number = 0;
    cropWidth: number = 75;
    
    ngOnChanges(changes: SimpleChanges): void {
        console.log(changes);
        this.cropWidth = this.rating * 75/5;
    }
}