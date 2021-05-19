import * as ko from "knockout";
import { IRuleModel } from "src/components/app/workspacePage/models/IRuleModel";

export interface IWorkspaceRulesComponentParams {
    rules: ko.ObservableArray<IRuleModel>;
    editRuleAction: (rule: IRuleModel) => {},
    deleteRuleAction: (rule: IRuleModel) => {}
}