import { IRuleModel } from "src/components/app/workspacePage/models/IRuleModel";

export interface IRuleCardParams {
    rule: IRuleModel;
    editRuleAction: (rule: IRuleModel) => {},
    deleteRuleAction: (rule: IRuleModel) => {}
}