import { IComponentDescriptor } from "src/componentsRegistration/IComponentDescriptor";
import { UpdateWorkspacePageViewModel } from "src/components/app/updateWorkspacePage/UpdateWorkspacePageViewModel";
import { IComponent } from "src/componentsRegistration/IComponent";
import template from "./UpdateWorkspacePage.html";
import "./UpdateWorkspacePage.scss";

export class UpdateWorkspacePageComponent implements IComponent {
    public generateDescriptor(): IComponentDescriptor {
        return { viewModel: UpdateWorkspacePageViewModel, template } as IComponentDescriptor;
    }
}