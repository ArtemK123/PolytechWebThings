import { IComponentDescriptor } from "src/componentsRegistration/IComponentDescriptor";
import { IComponent } from "src/componentsRegistration/IComponent";
import { RegisterViewModel } from "src/components/app/register/RegisterViewModel";
import template from "./Register.html";
import "./Register.scss";

export class RegisterComponent implements IComponent {
    generateDescriptor(): IComponentDescriptor {
        return { viewModel: RegisterViewModel, template };
    }
}