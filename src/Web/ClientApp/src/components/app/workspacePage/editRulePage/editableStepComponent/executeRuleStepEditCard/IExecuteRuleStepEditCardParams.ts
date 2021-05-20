import * as ko from "knockout";
import { IStepModel } from "src/components/app/workspacePage/models/IStepModel";
import { IExecuteRuleStepModel } from "src/components/app/workspacePage/models/IExecuteRuleStepModel";

export interface IExecuteRuleStepEditCardParams {
    step: IExecuteRuleStepModel;
    currentStepType: ko.Observable<string>;
    stepTypes: string[];
    availableRuleNames: ko.ObservableArray<string>;
    confirmAction: (step: IStepModel) => {};
    cancelAction: () => {};
}