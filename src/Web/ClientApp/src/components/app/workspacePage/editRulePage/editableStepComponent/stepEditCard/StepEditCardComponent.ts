import { IComponentDescriptor } from "src/componentsRegistration/IComponentDescriptor";
import { IComponent } from "src/componentsRegistration/IComponent";
import { StepEditCardViewModel } from "src/components/app/workspacePage/editRulePage/editableStepComponent/stepEditCard/StepEditCardViewModel";
import template from "./StepEditCard.html";
import "./StepEditCard.scss";

export class StepEditCardComponent implements IComponent {
    public generateDescriptor(): IComponentDescriptor {
        return { viewModel: StepEditCardViewModel, template } as IComponentDescriptor;
    }
}