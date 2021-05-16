import template from "./WorkspaceRulesComponent.html";
import { WorkspaceRulesComponentViewModel } from "./WorkspaceRulesComponentViewModel";
import { IComponentDescriptor } from "../../../../componentsRegistration/IComponentDescriptor";
import { IComponent } from "../../../../componentsRegistration/IComponent";

export class WorkspaceRulesComponent implements IComponent {
    public generateDescriptor(): IComponentDescriptor {
        return { viewModel: WorkspaceRulesComponentViewModel, template } as IComponentDescriptor;
    }
}