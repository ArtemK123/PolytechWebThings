import * as ko from "knockout";
import { IRuleModel } from "src/components/app/workspacePage/models/IRuleModel";

export interface IEditRulePageParams {
    rule: ko.Observable<IRuleModel>;
    confirmAction: (updatedRule: IRuleModel) => {},
    cancelAction: () => {}
}