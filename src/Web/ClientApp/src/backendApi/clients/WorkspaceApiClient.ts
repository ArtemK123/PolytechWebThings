import { IGetUserWorkspacesResponse } from "../models/response/IGetUserWorkspacesResponse";
import { ICreateWorkspaceRequest } from "../models/request/workspace/ICreateWorkspaceRequest";
import { IDeleteWorkspaceRequest } from "../models/request/workspace/IDeleteWorkspaceRequest";
import { IGetWorkspaceByIdRequest } from "../models/request/workspace/IGetWorkspaceByIdRequest";
import { IUpdateWorkspaceRequest } from "../models/request/workspace/IUpdateWorkspaceRequest";
import { IOperationResult } from "../models/response/OperationResult/IOperationResult";
import { IWorkspaceApiModel } from "../models/response/IWorkspaceApiModel";
import { BackendRequestSender } from "../senders/BackendRequestSender";
import { IGetWorkspaceWithThingsRequest } from "../models/request/workspace/IGetWorkspaceWithThingsRequest";
import { IGetWorkspaceWithThingsResponse } from "../models/response/IGetWorkspaceWithThingsResponse";

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

    public getWorkspaceWithThingsRequest(requestModel: IGetWorkspaceWithThingsRequest): Promise<IOperationResult<IGetWorkspaceWithThingsResponse>> {
        return this.backendRequestSender.sendPostRequest(`${WorkspaceApiClient.apiUrl}GetWorkspaceWithThings`, requestModel);
    }
}