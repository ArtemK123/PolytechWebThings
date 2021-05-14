import template from "./CreateRuleModal.html";
import { IComponentDescriptor } from "../../../../componentsRegistration/IComponentDescriptor";
import { IComponent } from "../../../../componentsRegistration/IComponent";
import { CreateRuleModalViewModel } from "./CreateRuleViewModel";

export class CreateRuleModalComponent implements IComponent {
    public generateDescriptor(): IComponentDescriptor {
        return { viewModel: CreateRuleModalViewModel, template } as IComponentDescriptor;
    }
}