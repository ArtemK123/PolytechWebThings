import * as ko from "knockout";
import { IChangeThingStateStepModel } from "src/components/app/workspacePage/models/IChangeThingStateStepModel";

export interface IChangeThingStateStepEditCardParams {
    step: IChangeThingStateStepModel;
    currentStepType: ko.Observable<string>;
    stepTypes: string[];
}