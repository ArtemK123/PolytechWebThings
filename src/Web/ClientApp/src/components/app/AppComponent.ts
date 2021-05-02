import template from "./App.html";
import AppViewModel from "./AppViewModel";
import {IComponent} from "../../componentsRegistration/IComponent";
import {IComponentDescriptor} from "../../componentsRegistration/IComponentDescriptor";

export class AppComponent implements IComponent {
    generateDescriptor(): IComponentDescriptor {
        return { viewModel: AppViewModel, template: template };
    }
}
