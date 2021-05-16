import { IThingApiModel } from "src/backendApi/models/response/things/IThingApiModel";
import { IWorkspaceApiModel } from "src/backendApi/models/response/IWorkspaceApiModel";

export interface IGetWorkspaceWithThingsResponse {
    workspace: IWorkspaceApiModel;
    things: IThingApiModel[];
}