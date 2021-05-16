import { RedirectHandler } from "src/services/RedirectHandler";
import { IViewModel } from "src/componentsRegistration/IViewModel";

export class UnauthorizedHomePageViewModel implements IViewModel {
    public handleSignIn(): void {
        RedirectHandler.redirect("/login");
    }

    public handleSignUp(): void {
        RedirectHandler.redirect("/register");
    }
}