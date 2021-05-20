import * as ko from "knockout";
import { IStepModel } from "src/components/app/workspacePage/models/IStepModel";
import { IThingApiModel } from "src/backendApi/models/entities/IThingApiModel";

export interface IStepEditCardParams {
    step: IStepModel;
    index: ko.Observable<number>;
    things: ko.ObservableArray<IThingApiModel>;
    availableRuleNames: ko.Computed<string[]>
    confirmEditAction: (step: IStepModel) => {};
    cancelEditAction: () => {};
}