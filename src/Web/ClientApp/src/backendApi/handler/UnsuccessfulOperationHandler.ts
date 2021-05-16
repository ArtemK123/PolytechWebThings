import { RedirectHandler } from "src/services/RedirectHandler";
import { OperationStatus } from "src/backendApi/models/response/OperationResult/OperationStatus";
import { IOperationResult } from "src/backendApi/models/response/OperationResult/IOperationResult";

export class UnsuccessfulOperationHandler {
    public handle<TData>(response: IOperationResult<TData>): void {
        if (response.status === OperationStatus.Unauthorized) {
            localStorage.removeItem("email");
            RedirectHandler.redirect("/login");
        } else if (response.status === OperationStatus.Forbidden || response.status === OperationStatus.Error) {
            alert(response.message);
        }
    }
}