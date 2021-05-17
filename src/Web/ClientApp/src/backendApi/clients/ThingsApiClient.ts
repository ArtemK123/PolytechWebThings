import { BackendRequestSender } from "src/backendApi/senders/BackendRequestSender";
import { IChangePropertyStateRequest } from "src/backendApi/models/request/things/IChangePropertyStateRequest";
import { IOperationResult } from "src/backendApi/models/response/OperationResult/IOperationResult";

export class ThingsApiClient {
    private static readonly apiUrlBase: string = "/api/ThingsApi/";
    private readonly backendRequestSender: BackendRequestSender = new BackendRequestSender();

    public changePropertyState(request: IChangePropertyStateRequest): Promise<IOperationResult<void>> {
        return this.backendRequestSender.sendPostRequest(`${ThingsApiClient.apiUrlBase}ChangePropertyState`, request);
    }
}