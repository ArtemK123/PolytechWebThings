import * as ko from "knockout";
import { IViewModel } from "../../componentsRegistration/IViewModel";

export class AuthorizedHomePageViewModel implements IViewModel {
    public readonly email: ko.Observable<string> = ko.observable<string>("");

    constructor() {
        const storedEmail: string | null | undefined = localStorage.getItem("email");
        if (!storedEmail) {
            throw new Error("User email is not found");
        }

        this.email(storedEmail);
    }
}