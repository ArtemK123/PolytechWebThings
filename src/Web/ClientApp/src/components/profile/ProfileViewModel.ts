import * as ko from "knockout";

export class ProfileViewModel {
  public email: ko.Observable<string> = ko.observable<string>("");
  public authorized: ko.Computed<boolean>;

  constructor() {
    this.initializeEmail();
    this.authorized = ko.computed<boolean>(() => Boolean(this.email()));
  }

  private initializeEmail() {
    const storedEmail: string | undefined = localStorage.getItem("userEmail");
    if (!storedEmail) {
      this.email("");
    }
    this.email(storedEmail);
  }
}