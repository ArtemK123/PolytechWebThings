import { IStepModel } from "src/components/app/workspacePage/models/IStepModel";

export interface IChangeThingStateStepModel extends IStepModel {
    thingName: string;
    propertyName: string;
    newPropertyState: string;
}