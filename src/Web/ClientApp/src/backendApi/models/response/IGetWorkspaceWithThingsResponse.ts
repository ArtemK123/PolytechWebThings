import { IThingApiModel } from "./things/IThingApiModel";
import { IWorkspaceApiModel } from "./IWorkspaceApiModel";

export interface IGetWorkspaceWithThingsResponse {
    workspace: IWorkspaceApiModel;
    things: IThingApiModel[];
}