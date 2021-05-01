import * as ko from "knockout";

export default class HelloWorldViewModel {
  public greeting: ko.Observable<string>;
  public name: ko.Observable<string>;
  public appHeading: ko.PureComputed<string>;

  constructor() {
    this.greeting = ko.observable<string>("Hello");
    this.name = ko.observable<string>("World");
    this.appHeading = ko.pureComputed<string>(() => this.greeting() + ", " + this.name() + "!");
  }
}