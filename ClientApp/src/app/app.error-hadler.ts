import * as Sentry from '@sentry/browser';
import { ErrorHandler, Injectable, Injector, isDevMode} from '@angular/core';
import { Toasts } from './app-toasts';

@Injectable()
export class AppErrorHandler implements ErrorHandler{
    private toasts: Toasts;
    constructor(private injector: Injector) {}
    
    private injectToasts(){
        if(!this.toasts){
            this.toasts = this.injector.get(Toasts);
        }
    }

    handleError(error: any): void {
        if(!isDevMode()) 
        {
            Sentry.captureException(error.originalError || error);
        }

        let message = "Unexpected error has occured:(";
        this.injectToasts();
        this.toasts.displayErrorToast(message);
    }
}