import {UserApiClient} from "../../services/UserApiClient";
import {IViewModel} from "../../componentsRegistration/IViewModel";

export class LogoutViewModel implements IViewModel{
  private readonly userApiClient = new UserApiClient();

  public handleLogout() {
    this.userApiClient.logout().then(async response => {
      if (response.status === 200) {
        alert("Logout successfully");
        window.location.replace("/");
        return;
      }
      const message = await response.text();
      alert(`Error while logging out: ${message}`);
    });
  }
}