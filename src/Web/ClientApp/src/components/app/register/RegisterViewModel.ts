import * as ko from "knockout";
import { ICreateUserRequest } from "src/backendApi/models/request/user/ICreateUserRequest";
import { RedirectHandler } from "src/services/RedirectHandler";
import { OperationStatus } from "src/backendApi/models/response/OperationResult/OperationStatus";
import { IViewModel } from "src/componentsRegistration/IViewModel";
import { UserApiClient } from "src/backendApi/clients/UserApiClient";
import { IOperationResult } from "src/backendApi/models/response/OperationResult/IOperationResult";

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