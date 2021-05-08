import * as ko from "knockout";
import { UserApiClient } from "../../services/UserApiClient";
import { IViewModel } from "../../componentsRegistration/IViewModel";
import { ILoginUserRequest } from "../../models/request/ILoginUserRequest";
import { RedirectHandler } from "../../services/RedirectHandler";

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
                // eslint-disable-next-line no-alert
                alert("Login successfully");
                LoginViewModel.redirectToHomePage();
                return;
            }
            const message = await response.text();
            // eslint-disable-next-line no-alert
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