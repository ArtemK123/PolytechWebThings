import { IOperationResult } from "../models/response/OperationResult/IOperationResult";
import { BackendRequestSender } from "../senders/BackendRequestSender";

export class HealthCheckApiClient {
    public async healthCheck(): Promise<IOperationResult<string>> {
        return BackendRequestSender.send<string>("/api/HealthCheckApi/HealthCheck", { method: "GET" });
    }
}