import template from "./CreateWorkspacePage.html";
import { CreateWorkspacePageViewModel } from "./CreateWorkspacePageViewModel";
import { IComponentDescriptor } from "../../componentsRegistration/IComponentDescriptor";
import { IComponent } from "../../componentsRegistration/IComponent";

export class CreateWorkspacePageComponent implements IComponent {
    public generateDescriptor(): IComponentDescriptor {
        return { viewModel: CreateWorkspacePageViewModel, template } as IComponentDescriptor;
    }
}