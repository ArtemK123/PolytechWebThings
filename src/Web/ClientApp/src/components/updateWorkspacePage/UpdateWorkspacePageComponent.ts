import template from "./UpdateWorkspacePage.html";
import { UpdateWorkspacePageViewModel } from "./UpdateWorkspacePageViewModel";
import { IComponentDescriptor } from "../../componentsRegistration/IComponentDescriptor";
import { IComponent } from "../../componentsRegistration/IComponent";
import "./UpdateWorkspacePage.scss";

export class UpdateWorkspacePageComponent implements IComponent {
    public generateDescriptor(): IComponentDescriptor {
        return { viewModel: UpdateWorkspacePageViewModel, template } as IComponentDescriptor;
    }
}