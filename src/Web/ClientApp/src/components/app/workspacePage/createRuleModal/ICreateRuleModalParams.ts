import * as ko from "knockout";
import { IRuleModel } from "../models/IRuleModel";

export interface ICreateRuleModalParams {
    isVisible: ko.Observable<boolean>;
    rules: ko.ObservableArray<IRuleModel>;
}