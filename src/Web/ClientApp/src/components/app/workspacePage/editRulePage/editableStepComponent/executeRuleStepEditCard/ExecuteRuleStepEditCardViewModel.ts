import * as ko from "knockout";
import { IViewModel } from "src/componentsRegistration/IViewModel";
import { IExecuteRuleStepEditCardParams } from "src/components/app/workspacePage/editRulePage/editableStepComponent/ExecuteRuleStepEditCard/IExecuteRuleStepEditCardParams";
import { IExecuteRuleStepModel } from "src/components/app/workspacePage/models/IExecuteRuleStepModel";
import { StepType } from "src/components/app/workspacePage/models/StepType";

export class ExecuteRuleStepEditCardViewModel implements IViewModel {
    public readonly params: IExecuteRuleStepEditCardParams;
    public readonly selectedRule: ko.Observable<string>;

    constructor(params: IExecuteRuleStepEditCardParams) {
        this.params = params;
        this.selectedRule = ko.observable(params.step.ruleName);
    }

    public handleConfirm(): void {
        const updatedStep: IExecuteRuleStepModel = {
            stepType: StepType.ExecuteRule,
            ruleName: this.selectedRule(),
        } as IExecuteRuleStepModel;

        this.params.confirmEditAction(updatedStep);
    }

    public handleCancel(): void {
        this.params.cancelEditAction();
    }
}