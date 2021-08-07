import { Component, Injector } from '@angular/core';
import { Router } from '@angular/router';
import { accountModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/app-component-base';
// import { AppUrlService } from '@shared/common/nav/app-url.service';
import { AccountServiceProxy, ForgotPassword } from '@shared/service-proxies/service-proxies';
import { finalize } from 'rxjs/operators';

@Component({
    templateUrl: './forgot-password.component.html',
    animations: [accountModuleAnimation()]
})
export class ForgotPasswordComponent extends AppComponentBase {

    model: ForgotPassword = new ForgotPassword();

    saving = false;

    constructor (
        injector: Injector,
        private _accountService: AccountServiceProxy,
        // private _appUrlService: AppUrlService,
        private _router: Router
        ) {
        super(injector);
    }

    save(): void {
        this.saving = true;
        this.model.sentSuccessfully = false;
        this._accountService.sendPasswordResetCode(this.model)
            .pipe(finalize(() => { this.saving = false; }))
            .subscribe(() => {
                debugger;
                this.message.success('Please check your mail', this.l('MailSent')).then(() => {
                    this._router.navigate(['account/login']);
                });
            });
    }
}
