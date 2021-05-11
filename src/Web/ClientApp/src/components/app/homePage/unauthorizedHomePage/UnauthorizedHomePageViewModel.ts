import { RedirectHandler } from "../../../../services/RedirectHandler";
import { IViewModel } from "../../../../componentsRegistration/IViewModel";

export class UnauthorizedHomePageViewModel implements IViewModel {
    public handleSignIn(): void {
        RedirectHandler.redirect("/login");
    }

    public handleSignUp(): void {
        RedirectHandler.redirect("/register");
    }
}