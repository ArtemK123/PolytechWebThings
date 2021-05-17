import * as ko from "knockout";
import { IPropertyApiModel } from "src/backendApi/models/response/things/IPropertyApiModel";
import { IThingStateApiModel } from "src/backendApi/models/response/things/IThingStateApiModel";

export interface IThingPropertyCardParams {
    model: IPropertyApiModel;
    workspaceId: number;
    thingState: ko.Observable<IThingStateApiModel>;
}