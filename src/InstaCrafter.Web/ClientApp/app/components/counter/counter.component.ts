import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'counter',
    templateUrl: './counter.component.html'
})
export class CounterComponent {
    public currentCount = 0;

    http: Http;
    private actionUrl: string;

    constructor(private _http: Http, @Inject('BASE_URL') baseUrl: string) {
        this.actionUrl = baseUrl + 'api/SampleData/StartRunner';

    }


    public incrementCounter() {
        this._http.get(this.actionUrl).subscribe(result => { console.log(result.json());
        }, error => console.error(error));
    }
}
