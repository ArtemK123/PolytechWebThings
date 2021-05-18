import { IComponentDescriptor } from "src/componentsRegistration/IComponentDescriptor";
import { IComponent } from "src/componentsRegistration/IComponent";
import { RuleCardViewModel } from "src/components/app/workspacePage/workspaceRulesComponent/RuleCard/RuleCardViewModel";
import template from "./RuleCard.html";
import "./RuleCard.scss";

export class RuleCardComponent implements IComponent {
    public generateDescriptor(): IComponentDescriptor {
        return { viewModel: RuleCardViewModel, template } as IComponentDescriptor;
    }
}