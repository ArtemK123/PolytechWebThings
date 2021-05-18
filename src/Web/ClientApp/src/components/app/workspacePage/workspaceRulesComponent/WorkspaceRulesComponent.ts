import { IComponentDescriptor } from "src/componentsRegistration/IComponentDescriptor";
import { IComponent } from "src/componentsRegistration/IComponent";
import { WorkspaceRulesComponentViewModel } from "src/components/app/workspacePage/workspaceRulesComponent/WorkspaceRulesComponentViewModel";
import template from "./WorkspaceRulesComponent.html";
import "./WorkspaceRulesComponent.scss";

export class WorkspaceRulesComponent implements IComponent {
    public generateDescriptor(): IComponentDescriptor {
        return { viewModel: WorkspaceRulesComponentViewModel, template } as IComponentDescriptor;
    }
}