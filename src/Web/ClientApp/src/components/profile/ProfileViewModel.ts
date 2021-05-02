import * as ko from "knockout";
import {IViewModel} from "../../componentsRegistration/IViewModel";

export class ProfileViewModel implements IViewModel {
  public authorized: ko.Computed<boolean>;

  constructor() {
    this.authorized = ko.computed<boolean>(this.isAuthorized.bind(this));
  }

  private isAuthorized(): boolean {
    const cookies: { [_: string]: string } = this.getCookies();
    const authCookie: string | undefined = cookies[".AspNetCore.Cookies"];
    return authCookie !== undefined;
  }

  private getCookies(): { [_: string]: string } {
    const allCookies: { [_: string]: string } = {};
    const allCookiesArray: string[] = document.cookie.split('; ');

    allCookiesArray.forEach(cookie => {
      const keyValuePair: string[] = cookie.split("=");
      allCookies[keyValuePair[0]] = keyValuePair[1];
    });
    return allCookies;
  }
}