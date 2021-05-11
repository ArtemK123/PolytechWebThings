import { IOperationResult } from "../models/response/OperationResult/IOperationResult";

export class HealthCheckApiClient {
    public async healthCheck(): Promise<IOperationResult<string>> {
        const response = await fetch("/api/HealthCheckApi/HealthCheck", { method: "GET" });
        return response.json();
    }
}