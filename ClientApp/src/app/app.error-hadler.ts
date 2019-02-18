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
            Sentry.init({
                dsn: "https://efbc1a22f1704e4ba72ab81a0df38fbe@sentry.io/1395070"
              });
            Sentry.captureException(error.originalError || error);
        }

        let message = "Unexpected error has occured:(";
        this.injectToasts();
        this.toasts.displayErrorToast(message);
    }
}