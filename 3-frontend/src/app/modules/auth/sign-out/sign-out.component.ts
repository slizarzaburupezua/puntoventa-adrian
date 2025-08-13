import { CommonModule, I18nPluralPipe, NgIf } from '@angular/common';
import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { Meta } from '@angular/platform-browser';
import { Router, RouterLink } from '@angular/router';
import { SecurityService } from 'app/core/auth/auth.service';
import { ParametroService } from 'app/core/services/parametro/parametro.service';
import { ToolService } from 'app/core/services/tool/tool.service';
import { finalize, Subject, takeUntil, takeWhile, tap, timer } from 'rxjs';

@Component({
    selector: 'auth-sign-out',
    templateUrl: './sign-out.component.html',
    encapsulation: ViewEncapsulation.None,
    standalone: true,
    imports: [NgIf, RouterLink, I18nPluralPipe, CommonModule],
})
export class AuthSignOutComponent implements OnInit, OnDestroy {
    logoPcp$ = this._parametroService.logoPcp$;
    countdown: number = 5;
    countdownMapping: any = {
        '=1': '# segundo',
        'other': '# segundos',
    };
    private _unsubscribeAll: Subject<any> = new Subject<any>();

    /**
     * Constructor
     */
    constructor(
        private _securityService: SecurityService,
        private _router: Router,
        private metaService: Meta,
        private _parametroService: ParametroService,
        private _toolService: ToolService,
    ) {
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    ngOnInit(): void {

        this.metaService.updateTag({ name: 'robots', content: 'noindex, nofollow' });
        // Sign out
        this._securityService.signOut();

        // Redirect after the countdown
        timer(1000, 1000)
            .pipe(
                finalize(() => {
                    this._router.navigate(['sign-in']);
                }),
                takeWhile(() => this.countdown > 0),
                takeUntil(this._unsubscribeAll),
                tap(() => this.countdown--),
            )
            .subscribe();
    }

    /**
     * On destroy
     */
    ngOnDestroy(): void {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
    }
}
