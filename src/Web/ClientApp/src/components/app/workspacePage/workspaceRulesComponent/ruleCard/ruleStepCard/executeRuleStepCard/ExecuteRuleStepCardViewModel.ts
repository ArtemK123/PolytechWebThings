import * as ko from "knockout";
import { IViewModel } from "src/componentsRegistration/IViewModel";
import { IExecuteRuleStepCardParams } from "src/components/app/workspacePage/workspaceRulesComponent/ruleCard/ruleStepCard/ExecuteRuleStepCard/IExecuteRuleStepCardParams";

export class ExecuteRuleStepCardViewModel implements IViewModel {
    public readonly params: IExecuteRuleStepCardParams;
    public readonly titleText: ko.Computed<string>;

    constructor(params: IExecuteRuleStepCardParams) {
        this.params = params;
        this.titleText = ko.computed(() => `${params.index()}. Execute rule`);
    }
}