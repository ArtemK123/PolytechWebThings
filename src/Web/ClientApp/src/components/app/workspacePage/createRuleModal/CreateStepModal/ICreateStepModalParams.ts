import * as ko from "knockout";
import { IRuleModel } from "../../models/IRuleModel";
import { IThingApiModel } from "../../../../../backendApi/models/response/things/IThingApiModel";

export interface ICreateStepModalParams {
    isVisible: ko.Observable<boolean>;
    steps: ko.ObservableArray<string>;
    rules: ko.ObservableArray<IRuleModel>;
    things: ko.ObservableArray<IThingApiModel>;
}