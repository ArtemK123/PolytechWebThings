import * as ko from "knockout";
import { IRuleModel } from "src/components/app/workspacePage/models/IRuleModel";
import { IThingApiModel } from "src/backendApi/models/entities/IThingApiModel";

export interface IEditRulePageParams {
    ruleId: number | undefined;
    things: ko.ObservableArray<IThingApiModel>;
    rules: ko.ObservableArray<IThingApiModel>;
    confirmAction: (updatedRule: IRuleModel) => {};
    cancelAction: () => {};
}