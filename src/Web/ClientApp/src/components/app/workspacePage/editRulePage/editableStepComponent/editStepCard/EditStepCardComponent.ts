import { IComponentDescriptor } from "src/componentsRegistration/IComponentDescriptor";
import { IComponent } from "src/componentsRegistration/IComponent";
import { EditStepCardViewModel } from "src/components/app/workspacePage/editRulePage/editableStepComponent/editStepCard/EditStepCardViewModel";
import template from "./EditStepCard.html";
import "./EditStepCard.scss";

export class EditStepCardComponent implements IComponent {
    public generateDescriptor(): IComponentDescriptor {
        return { viewModel: EditStepCardViewModel, template } as IComponentDescriptor;
    }
}