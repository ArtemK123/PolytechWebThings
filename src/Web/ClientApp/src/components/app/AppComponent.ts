import template from "./App.html";
import { IComponent } from "../../componentsRegistration/IComponent";
import { IComponentDescriptor } from "../../componentsRegistration/IComponentDescriptor";
import { AppViewModel } from "./AppViewModel";

export class AppComponent implements IComponent {
    generateDescriptor(): IComponentDescriptor {
        return { viewModel: AppViewModel, template };
    }
}