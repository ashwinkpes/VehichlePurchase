import { ErrorHandler, isDevMode } from "@angular/core";

export class AppErrorHandler implements ErrorHandler {
    
   handleError(error: any): void {
    if (isDevMode)
    console.log('An unknown error occured!!!' + error);   
    
    }
}