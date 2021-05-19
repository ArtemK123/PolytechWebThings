import { IComponentDescriptor } from "src/componentsRegistration/IComponentDescriptor";
import { IComponent } from "src/componentsRegistration/IComponent";
import { ExecuteRuleStepCardViewModel } from "src/components/app/workspacePage/workspaceRulesComponent/ruleCard/ruleStepCard/ExecuteRuleStepCard/ExecuteRuleStepCardViewModel";
import template from "./ExecuteRuleStepCard.html";
import "./ExecuteRuleStepCard.scss";

export class ExecuteRuleStepCardComponent implements IComponent {
    public generateDescriptor(): IComponentDescriptor {
        return { viewModel: ExecuteRuleStepCardViewModel, template } as IComponentDescriptor;
    }
}