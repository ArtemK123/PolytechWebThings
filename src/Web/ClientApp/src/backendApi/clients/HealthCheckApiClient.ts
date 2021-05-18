import { BackendRequestSender } from "src/backendApi/senders/BackendRequestSender";
import { IOperationResult } from "src/backendApi/models/entities/OperationResult/IOperationResult";

export class HealthCheckApiClient {
    private readonly backendRequestSender: BackendRequestSender = new BackendRequestSender();

    public async healthCheck(): Promise<IOperationResult<string>> {
        return this.backendRequestSender.send<string>("/api/HealthCheckApi/HealthCheck", { method: "GET" });
    }
}