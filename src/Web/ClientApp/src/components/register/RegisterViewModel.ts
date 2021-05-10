import * as ko from "knockout";
import { IViewModel } from "../../componentsRegistration/IViewModel";
import { RedirectHandler } from "../../services/RedirectHandler";
import { UserApiClient } from "../../backendApi/clients/UserApiClient";
import { ICreateUserRequest } from "../../backendApi/models/request/user/ICreateUserRequest";

export class RegisterViewModel implements IViewModel {
    public readonly email: ko.Observable<string> = ko.observable("");

    public readonly password: ko.Observable<string> = ko.observable("");

    private readonly userApiClient = new UserApiClient();

    public handleRegister() {
        const requestModel: ICreateUserRequest = {
            email: this.email(),
            password: this.password(),
        };

        this.userApiClient.create(requestModel).then(async (response) => {
            if (response.status === 200) {
                alert("Registered successfully");
                RedirectHandler.redirect("/");
                return;
            }
            const message = await response.text();
            alert(`Error while registering: ${message}`);
        });
    }

    public handleRedirectToHomePage() {
        RedirectHandler.redirect("/");
    }
}