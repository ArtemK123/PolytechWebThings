import * as ko from "knockout";
import { IViewModel } from "../../componentsRegistration/IViewModel";
import { UserApiClient } from "../../backendApi/clients/UserApiClient";
import { RedirectHandler } from "../../services/RedirectHandler";

export class HeaderViewModel implements IViewModel {
    public readonly userAccount: ko.Observable<string> = ko.observable("Unauthorized");

    private readonly userApiClient: UserApiClient = new UserApiClient();

    constructor() {
        this.updateUserAccount();
    }

    public handleRedirectToHome(): void {
        RedirectHandler.redirect("/");
    }

    public handleLogout(): void {
        this.userApiClient
            .logout()
            .then(() => {
                localStorage.removeItem("email");
                RedirectHandler.redirect("/");
            });
    }

    private updateUserAccount() {
        const email: string | null | undefined = localStorage.getItem("email");
        if (email) {
            this.userAccount(email);
        }
    }
}