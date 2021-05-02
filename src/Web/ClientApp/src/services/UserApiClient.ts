import {ICreateUserCommand} from "../models/ICreateUserCommand";
import {ILoginUserCommand} from "../models/ILoginUserCommand";

export class UserApiClient {
    public create(requestModel: ICreateUserCommand): Promise<Response> {
        return fetch("api/UserApi/Create", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(requestModel)}
        );
    }

    public login(requestModel: ILoginUserCommand): Promise<Response> {
        return fetch("api/UserApi/Login", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(requestModel)}
        );
    }

    public logout(): Promise<Response> {
        return fetch("api/UserApi/Logout", {
            method: "POST"
        });
    }
}