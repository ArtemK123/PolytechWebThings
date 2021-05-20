import * as ko from "knockout";
import { IStepModel } from "src/components/app/workspacePage/models/IStepModel";

export interface IEditableStepComponentParams {
    step: IStepModel;
    index: ko.Observable<number>;
    availableRuleNames: ko.ObservableArray<string>;
    deleteAction: (stepIndex: number) => {};
    updateAction: (stepIndex: number, updatedStep: IStepModel) => {};
}