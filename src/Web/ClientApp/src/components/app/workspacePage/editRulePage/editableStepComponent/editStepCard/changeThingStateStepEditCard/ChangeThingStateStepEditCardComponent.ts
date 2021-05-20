import { IComponentDescriptor } from "src/componentsRegistration/IComponentDescriptor";
import { IComponent } from "src/componentsRegistration/IComponent";
import { ChangeThingStateStepEditCardViewModel }
    from "src/components/app/workspacePage/editRulePage/editableStepComponent/editStepCard/changeThingStateStepEditCard/ChangeThingStateStepEditCardViewModel";
import template from "./ChangeThingStateStepEditCard.html";
import "./ChangeThingStateStepEditCard.scss";

export class ChangeThingStateStepEditCardComponent implements IComponent {
    public generateDescriptor(): IComponentDescriptor {
        return { viewModel: ChangeThingStateStepEditCardViewModel, template } as IComponentDescriptor;
    }
}