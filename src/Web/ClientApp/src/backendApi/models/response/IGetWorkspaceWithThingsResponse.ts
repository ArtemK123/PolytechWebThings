import { IThingApiModel } from "src/backendApi/models/entities/IThingApiModel";
import { IWorkspaceApiModel } from "src/backendApi/models/entities/IWorkspaceApiModel";

export interface IGetWorkspaceWithThingsResponse {
    workspace: IWorkspaceApiModel;
    things: IThingApiModel[];
}