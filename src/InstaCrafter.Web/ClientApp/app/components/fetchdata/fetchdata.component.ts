import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'fetchdata',
    templateUrl: './fetchdata.component.html'
})
export class FetchDataComponent {
    public users: InstaUser[];

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        http.get(baseUrl + 'api/SampleData/GetAllUsers').subscribe(result => {
            this.users = result.json() as InstaUser[];
        }, error => console.error(error));
    }
}

interface InstaUser {
    isVerified: boolean;
    isPrivate: boolean;
    userName: string;
    fullName: string;
}
