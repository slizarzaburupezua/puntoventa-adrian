import { Component } from '@angular/core';
import { NavigationEnd, Router, RouterOutlet } from '@angular/router';
import { TourMatMenuModule } from 'ngx-ui-tour-md-menu';
 
@Component({
    selector   : 'app-root',
    templateUrl: './app.component.html',
    styleUrls  : ['./app.component.scss'],
    standalone : true,
    imports    : [RouterOutlet, TourMatMenuModule ],
})
export class AppComponent
{
    /**
     * Constructor
     */
    constructor(private router: Router )
    {
   

    }

    
}
