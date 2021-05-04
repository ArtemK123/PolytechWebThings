import template from "./BackendConnectionCheck.html";
import { BackendConnectionCheckViewModel } from "./BackendConnectionCheckViewModel";
import { IComponentDescriptor } from "../../componentsRegistration/IComponentDescriptor";
import { IComponent } from "../../componentsRegistration/IComponent";

export class BackendConnectionCheckComponent implements IComponent {
    public generateDescriptor(): IComponentDescriptor {
        return { viewModel: BackendConnectionCheckViewModel, template } as IComponentDescriptor;
    }
}