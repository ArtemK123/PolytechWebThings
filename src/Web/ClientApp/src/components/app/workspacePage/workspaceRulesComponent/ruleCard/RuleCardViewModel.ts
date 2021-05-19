import * as ko from "knockout";
import { IViewModel } from "src/componentsRegistration/IViewModel";
import { IRuleCardParams } from "src/components/app/workspacePage/workspaceRulesComponent/ruleCard/IRuleCardParams";

export class RuleCardViewModel implements IViewModel {
    public readonly isCollapsed: ko.Observable<boolean> = ko.observable(true);
    public readonly params: IRuleCardParams;

    constructor(params: IRuleCardParams) {
        this.params = params;
    }

    public changeCollapseState(): void {
        this.isCollapsed(!this.isCollapsed());
    }

    public executeRule(): void {
        console.log("executeRule");
    }

    public updateRule(): void {
        console.log("updateRule");
    }

    public deleteRule(): void {
        console.log("deleteRule");
    }
}