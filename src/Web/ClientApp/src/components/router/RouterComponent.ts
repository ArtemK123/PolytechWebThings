import template from "./Router.html";
import { RouterViewModel } from "./RouterViewModel";
import { IComponent } from "../../componentsRegistration/IComponent";
import { IComponentDescriptor } from "../../componentsRegistration/IComponentDescriptor";

export class RouterComponent implements IComponent {
    generateDescriptor(): IComponentDescriptor {
        return { viewModel: RouterViewModel, template };
    }
}