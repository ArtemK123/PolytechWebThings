import { IComponentDescriptor } from "src/componentsRegistration/IComponentDescriptor";
import { IComponent } from "src/componentsRegistration/IComponent";
import template from "./WorkspaceCard.html";
import { WorkspaceCardViewModel } from "./WorkspaceCardViewModel";

export class WorkspaceCardComponent implements IComponent {
    public generateDescriptor(): IComponentDescriptor {
        return { viewModel: WorkspaceCardViewModel, template } as IComponentDescriptor;
    }
}