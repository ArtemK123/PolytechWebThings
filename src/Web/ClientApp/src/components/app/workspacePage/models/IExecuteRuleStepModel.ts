import { IStepModel } from "src/components/app/workspacePage/models/IStepModel";

export interface IExecuteRuleStepModel extends IStepModel {
    ruleName: string;
}