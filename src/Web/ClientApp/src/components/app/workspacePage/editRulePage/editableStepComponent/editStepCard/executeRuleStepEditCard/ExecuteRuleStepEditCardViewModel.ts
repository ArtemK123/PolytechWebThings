import * as ko from "knockout";
import { IViewModel } from "src/componentsRegistration/IViewModel";
import { IExecuteRuleStepEditCardParams }
    from "src/components/app/workspacePage/editRulePage/editableStepComponent/editStepCard/ExecuteRuleStepEditCard/IExecuteRuleStepEditCardParams";

export class ExecuteRuleStepEditCardViewModel implements IViewModel {
    public readonly params: IExecuteRuleStepEditCardParams;

    constructor(params: IExecuteRuleStepEditCardParams) {
        this.params = params;
    }
}