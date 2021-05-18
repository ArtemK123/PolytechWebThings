import * as ko from "knockout";
import { IViewModel } from "src/componentsRegistration/IViewModel";
import { IRuleCardParams } from "src/components/app/workspacePage/workspaceRulesComponent/ruleCard/IRuleCardParams";

export class RuleCardViewModel implements IViewModel {
    public readonly params: IRuleCardParams;

    constructor(params: IRuleCardParams) {
        this.params = params;
    }
}