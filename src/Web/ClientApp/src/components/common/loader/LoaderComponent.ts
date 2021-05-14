import template from "./Loader.html";
import "./Loader.scss";
import { IComponentDescriptor } from "../../../componentsRegistration/IComponentDescriptor";
import { IComponent } from "../../../componentsRegistration/IComponent";
import { LoaderViewModel } from "./LoaderViewModel";

export class LoaderComponent implements IComponent {
    generateDescriptor(): IComponentDescriptor {
        return { viewModel: LoaderViewModel, template };
    }
}