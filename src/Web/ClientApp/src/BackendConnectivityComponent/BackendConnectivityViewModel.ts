import * as ko from "knockout";

export default class BackendConnectivityViewModel {
  public backendMessage: KnockoutObservable<string>;
  public firstName: KnockoutObservable<string>;
  public lastName: KnockoutObservable<string>;

  constructor() {
    this.backendMessage = ko.observable<string>("");
    this.firstName = ko.observable<string>("");
    this.lastName = ko.observable<string>("");

    fetch("http://localhost:5000/api/TestApi/GetMessage", { method: "GET"} )
        .then(response => response.text())
        .then(text =>  this.backendMessage(text));
  }

  public sendForm(): void {
    const requestModel: IFormModel = {
      firstName: this.firstName(),
      lastName: this.lastName()
    } as IFormModel;

    fetch("http://localhost:5000/api/TestApi/SendForm", {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify(requestModel)
    }).then(response => console.log(`Form is sent. Response is ${response.status} ${response.statusText}`));
  }
}