import { IViewModel } from "../../componentsRegistration/IViewModel";
import { UserApiClient } from "../../backendApi/clients/UserApiClient";

export class LogoutViewModel implements IViewModel {
    private readonly userApiClient = new UserApiClient();

    public handleLogout() {
        this.userApiClient.logout().then(async (response) => {
            if (response.status === 200) {
                // eslint-disable-next-line no-alert
                alert("Logout successfully");
                window.location.replace("/");
                return;
            }
            const message = await response.text();
            // eslint-disable-next-line no-alert
            alert(`Error while logging out: ${message}`);
        });
    }
}