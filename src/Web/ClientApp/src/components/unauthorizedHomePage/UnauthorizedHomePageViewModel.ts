import { IViewModel } from "../../componentsRegistration/IViewModel";
import { RedirectHandler } from "../../services/RedirectHandler";

export class UnauthorizedHomePageViewModel implements IViewModel {
    public handleSignIn(): void {
        RedirectHandler.redirect("/login");
    }

    public handleSignUp(): void {
        RedirectHandler.redirect("/register");
    }
}