import * as ko from "knockout";
import { IViewModel } from "../../componentsRegistration/IViewModel";
import { UserApiClient } from "../../backendApi/clients/UserApiClient";
import { RedirectHandler } from "../../services/RedirectHandler";
import { LogoutUseCase } from "../../useCases/LogoutUseCase";

export class HeaderViewModel implements IViewModel {
    public readonly userAccount: ko.Observable<string> = ko.observable("Unauthorized");

    private readonly userApiClient: UserApiClient = new UserApiClient();
    private readonly logoutUseCase: LogoutUseCase = new LogoutUseCase();

    constructor() {
        this.updateUserAccount();
    }

    public handleRedirectToHome(): void {
        RedirectHandler.redirect("/");
    }

    public handleLogout(): void {
        this.logoutUseCase.execute();
    }

    private updateUserAccount() {
        const email: string | null | undefined = localStorage.getItem("email");
        if (email) {
            this.userAccount(email);
        }
    }
}