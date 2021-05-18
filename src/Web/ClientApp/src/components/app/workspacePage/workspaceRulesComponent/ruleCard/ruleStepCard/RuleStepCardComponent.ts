import { IComponentDescriptor } from "src/componentsRegistration/IComponentDescriptor";
import { IComponent } from "src/componentsRegistration/IComponent";
import { RuleStepCardViewModel } from "src/components/app/workspacePage/workspaceRulesComponent/ruleCard/ruleStepCard/RuleStepCardViewModel";
import template from "./RuleStepCard.html";
import "./RuleStepCard.scss";

export class RuleStepCardComponent implements IComponent {
    public generateDescriptor(): IComponentDescriptor {
        return { viewModel: RuleStepCardViewModel, template } as IComponentDescriptor;
    }
}