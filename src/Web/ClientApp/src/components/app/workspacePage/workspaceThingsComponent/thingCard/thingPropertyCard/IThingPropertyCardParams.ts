import * as ko from "knockout";
import { IPropertyApiModel } from "src/backendApi/models/entities/IPropertyApiModel";
import { IThingStateApiModel } from "src/backendApi/models/entities/IThingStateApiModel";

export interface IThingPropertyCardParams {
    model: IPropertyApiModel;
    workspaceId: number;
    thingState: ko.Observable<IThingStateApiModel>;
}