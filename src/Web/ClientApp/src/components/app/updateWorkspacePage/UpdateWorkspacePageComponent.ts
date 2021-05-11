import template from "./UpdateWorkspacePage.html";
import { UpdateWorkspacePageViewModel } from "./UpdateWorkspacePageViewModel";
import "./UpdateWorkspacePage.scss";
import { IComponentDescriptor } from "../../../componentsRegistration/IComponentDescriptor";
import { IComponent } from "../../../componentsRegistration/IComponent";

export class UpdateWorkspacePageComponent implements IComponent {
    public generateDescriptor(): IComponentDescriptor {
        return { viewModel: UpdateWorkspacePageViewModel, template } as IComponentDescriptor;
    }
}