import { IWorkspaceApiModel } from "../IWorkspaceApiModel";
import { IThingApiModel } from "./IThingApiModel";

export interface IGetWorkspaceWithThingsResponse {
    workspace: IWorkspaceApiModel;
    things: IThingApiModel[];
}