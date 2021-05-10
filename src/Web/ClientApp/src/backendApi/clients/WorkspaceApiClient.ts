﻿import { IGetUserWorkspacesResponse } from "../models/response/IGetUserWorkspacesResponse";
import { ICreateWorkspaceRequest } from "../models/request/workspace/ICreateWorkspaceRequest";
import { IDeleteWorkspaceRequest } from "../models/request/workspace/IDeleteWorkspaceRequest";
import { IGetWorkspaceByIdRequest } from "../models/request/workspace/IGetWorkspaceByIdRequest";
import { IUpdateWorkspaceRequest } from "../models/request/workspace/IUpdateWorkspaceRequest";

export class WorkspaceApiClient {
    private static readonly apiUrl: string = "/api/WorkspaceApi/";

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

    public deleteWorkspace(requestModel: IDeleteWorkspaceRequest): Promise<Response> {
        return fetch(`${WorkspaceApiClient.apiUrl}Delete`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(requestModel),
        });
    }

    public getWorkspaceById(requestModel: IGetWorkspaceByIdRequest): Promise<Response> {
        return fetch(`${WorkspaceApiClient.apiUrl}GetById`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(requestModel),
        });
    }

    public updateWorkspace(requestModel: IUpdateWorkspaceRequest): Promise<Response> {
        return fetch(`${WorkspaceApiClient.apiUrl}Update`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(requestModel),
        });
    }
}