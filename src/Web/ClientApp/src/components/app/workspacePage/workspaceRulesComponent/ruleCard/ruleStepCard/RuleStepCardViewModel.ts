import * as ko from "knockout";
import { IViewModel } from "src/componentsRegistration/IViewModel";
import { IRuleStepCardParams } from "src/components/app/workspacePage/workspaceRulesComponent/ruleCard/ruleStepCard/IRuleStepCardParams";

export class RuleStepCardViewModel implements IViewModel {
    public readonly params: IRuleStepCardParams;
    public readonly description: ko.Observable<string> = ko.observable("");

    constructor(params: IRuleStepCardParams) {
        this.params = params;
        this.description(`${params.index()}. ${params.step.description}`);
    }
}