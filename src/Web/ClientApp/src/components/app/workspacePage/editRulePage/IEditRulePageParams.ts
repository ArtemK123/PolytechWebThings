import { IRuleModel } from "src/components/app/workspacePage/models/IRuleModel";

export interface IEditRulePageParams {
    ruleId: number | undefined;
    confirmAction: (updatedRule: IRuleModel) => {},
    cancelAction: () => {}
}