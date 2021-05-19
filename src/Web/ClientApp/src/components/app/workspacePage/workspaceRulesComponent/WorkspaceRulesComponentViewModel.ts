import * as ko from "knockout";
import { IViewModel } from "src/componentsRegistration/IViewModel";
import { RedirectHandler } from "src/services/RedirectHandler";
import { IWorkspaceRulesComponentParams } from "src/components/app/workspacePage/workspaceRulesComponent/IWorkspaceRulesComponentParams";

export class WorkspaceRulesComponentViewModel implements IViewModel {
    public readonly params: IWorkspaceRulesComponentParams;

    constructor(params: IWorkspaceRulesComponentParams) {
        this.params = params;
    }

    public createRule(): void {
        RedirectHandler.redirect(`${window.location.pathname}/create`);
    }
}