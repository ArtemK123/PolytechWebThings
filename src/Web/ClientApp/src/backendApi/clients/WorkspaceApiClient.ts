import { IUpdateWorkspaceRequest } from "src/backendApi/models/request/workspace/IUpdateWorkspaceRequest";
import { ICreateWorkspaceRequest } from "src/backendApi/models/request/workspace/ICreateWorkspaceRequest";
import { IGetUserWorkspacesResponse } from "src/backendApi/models/response/IGetUserWorkspacesResponse";
import { IWorkspaceApiModel } from "src/backendApi/models/response/IWorkspaceApiModel";
import { BackendRequestSender } from "src/backendApi/senders/BackendRequestSender";
import { IDeleteWorkspaceRequest } from "src/backendApi/models/request/workspace/IDeleteWorkspaceRequest";
import { IGetWorkspaceByIdRequest } from "src/backendApi/models/request/workspace/IGetWorkspaceByIdRequest";
import { IOperationResult } from "src/backendApi/models/response/OperationResult/IOperationResult";

export class WorkspaceApiClient {
    private static readonly apiUrl: string = "/api/WorkspaceApi/";
    private readonly backendRequestSender: BackendRequestSender = new BackendRequestSender();

    public async getUserWorkspaces(): Promise<IOperationResult<IGetUserWorkspacesResponse>> {
        return this.backendRequestSender.sendGetRequest(`${WorkspaceApiClient.apiUrl}GetUserWorkspaces`);
    }

    public createWorkspace(requestModel: ICreateWorkspaceRequest): Promise<IOperationResult<void>> {
        return this.backendRequestSender.sendPostRequest(`${WorkspaceApiClient.apiUrl}Create`, requestModel);
    }

    public deleteWorkspace(requestModel: IDeleteWorkspaceRequest): Promise<IOperationResult<void>> {
        return this.backendRequestSender.sendPostRequest(`${WorkspaceApiClient.apiUrl}Delete`, requestModel);
    }

    public getWorkspaceById(requestModel: IGetWorkspaceByIdRequest): Promise<IOperationResult<IWorkspaceApiModel>> {
        return this.backendRequestSender.sendPostRequest(`${WorkspaceApiClient.apiUrl}GetById`, requestModel);
    }

    public updateWorkspace(requestModel: IUpdateWorkspaceRequest): Promise<IOperationResult<void>> {
        return this.backendRequestSender.sendPostRequest(`${WorkspaceApiClient.apiUrl}Update`, requestModel);
    }
}