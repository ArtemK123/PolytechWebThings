import template from "./BackendConnectionCheck.html";
import { BackendConnectionCheckViewModel } from "./BackendConnectionCheckViewModel";
import { IComponent } from "../../../componentsRegistration/IComponent";
import { IComponentDescriptor } from "../../../componentsRegistration/IComponentDescriptor";

export class BackendConnectionCheckComponent implements IComponent {
    public generateDescriptor(): IComponentDescriptor {
        return { viewModel: BackendConnectionCheckViewModel, template } as IComponentDescriptor;
    }
}