import { IOperationResult } from "../models/response/OperationResult/IOperationResult";
import { BackendRequestSender } from "../senders/BackendRequestSender";

export class HealthCheckApiClient {
    private readonly backendRequestSender: BackendRequestSender = new BackendRequestSender();

    public async healthCheck(): Promise<IOperationResult<string>> {
        return this.backendRequestSender.send<string>("/api/HealthCheckApi/HealthCheck", { method: "GET" });
    }
}