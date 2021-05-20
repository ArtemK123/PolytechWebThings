import { IComponentDescriptor } from "src/componentsRegistration/IComponentDescriptor";
import { IComponent } from "src/componentsRegistration/IComponent";
import { ExecuteRuleStepEditCardViewModel }
    from "src/components/app/workspacePage/editRulePage/editableStepComponent/ExecuteRuleStepEditCard/ExecuteRuleStepEditCardViewModel";
import template from "./ExecuteRuleStepEditCard.html";
import "./ExecuteRuleStepEditCard.scss";

export class ExecuteRuleStepEditCardComponent implements IComponent {
    public generateDescriptor(): IComponentDescriptor {
        return { viewModel: ExecuteRuleStepEditCardViewModel, template } as IComponentDescriptor;
    }
}