import { UserApiClient } from "../backendApi/clients/UserApiClient";

export class LogoutUseCase {
    private readonly userApiClient = new UserApiClient();

    public execute(): Promise<void> {
        return this.userApiClient.logout().then(() => {
            alert("Logout successfully");
            localStorage.removeItem("email");
            window.location.replace("/login");
        });
    }
}