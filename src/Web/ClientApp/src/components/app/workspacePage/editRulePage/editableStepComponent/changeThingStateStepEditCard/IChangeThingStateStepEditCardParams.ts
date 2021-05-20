import * as ko from "knockout";
import { IChangeThingStateStepModel } from "src/components/app/workspacePage/models/IChangeThingStateStepModel";
import { IStepModel } from "src/components/app/workspacePage/models/IStepModel";

export interface IChangeThingStateStepEditCardParams {
    step: IChangeThingStateStepModel;
    currentStepType: ko.Observable<string>;
    stepTypes: string[];
    confirmAction: (step: IStepModel) => {};
    cancelAction: () => {};
}