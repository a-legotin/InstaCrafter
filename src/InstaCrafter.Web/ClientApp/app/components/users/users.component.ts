import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'users',
    templateUrl: './users.component.html'
})
export class UsersComponent {
    public users: InstaUser[];

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        http.get(baseUrl + 'api/Users/GetAllUsers').subscribe(result => {
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
