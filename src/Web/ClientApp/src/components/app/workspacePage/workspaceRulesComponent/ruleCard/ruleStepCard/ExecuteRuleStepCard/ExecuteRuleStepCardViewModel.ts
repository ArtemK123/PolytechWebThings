import * as ko from "knockout";
import { IViewModel } from "src/componentsRegistration/IViewModel";
import { IExecuteRuleStepCardParams } from "src/components/app/workspacePage/workspaceRulesComponent/ruleCard/ruleStepCard/ExecuteRuleStepCard/IExecuteRuleStepCardParams";

export class ExecuteRuleStepCardViewModel implements IViewModel {
    public readonly params: IExecuteRuleStepCardParams;
    public readonly description: ko.Observable<string> = ko.observable("");

    constructor(params: IExecuteRuleStepCardParams) {
        this.params = params;
        this.description(`${params.index() + 1}. ${params.step.name}`);
    }
}