import { IComponentDescriptor } from "src/componentsRegistration/IComponentDescriptor";
import { IComponent } from "src/componentsRegistration/IComponent";
import { ChangeThingStateStepCardViewModel } from "src/components/app/workspacePage/workspaceRulesComponent/ruleCard/ruleStepCard/ChangeThingStateStepCard/ChangeThingStateStepCardViewModel";
import template from "./ChangeThingStateStepCard.html";
import "./ChangeThingStateStepCard.scss";

export class ChangeThingStateStepCardComponent implements IComponent {
    public generateDescriptor(): IComponentDescriptor {
        return { viewModel: ChangeThingStateStepCardViewModel, template } as IComponentDescriptor;
    }
}