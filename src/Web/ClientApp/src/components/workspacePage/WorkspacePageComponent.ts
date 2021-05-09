import template from "./WorkspacePage.html";
import { WorkspacePageViewModel } from "./WorkspacePageViewModel";
import { IComponentDescriptor } from "../../componentsRegistration/IComponentDescriptor";
import { IComponent } from "../../componentsRegistration/IComponent";

export class WorkspacePageComponent implements IComponent {
    public generateDescriptor(): IComponentDescriptor {
        return { viewModel: WorkspacePageViewModel, template } as IComponentDescriptor;
    }
}