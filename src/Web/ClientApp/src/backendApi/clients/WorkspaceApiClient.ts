import { IGetUserWorkspacesResponse } from "../models/response/IGetUserWorkspacesResponse";
import { ICreateWorkspaceRequest } from "../models/request/ICreateWorkspaceRequest";

export class WorkspaceApiClient {
    public async getUserWorkspaces(): Promise<IGetUserWorkspacesResponse> {
        const response: Response = await fetch("api/workspace/GetUserWorkspaces", { method: "GET" });
        return response.json();
    }

    public createWorkspace(requestModel: ICreateWorkspaceRequest): Promise<Response> {
        return fetch("api/workspace/Create", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(requestModel),
        });
    }
}