import { IOperationResult } from "../models/response/OperationResult/IOperationResult";

export class BackendRequestSender {
    public static async send<TData>(input: RequestInfo, config?: RequestInit): Promise<IOperationResult<TData>> {
        const response = await fetch(input, config);
        if (!response.ok) {
            throw new Error("Add appropriate handling here.");
        }
        return response.json();
    }
}