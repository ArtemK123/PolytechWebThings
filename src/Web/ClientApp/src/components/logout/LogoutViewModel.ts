import { IViewModel } from "../../componentsRegistration/IViewModel";
import { LogoutUseCase } from "../../useCases/LogoutUseCase";

export class LogoutViewModel implements IViewModel {
    private readonly logoutUseCase: LogoutUseCase = new LogoutUseCase();

    public handleLogout() {
        this.logoutUseCase.execute();
    }
}