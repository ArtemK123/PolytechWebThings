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

    public shouldInsertCardSeparator(index: ko.Observable<number>): boolean {
        return index() < this.params.rule.steps.length - 1;
    }

    public executeRule(): void {
        console.log("executeRule");
    }

    public editRule(): void {
        this.params.editRuleAction(this.params.rule);
    }

    public deleteRule(): void {
        this.params.deleteRuleAction(this.params.rule);
    }
}