import * as ko from "knockout";
import {ICreateUserCommand} from "../../models/ICreateUserCommand";
import {UserRole} from "../../models/UserRole";

export class RegisterViewModel {
  public email: ko.Observable<string> = ko.observable("");
  public password: ko.Observable<string> = ko.observable("");

  constructor() {
  }

  public handleRegister() {
    const requestModel: ICreateUserCommand = {
      email: this.email(),
      password: this.password(),
      role: UserRole.User
    };

    fetch("api/UserApi/Create", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(requestModel)}
    ).then(async response => {
      if (response.status === 200) {
        alert("Registered successfully");
        return;
      }
      const message = await response.text();
      alert(`Error while registering: ${message}`);
    });
  }
}