import { IComponentDescriptor } from "src/componentsRegistration/IComponentDescriptor";
import { IComponent } from "src/componentsRegistration/IComponent";
import { LoginViewModel } from "src/components/app/login/LoginViewModel";
import template from "./Login.html";

export class LoginComponent implements IComponent {
    generateDescriptor(): IComponentDescriptor {
        return { viewModel: LoginViewModel, template };
    }
}