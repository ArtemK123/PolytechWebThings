import { IComponentDescriptor } from "src/componentsRegistration/IComponentDescriptor";
import { IComponent } from "src/componentsRegistration/IComponent";
import { EditRulePageViewModel } from "src/components/app/workspacePage/EditRulePage/EditRulePageViewModel";
import template from "./EditRulePage.html";
import "./EditRulePage.scss";

export class EditRulePageComponent implements IComponent {
    public generateDescriptor(): IComponentDescriptor {
        return { viewModel: EditRulePageViewModel, template } as IComponentDescriptor;
    }
}