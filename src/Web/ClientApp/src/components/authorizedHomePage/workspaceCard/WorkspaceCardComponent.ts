import template from "./WorkspaceCard.html";
import { WorkspaceCardViewModel } from "./WorkspaceCardViewModel";
import { IComponent } from "../../../componentsRegistration/IComponent";
import { IComponentDescriptor } from "../../../componentsRegistration/IComponentDescriptor";

export class WorkspaceCardComponent implements IComponent {
    public generateDescriptor(): IComponentDescriptor {
        return { viewModel: WorkspaceCardViewModel, template } as IComponentDescriptor;
    }
}