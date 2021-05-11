import { IOperationResult } from "../models/response/OperationResult/IOperationResult";

export class BackendRequestDirectSender {
    public async send<TData>(url: RequestInfo, config?: RequestInit): Promise<IOperationResult<TData>> {
        const response = await fetch(url, config);
        if (!response.ok) {
            throw new Error("Invalid response from server.");
        }
        return response.json();
    }

    public async sendGetRequest<TResponseData>(url: RequestInfo): Promise<IOperationResult<TResponseData>> {
        return this.send(url, {
            method: "GET",
        });
    }

    public async sendPostRequest<TRequestData, TResponseData>(url: RequestInfo, request: TRequestData): Promise<IOperationResult<TResponseData>> {
        return this.send(url, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(request),
        });
    }

    public async sendPostRequestWithoutBody<TResponseData>(url: RequestInfo): Promise<IOperationResult<TResponseData>> {
        return this.send(url, {
            method: "POST",
        });
    }
}