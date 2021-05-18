import { IThingApiModel } from "src/backendApi/models/entities/IThingApiModel";

export interface IThingCardParams {
    model: IThingApiModel;
    workspaceId: number;
}