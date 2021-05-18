import * as ko from "knockout";
import { IStepModel } from "src/components/app/workspacePage/models/IStepModel";

export interface IRuleStepCardParams {
    step: IStepModel;
    index: ko.Observable<number>;
}