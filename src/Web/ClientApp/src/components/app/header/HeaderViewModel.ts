import * as ko from "knockout";
import { RedirectHandler } from "src/services/RedirectHandler";
import { LogoutUseCase } from "src/useCases/LogoutUseCase";
import { IViewModel } from "src/componentsRegistration/IViewModel";
import { UserApiClient } from "src/backendApi/clients/UserApiClient";

export class HeaderViewModel implements IViewModel {
    public readonly userAccount: ko.Observable<string> = ko.observable("");
    public readonly isUserAccountShown: ko.Computed<boolean> = ko.computed(() => this.userAccount() !== "");

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