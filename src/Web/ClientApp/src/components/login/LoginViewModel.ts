import * as ko from "knockout";
import { ILoginUserCommand } from "../../models/ILoginUserCommand";

export class LoginViewModel {
  public email: ko.Observable<string> = ko.observable("");
  public password: ko.Observable<string> = ko.observable("");

  constructor() {
  }

  public handleLogin() {
    const requestModel: ILoginUserCommand = {
      email: this.email(),
      password: this.password(),
    };

    fetch("api/UserApi/Login", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(requestModel)}
    ).then(async response => {
      if (response.status === 200) {
        alert("Login successfully");
        return;
      }
      const message = await response.text();
      alert(`Error while logging in: ${message}`);
    });
  }
}