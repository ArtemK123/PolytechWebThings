﻿import { BackendRequestSender } from "src/backendApi/senders/BackendRequestSender";
import { IChangePropertyStateRequest } from "src/backendApi/models/request/things/IChangePropertyStateRequest";
import { IOperationResult } from "src/backendApi/models/entities/OperationResult/IOperationResult";
import { IGetThingStateRequest } from "src/backendApi/models/request/things/IGetThingStateRequest";
import { IThingStateApiModel } from "src/backendApi/models/entities/IThingStateApiModel";
import { IGetWorkspaceWithThingsRequest } from "src/backendApi/models/request/things/IGetWorkspaceWithThingsRequest";
import { IGetWorkspaceWithThingsResponse } from "src/backendApi/models/response/IGetWorkspaceWithThingsResponse";

export class ThingsApiClient {
    private static readonly apiUrlBase: string = "/api/ThingsApi/";
    private readonly backendRequestSender: BackendRequestSender = new BackendRequestSender();

    public changePropertyState(request: IChangePropertyStateRequest): Promise<IOperationResult<void>> {
        return this.backendRequestSender.sendPostRequest(`${ThingsApiClient.apiUrlBase}ChangePropertyState`, request);
    }

    public getThingState(request: IGetThingStateRequest): Promise<IOperationResult<IThingStateApiModel>> {
        return this.backendRequestSender.sendPostRequest(`${ThingsApiClient.apiUrlBase}GetThingState`, request);
    }

    public getWorkspaceWithThingsRequest(requestModel: IGetWorkspaceWithThingsRequest): Promise<IOperationResult<IGetWorkspaceWithThingsResponse>> {
        return this.backendRequestSender.sendPostRequest(`${ThingsApiClient.apiUrlBase}GetWorkspaceWithThings`, requestModel);
    }
}