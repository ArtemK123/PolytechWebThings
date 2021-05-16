import { IComponentDescriptor } from "src/componentsRegistration/IComponentDescriptor";
import { IComponent } from "src/componentsRegistration/IComponent";
import { LoaderViewModel } from "src/components/common/loader/LoaderViewModel";
import template from "./Loader.html";
import "./Loader.scss";

export class LoaderComponent implements IComponent {
    generateDescriptor(): IComponentDescriptor {
        return { viewModel: LoaderViewModel, template };
    }
}