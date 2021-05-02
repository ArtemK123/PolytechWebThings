import * as ko from "knockout";
import {ILoginUserCommand} from "../../models/ILoginUserCommand";
import {UserApiClient} from "../../services/UserApiClient";

export class LoginViewModel {
  public readonly email: ko.Observable<string> = ko.observable("");
  public readonly password: ko.Observable<string> = ko.observable("");

  private readonly userApiClient = new UserApiClient();

  public handleLogin() {
    const requestModel: ILoginUserCommand = {
      email: this.email(),
      password: this.password(),
    };

    this.userApiClient.login(requestModel).then(async response => {
      if (response.status === 200) {
        alert("Login successfully");
        window.location.replace("/");
        return;
      }
      const message = await response.text();
      alert(`Error while logging in: ${message}`);
    });
  }
}