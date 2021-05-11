import * as ko from "knockout";
import { IViewModel } from "../../../componentsRegistration/IViewModel";

export class HomePageViewModel implements IViewModel {
    public readonly authorizedUser: ko.Observable<boolean> = ko.observable(false);

    constructor() {
        const email: string | null | undefined = localStorage.getItem("email");
        if (email) {
            this.authorizedUser(true);
        }
    }
}