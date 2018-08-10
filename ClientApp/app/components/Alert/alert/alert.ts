export class Alert {
    type: AlertType;
    message: string;

    constructor() { 
        this.message = '';
        this.type = AlertType.Success;
    }
}

export enum AlertType {
    Success,
    Error,
    Info,
    Warning
}