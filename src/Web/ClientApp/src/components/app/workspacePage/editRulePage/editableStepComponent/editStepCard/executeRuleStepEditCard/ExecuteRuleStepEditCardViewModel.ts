import * as ko from "knockout";
import { IViewModel } from "src/componentsRegistration/IViewModel";
import { IExecuteRuleStepEditCardParams }
    from "src/components/app/workspacePage/editRulePage/editableStepComponent/editStepCard/ExecuteRuleStepEditCard/IExecuteRuleStepEditCardParams";

export class ExecuteRuleStepEditCardViewModel implements IViewModel {
    public readonly params: IExecuteRuleStepEditCardParams;
    public readonly titleText: ko.Computed<string>;

    constructor(params: IExecuteRuleStepEditCardParams) {
        this.params = params;
        this.titleText = ko.computed(() => "Change thing state");
    }
}