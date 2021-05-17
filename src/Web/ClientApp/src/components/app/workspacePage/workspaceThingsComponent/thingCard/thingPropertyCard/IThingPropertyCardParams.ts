import { IPropertyApiModel } from "src/backendApi/models/response/things/IPropertyApiModel";

export interface IThingPropertyCardParams {
    model: IPropertyApiModel;
    thingId: string;
    workspaceId: number;
}