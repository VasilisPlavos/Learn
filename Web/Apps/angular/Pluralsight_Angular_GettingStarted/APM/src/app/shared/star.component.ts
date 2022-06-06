import { Component, EventEmitter, Input, OnChanges, Output, SimpleChanges } from "@angular/core";

@Component({
    selector: 'pm-star',
    templateUrl: './star.component.html',
    styleUrls: ['./star.component.css']
})

export class StarComponent implements OnChanges {
    @Input() rating: number = 0;
    cropWidth: number = 75;
    @Output() ratingClicked: EventEmitter<string> = new EventEmitter<string>();
    
    ngOnChanges(changes: SimpleChanges): void {
        console.log(changes);
        this.cropWidth = this.rating * 75/5;
    }

    onClick() : void {
        console.log(this.rating, this.cropWidth);
        this.ratingClicked.emit(this.rating.toString());
    }
}