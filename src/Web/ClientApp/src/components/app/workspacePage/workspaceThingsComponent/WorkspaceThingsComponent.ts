import { IComponentDescriptor } from "src/componentsRegistration/IComponentDescriptor";
import { WorkspaceThingsComponentViewModel } from "src/components/app/workspacePage/workspaceThingsComponent/WorkspaceThingsComponentViewModel";
import { IComponent } from "src/componentsRegistration/IComponent";
import template from "./WorkspaceThingsComponent.html";

export class WorkspaceThingsComponent implements IComponent {
    public generateDescriptor(): IComponentDescriptor {
        return { viewModel: WorkspaceThingsComponentViewModel, template } as IComponentDescriptor;
    }
}