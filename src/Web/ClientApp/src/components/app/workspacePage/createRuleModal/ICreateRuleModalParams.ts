import * as ko from "knockout";
import { IThingApiModel } from "src/backendApi/models/entities/IThingApiModel";
import { IRuleModel } from "src/components/app/workspacePage/models/IRuleModel";

export interface ICreateRuleModalParams {
    isVisible: ko.Observable<boolean>;
    rules: ko.ObservableArray<IRuleModel>;
    things: ko.ObservableArray<IThingApiModel>;
}