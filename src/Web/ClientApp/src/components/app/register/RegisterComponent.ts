import template from "./Register.html";
import { RegisterViewModel } from "./RegisterViewModel";
import { IComponentDescriptor } from "../../../componentsRegistration/IComponentDescriptor";
import { IComponent } from "../../../componentsRegistration/IComponent";

export class RegisterComponent implements IComponent {
    generateDescriptor(): IComponentDescriptor {
        return { viewModel: RegisterViewModel, template };
    }
}