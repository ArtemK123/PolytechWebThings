import { IWorkspaceApiModel } from "src/backendApi/models/response/IWorkspaceApiModel";

export interface IGetUserWorkspacesResponse {
    workspaces: IWorkspaceApiModel[];
}