import * as ko from "knockout";
import {ICreateUserCommand} from "../../models/ICreateUserCommand";
import {UserRole} from "../../models/UserRole";
import {UserApiClient} from "../../services/UserApiClient";
import {IViewModel} from "../../componentsRegistration/IViewModel";

export class RegisterViewModel implements IViewModel {
  public readonly email: ko.Observable<string> = ko.observable("");
  public readonly password: ko.Observable<string> = ko.observable("");

  private readonly userApiClient = new UserApiClient();

  public handleRegister() {
    const requestModel: ICreateUserCommand = {
      email: this.email(),
      password: this.password(),
      role: UserRole.User
    };

    this.userApiClient.create(requestModel).then(async response => {
      if (response.status === 200) {
        alert("Registered successfully");
        return;
      }
      const message = await response.text();
      alert(`Error while registering: ${message}`);
    });
  }
}