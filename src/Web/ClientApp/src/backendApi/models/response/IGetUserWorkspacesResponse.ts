import { IWorkspaceApiModel } from "src/backendApi/models/entities/IWorkspaceApiModel";

export interface IGetUserWorkspacesResponse {
    workspaces: IWorkspaceApiModel[];
}