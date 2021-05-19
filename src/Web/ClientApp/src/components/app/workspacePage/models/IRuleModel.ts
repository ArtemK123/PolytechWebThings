import { IStepModel } from "src/components/app/workspacePage/models/IStepModel";

export interface IRuleModel {
    id: number;
    name: string;
    steps: IStepModel[];
}