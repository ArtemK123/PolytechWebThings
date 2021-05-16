import { IComponentDescriptor } from "src/componentsRegistration/IComponentDescriptor";
import { WorkspacePageViewModel } from "src/components/app/workspacePage/WorkspacePageViewModel";
import { IComponent } from "src/componentsRegistration/IComponent";
import template from "./WorkspacePage.html";

export class WorkspacePageComponent implements IComponent {
    public generateDescriptor(): IComponentDescriptor {
        return { viewModel: WorkspacePageViewModel, template } as IComponentDescriptor;
    }
}