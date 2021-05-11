import { IWorkspaceApiModel } from "../IWorkspaceApiModel";
import { IThingApiModel } from "./IThingApiModel";

export interface IGetWorkspaceThingsResponse {
    workspace: IWorkspaceApiModel;
    things: IThingApiModel[];
}