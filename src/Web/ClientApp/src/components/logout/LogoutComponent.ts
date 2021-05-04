import template from "./Logout.html";
import { LogoutViewModel } from "./LogoutViewModel";
import { IComponent } from "../../componentsRegistration/IComponent";
import { IComponentDescriptor } from "../../componentsRegistration/IComponentDescriptor";

export class LogoutComponent implements IComponent {
    generateDescriptor(): IComponentDescriptor {
        return { viewModel: LogoutViewModel, template };
    }
}