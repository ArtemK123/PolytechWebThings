import template from "./Register.html";
import {RegisterViewModel} from "./RegisterViewModel";
import {IComponent} from "../../componentsRegistration/IComponent";
import {IComponentDescriptor} from "../../componentsRegistration/IComponentDescriptor";

export class RegisterComponent implements IComponent {
    generateDescriptor(): IComponentDescriptor {
        return { viewModel: RegisterViewModel, template: template };
    }

}