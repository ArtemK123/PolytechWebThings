import * as ko from "knockout";
import { IExecuteRuleStepModel } from "src/components/app/workspacePage/models/IExecuteRuleStepModel";

export interface IExecuteRuleStepCardParams {
    step: IExecuteRuleStepModel;
    index: ko.Observable<number>;
}