import { IRuleModel } from "src/components/app/workspacePage/models/IRuleModel";

export interface IEditRulePageParams {
    rule: IRuleModel;
    confirmAction: () => {},
    cancelAction: () => {}
}