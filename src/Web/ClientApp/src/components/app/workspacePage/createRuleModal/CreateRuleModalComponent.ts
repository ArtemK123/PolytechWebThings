import { IComponentDescriptor } from "src/componentsRegistration/IComponentDescriptor";
import { IComponent } from "src/componentsRegistration/IComponent";
import { CreateRuleModalViewModel } from "src/components/app/workspacePage/createRuleModal/CreateRuleModalViewModel";
import template from "./CreateRuleModal.html";
import "./CreateRuleModal.scss";

export class CreateRuleModalComponent implements IComponent {
    public generateDescriptor(): IComponentDescriptor {
        return { viewModel: CreateRuleModalViewModel, template } as IComponentDescriptor;
    }
}