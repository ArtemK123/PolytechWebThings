import * as ko from "knockout";
import { IChangeThingStateStepModel } from "src/components/app/workspacePage/models/IChangeThingStateStepModel";

export interface IChangeThingStateStepCardParams {
    step: IChangeThingStateStepModel;
    index: ko.Observable<number>;
}