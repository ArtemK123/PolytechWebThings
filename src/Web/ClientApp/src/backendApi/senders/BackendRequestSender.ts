import { IOperationResult } from "../models/response/OperationResult/IOperationResult";
import { OperationStatus } from "../models/response/OperationResult/OperationStatus";
import { BackendRequestDirectSender } from "./BackendRequestDirectSender";
import { UnsuccessfulOperationHandler } from "../handler/UnsuccessfulOperationHandler";

export class BackendRequestSender extends BackendRequestDirectSender {
    private readonly unsuccessfulOperationHandler: UnsuccessfulOperationHandler = new UnsuccessfulOperationHandler();

    public async send<TData>(url: RequestInfo, config?: RequestInit): Promise<IOperationResult<TData>> {
        const response = await fetch(url, config);
        if (!response.ok) {
            throw new Error("Add appropriate handling here.");
        }
        return response.json();
    }

    public async sendGetRequest<TResponseData>(url: RequestInfo): Promise<IOperationResult<TResponseData>> {
        const response: IOperationResult<TResponseData> = await super.sendGetRequest(url);
        this.checkResponse(response);
        return response;
    }

    public async sendPostRequest<TRequestData, TResponseData>(url: RequestInfo, request: TRequestData): Promise<IOperationResult<TResponseData>> {
        const response: IOperationResult<TResponseData> = await super.sendPostRequest(url, request);
        this.checkResponse(response);
        return response;
    }

    public async sendPostRequestWithoutBody<TResponseData>(url: RequestInfo): Promise<IOperationResult<TResponseData>> {
        const response: IOperationResult<TResponseData> = await super.sendPostRequestWithoutBody(url);
        this.checkResponse(response);
        return response;
    }

    private checkResponse<TResponseData>(response: IOperationResult<TResponseData>) {
        if (response.status !== OperationStatus.Success) {
            this.unsuccessfulOperationHandler.handle(response);
        }
    }
}