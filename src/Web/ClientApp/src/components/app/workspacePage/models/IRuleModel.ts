import { IStepModel } from "src/components/app/workspacePage/models/IStepModel";

export interface IRuleModel {
    name: string;
    steps: IStepModel[];
}