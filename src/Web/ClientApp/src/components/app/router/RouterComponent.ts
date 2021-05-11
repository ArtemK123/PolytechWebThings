import template from "./Router.html";
import { RouterViewModel } from "./RouterViewModel";
import { IComponentDescriptor } from "../../../componentsRegistration/IComponentDescriptor";
import { IComponent } from "../../../componentsRegistration/IComponent";

export class RouterComponent implements IComponent {
    generateDescriptor(): IComponentDescriptor {
        return { viewModel: RouterViewModel, template };
    }
}