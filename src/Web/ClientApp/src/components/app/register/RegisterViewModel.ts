import * as ko from "knockout";
import { ICreateUserRequest } from "../../../backendApi/models/request/user/ICreateUserRequest";
import { RedirectHandler } from "../../../services/RedirectHandler";
import { OperationStatus } from "../../../backendApi/models/response/OperationResult/OperationStatus";
import { IViewModel } from "../../../componentsRegistration/IViewModel";
import { UserApiClient } from "../../../backendApi/clients/UserApiClient";
import { IOperationResult } from "../../../backendApi/models/response/OperationResult/IOperationResult";

export class RegisterViewModel implements IViewModel {
    public readonly email: ko.Observable<string> = ko.observable("");

    public readonly password: ko.Observable<string> = ko.observable("");

    private readonly userApiClient = new UserApiClient();

    public handleRegister() {
        const requestModel: ICreateUserRequest = {
            email: this.email(),
            password: this.password(),
        };

        this.userApiClient.create(requestModel).then((result: IOperationResult<void>) => {
            if (result.status === OperationStatus.Success) {
                alert("Registered successfully");
                RedirectHandler.redirect("/");
            }
        });
    }

    public handleRedirectToHomePage() {
        RedirectHandler.redirect("/");
    }
}