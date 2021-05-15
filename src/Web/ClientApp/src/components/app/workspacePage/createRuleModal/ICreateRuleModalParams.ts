import * as ko from "knockout";
import { IRuleModel } from "../models/IRuleModel";
import { IThingApiModel } from "../../../../backendApi/models/response/things/IThingApiModel";

export interface ICreateRuleModalParams {
    isVisible: ko.Observable<boolean>;
    rules: ko.ObservableArray<IRuleModel>;
    things: ko.ObservableArray<IThingApiModel>;
}