import * as ko from "knockout";
import { IChangeThingStateStepModel } from "src/components/app/workspacePage/models/IChangeThingStateStepModel";
import { IStepModel } from "src/components/app/workspacePage/models/IStepModel";
import { IThingApiModel } from "src/backendApi/models/entities/IThingApiModel";

export interface IChangeThingStateStepEditCardParams {
    step: IChangeThingStateStepModel;
    currentStepType: ko.Observable<string>;
    things: ko.ObservableArray<IThingApiModel>;
    stepTypes: string[];
    confirmEditAction: (step: IStepModel) => {};
    cancelEditAction: () => {};
}