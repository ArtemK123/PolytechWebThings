import * as ko from "knockout";
import { IViewModel } from "src/componentsRegistration/IViewModel";
import { IRuleStepCardParams } from "src/components/app/workspacePage/workspaceRulesComponent/ruleCard/ruleStepCard/IRuleStepCardParams";

export class RuleStepCardViewModel implements IViewModel {
    public readonly params: IRuleStepCardParams;
    public readonly normalizedIndex: ko.Computed<number>;

    constructor(params: IRuleStepCardParams) {
        this.params = params;
        this.normalizedIndex = ko.computed(() => this.params.index() + 1);
    }
}