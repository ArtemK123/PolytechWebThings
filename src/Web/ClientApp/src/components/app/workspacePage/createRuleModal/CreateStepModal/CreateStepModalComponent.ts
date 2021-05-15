import template from "./CreateStepModal.html";
import { IComponent } from "../../../../../componentsRegistration/IComponent";
import { IComponentDescriptor } from "../../../../../componentsRegistration/IComponentDescriptor";
import { CreateStepModalViewModel } from "./CreateStepModalViewModel";

export class CreateStepModalComponent implements IComponent {
    public generateDescriptor(): IComponentDescriptor {
        return { viewModel: CreateStepModalViewModel, template } as IComponentDescriptor;
    }
}