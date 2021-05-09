import { IGetUserWorkspacesResponse } from "../models/response/IGetUserWorkspacesResponse";
import { ICreateWorkspaceRequest } from "../models/request/ICreateWorkspaceRequest";

export class WorkspaceApiClient {
    private static readonly apiUrl: string = "api/WorkspaceApi/";

    public async getUserWorkspaces(): Promise<IGetUserWorkspacesResponse> {
        const response: Response = await fetch(`${WorkspaceApiClient.apiUrl}GetUserWorkspaces`, { method: "GET" });
        return response.json();
    }

    public createWorkspace(requestModel: ICreateWorkspaceRequest): Promise<Response> {
        return fetch(`${WorkspaceApiClient.apiUrl}Create`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(requestModel),
        });
    }
}