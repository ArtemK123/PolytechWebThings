import { IComponentDescriptor } from "src/componentsRegistration/IComponentDescriptor";
import { IComponent } from "src/componentsRegistration/IComponent";
import { WorkspaceCardViewModel } from "./WorkspaceCardViewModel";
import template from "./WorkspaceCard.html";
import "./WorkspaceCard.scss";

export class WorkspaceCardComponent implements IComponent {
    public generateDescriptor(): IComponentDescriptor {
        return { viewModel: WorkspaceCardViewModel, template } as IComponentDescriptor;
    }
}