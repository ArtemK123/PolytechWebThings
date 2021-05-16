import { IComponentDescriptor } from "src/componentsRegistration/IComponentDescriptor";
import { IComponent } from "src/componentsRegistration/IComponent";
import { BackendConnectionCheckViewModel } from "src/components/app/backendConnectionCheck/BackendConnectionCheckViewModel";
import template from "./BackendConnectionCheck.html";

export class BackendConnectionCheckComponent implements IComponent {
    public generateDescriptor(): IComponentDescriptor {
        return { viewModel: BackendConnectionCheckViewModel, template } as IComponentDescriptor;
    }
}