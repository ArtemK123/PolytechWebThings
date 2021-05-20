import { IComponentDescriptor } from "src/componentsRegistration/IComponentDescriptor";
import { IComponent } from "src/componentsRegistration/IComponent";
import { EditableStepComponentViewModel } from "src/components/app/workspacePage/editRulePage/EditableStepComponent/EditableStepComponentViewModel";
import template from "./EditableStepComponent.html";
import "./EditableStepComponent.scss";

export class EditableStepComponentComponent implements IComponent {
    public generateDescriptor(): IComponentDescriptor {
        return { viewModel: EditableStepComponentViewModel, template } as IComponentDescriptor;
    }
}