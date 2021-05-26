import * as ko from "knockout";
import { RedirectHandler } from "src/services/RedirectHandler";
import { OperationStatus } from "src/backendApi/models/entities/OperationResult/OperationStatus";
import { IViewModel } from "src/componentsRegistration/IViewModel";
import { UserApiClient } from "src/backendApi/clients/UserApiClient";
import { IOperationResult } from "src/backendApi/models/entities/OperationResult/IOperationResult";
import { ILoginUserRequest } from "src/backendApi/models/request/user/ILoginUserRequest";

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

    public handleRedirectToHome(): void {
        LoginViewModel.redirectToHomePage();
    }

    public handleRedirectToSignUp(): void {
        RedirectHandler.redirect("/register");
    }

    private static redirectToHomePage(): void {
        RedirectHandler.redirect("/");
    }
}