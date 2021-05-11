import template from "./WorkspaceCard.html";
import { WorkspaceCardViewModel } from "./WorkspaceCardViewModel";
import { IComponentDescriptor } from "../../../../../componentsRegistration/IComponentDescriptor";
import { IComponent } from "../../../../../componentsRegistration/IComponent";

export class WorkspaceCardComponent implements IComponent {
    public generateDescriptor(): IComponentDescriptor {
        return { viewModel: WorkspaceCardViewModel, template } as IComponentDescriptor;
    }
}