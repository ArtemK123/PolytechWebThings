import * as ko from "knockout";
import { IViewModel } from "../../componentsRegistration/IViewModel";
import { RedirectHandler } from "../../services/RedirectHandler";
import { ILoginUserRequest } from "../../backendApi/models/request/user/ILoginUserRequest";
import { UserApiClient } from "../../backendApi/clients/UserApiClient";

export class LoginViewModel implements IViewModel {
    public readonly email: ko.Observable<string> = ko.observable("");

    public readonly password: ko.Observable<string> = ko.observable("");

    private readonly userApiClient = new UserApiClient();

    public handleLogin() {
        const requestModel: ILoginUserRequest = {
            email: this.email(),
            password: this.password(),
        };

        this.userApiClient.login(requestModel).then(async (response) => {
            if (response.status === 200) {
                alert("Login successfully");
                localStorage.setItem("email", requestModel.email);
                LoginViewModel.redirectToHomePage();
                return;
            }
            const message = await response.text();
            alert(`Error while logging in: ${message}`);
        });
    }

    public handleRedirectToHomePage() {
        LoginViewModel.redirectToHomePage();
    }

    private static redirectToHomePage(): void {
        RedirectHandler.redirect("/");
    }
}