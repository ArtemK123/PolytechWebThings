import template from "./Login.html";
import {LoginViewModel} from "./LoginViewModel";
import {IComponent} from "../../componentsRegistration/IComponent";
import {IComponentDescriptor} from "../../componentsRegistration/IComponentDescriptor";

export class LoginComponent implements IComponent {
    generateDescriptor(): IComponentDescriptor {
        return { viewModel: LoginViewModel, template: template };
    }
}