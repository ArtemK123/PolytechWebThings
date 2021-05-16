import { IComponentDescriptor } from "src/componentsRegistration/IComponentDescriptor";
import { IComponent } from "src/componentsRegistration/IComponent";
import { CreateWorkspacePageViewModel } from "src/components/app/createWorkspacePage/CreateWorkspacePageViewModel";
import template from "./CreateWorkspacePage.html";
import "./CreateWorkspacePage.scss";

export class CreateWorkspacePageComponent implements IComponent {
    public generateDescriptor(): IComponentDescriptor {
        return { viewModel: CreateWorkspacePageViewModel, template } as IComponentDescriptor;
    }
}