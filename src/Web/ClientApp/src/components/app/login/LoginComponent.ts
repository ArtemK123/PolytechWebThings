import template from "./Login.html";
import { LoginViewModel } from "./LoginViewModel";
import { IComponentDescriptor } from "../../../componentsRegistration/IComponentDescriptor";
import { IComponent } from "../../../componentsRegistration/IComponent";

export class LoginComponent implements IComponent {
    generateDescriptor(): IComponentDescriptor {
        return { viewModel: LoginViewModel, template };
    }
}