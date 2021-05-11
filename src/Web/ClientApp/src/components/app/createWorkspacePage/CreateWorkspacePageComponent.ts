import template from "./CreateWorkspacePage.html";
import { CreateWorkspacePageViewModel } from "./CreateWorkspacePageViewModel";
import "./CreateWorkspacePage.scss";
import { IComponent } from "../../../componentsRegistration/IComponent";
import { IComponentDescriptor } from "../../../componentsRegistration/IComponentDescriptor";

export class CreateWorkspacePageComponent implements IComponent {
    public generateDescriptor(): IComponentDescriptor {
        return { viewModel: CreateWorkspacePageViewModel, template } as IComponentDescriptor;
    }
}