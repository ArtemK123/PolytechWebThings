import { ICreateUserRequest } from "../models/request/user/ICreateUserRequest";
import { ILoginUserRequest } from "../models/request/user/ILoginUserRequest";
import { IOperationResult } from "../models/response/OperationResult/IOperationResult";
import { BackendRequestSender } from "../senders/BackendRequestSender";

export class UserApiClient {
    private static readonly apiUrlBase: string = "/api/UserApi/";
    private readonly backendRequestSender: BackendRequestSender = new BackendRequestSender();

    public create(requestModel: ICreateUserRequest): Promise<IOperationResult<void>> {
        return this.backendRequestSender.sendPostRequest(`${UserApiClient.apiUrlBase}Create`, requestModel);
    }

    public login(requestModel: ILoginUserRequest): Promise<IOperationResult<void>> {
        return this.backendRequestSender.sendPostRequest(`${UserApiClient.apiUrlBase}Login`, requestModel);
    }

    public logout(): Promise<IOperationResult<void>> {
        return this.backendRequestSender.sendPostRequestWithoutBody(`${UserApiClient.apiUrlBase}Logout`);
    }
}