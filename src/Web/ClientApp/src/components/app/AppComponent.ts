import { IComponentDescriptor } from "src/componentsRegistration/IComponentDescriptor";
import { IComponent } from "src/componentsRegistration/IComponent";
import { AppViewModel } from "src/components/app/AppViewModel";
import template from "./App.html";

export class AppComponent implements IComponent {
    generateDescriptor(): IComponentDescriptor {
        return { viewModel: AppViewModel, template };
    }
}