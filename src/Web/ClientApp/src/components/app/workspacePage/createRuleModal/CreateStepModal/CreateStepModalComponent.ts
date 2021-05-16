import { IComponentDescriptor } from "src/componentsRegistration/IComponentDescriptor";
import { IComponent } from "src/componentsRegistration/IComponent";
import { CreateStepModalViewModel } from "src/components/app/workspacePage/createRuleModal/CreateStepModal/CreateStepModalViewModel";
import template from "./CreateStepModal.html";

export class CreateStepModalComponent implements IComponent {
    public generateDescriptor(): IComponentDescriptor {
        return { viewModel: CreateStepModalViewModel, template } as IComponentDescriptor;
    }
}