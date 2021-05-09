import { ICreateUserRequest } from "../models/request/ICreateUserRequest";
import { ILoginUserRequest } from "../models/request/ILoginUserRequest";

export class UserApiClient {
    public create(requestModel: ICreateUserRequest): Promise<Response> {
        return fetch("api/UserApi/Create", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(requestModel),
        });
    }

    public login(requestModel: ILoginUserRequest): Promise<Response> {
        return fetch("api/UserApi/Login", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(requestModel),
        });
    }

    public logout(): Promise<Response> {
        return fetch("api/UserApi/Logout", {
            method: "POST",
        });
    }
}