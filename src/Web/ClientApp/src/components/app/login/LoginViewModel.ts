import * as ko from "knockout";
import { RedirectHandler } from "../../../services/RedirectHandler";
import { OperationStatus } from "../../../backendApi/models/response/OperationResult/OperationStatus";
import { IViewModel } from "../../../componentsRegistration/IViewModel";
import { UserApiClient } from "../../../backendApi/clients/UserApiClient";
import { IOperationResult } from "../../../backendApi/models/response/OperationResult/IOperationResult";
import { ILoginUserRequest } from "../../../backendApi/models/request/user/ILoginUserRequest";

export class LoginViewModel implements IViewModel {
    public readonly email: ko.Observable<string> = ko.observable("");

    public readonly password: ko.Observable<string> = ko.observable("");

    private readonly userApiClient = new UserApiClient();

    public handleLogin() {
        const requestModel: ILoginUserRequest = {
            email: this.email(),
            password: this.password(),
        };

        this.userApiClient.login(requestModel).then((result: IOperationResult<void>) => {
            if (result.status === OperationStatus.Success) {
                alert("Login successfully");
                localStorage.setItem("email", requestModel.email);
                LoginViewModel.redirectToHomePage();
            }
        });
    }

    public handleRedirectToHomePage() {
        LoginViewModel.redirectToHomePage();
    }

    private static redirectToHomePage(): void {
        RedirectHandler.redirect("/");
    }
}