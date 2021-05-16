import { IComponentDescriptor } from "src/componentsRegistration/IComponentDescriptor";
import { IComponent } from "src/componentsRegistration/IComponent";
import { RouterViewModel } from "src/components/common/router/RouterViewModel";
import template from "./Router.html";

export class RouterComponent implements IComponent {
    generateDescriptor(): IComponentDescriptor {
        return { viewModel: RouterViewModel, template };
    }
}