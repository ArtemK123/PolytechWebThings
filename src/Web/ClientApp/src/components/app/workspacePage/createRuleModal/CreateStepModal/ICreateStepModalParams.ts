import * as ko from "knockout";
import { IThingApiModel } from "src/backendApi/models/entities/IThingApiModel";
import { IRuleModel } from "src/components/app/workspacePage/models/IRuleModel";

export interface ICreateStepModalParams {
    isVisible: ko.Observable<boolean>;
    steps: ko.ObservableArray<string>;
    rules: ko.ObservableArray<IRuleModel>;
    things: ko.ObservableArray<IThingApiModel>;
}