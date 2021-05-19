import { IViewModel } from "src/componentsRegistration/IViewModel";
import { IRuleStepCardParams } from "src/components/app/workspacePage/workspaceRulesComponent/ruleCard/ruleStepCard/IRuleStepCardParams";

export class RuleStepCardViewModel implements IViewModel {
    public readonly params: IRuleStepCardParams;

    constructor(params: IRuleStepCardParams) {
        this.params = params;
    }
}