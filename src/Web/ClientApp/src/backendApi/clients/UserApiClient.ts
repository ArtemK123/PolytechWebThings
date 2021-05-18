import { ICreateUserRequest } from "src/backendApi/models/request/user/ICreateUserRequest";
import { BackendRequestSender } from "src/backendApi/senders/BackendRequestSender";
import { IOperationResult } from "src/backendApi/models/entities/OperationResult/IOperationResult";
import { ILoginUserRequest } from "src/backendApi/models/request/user/ILoginUserRequest";

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