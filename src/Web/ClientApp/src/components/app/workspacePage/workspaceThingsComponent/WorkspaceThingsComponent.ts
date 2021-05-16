import template from "./WorkspaceThingsComponent.html";
import { WorkspaceThingsComponentViewModel } from "./WorkspaceThingsComponentViewModel";
import { IComponentDescriptor } from "../../../../componentsRegistration/IComponentDescriptor";
import { IComponent } from "../../../../componentsRegistration/IComponent";

export class WorkspaceThingsComponent implements IComponent {
    public generateDescriptor(): IComponentDescriptor {
        return { viewModel: WorkspaceThingsComponentViewModel, template } as IComponentDescriptor;
    }
}