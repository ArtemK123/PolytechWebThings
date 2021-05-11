import { IOperationResult } from "../models/response/OperationResult/IOperationResult";
import { OperationStatus } from "../models/response/OperationResult/OperationStatus";
import { RedirectHandler } from "../../services/RedirectHandler";

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