import * as ko from "knockout";
import { IStepModel } from "src/components/app/workspacePage/models/IStepModel";

export interface IEditStepCardParams {
    step: IStepModel;
    confirmAction: (step: IStepModel) => {};
    cancelAction: () => {};
}