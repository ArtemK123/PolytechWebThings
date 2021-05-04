import * as ko from "knockout";
import { UserApiClient } from "../../services/UserApiClient";
import { IViewModel } from "../../componentsRegistration/IViewModel";
import { ICreateUserRequest } from "../../models/request/ICreateUserRequest";

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
                // eslint-disable-next-line no-alert
                alert("Registered successfully");
                return;
            }
            const message = await response.text();
            // eslint-disable-next-line no-alert
            alert(`Error while registering: ${message}`);
        });
    }
}