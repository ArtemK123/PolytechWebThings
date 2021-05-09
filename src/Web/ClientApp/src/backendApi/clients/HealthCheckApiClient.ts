export class HealthCheckApiClient {
    public healthCheck(): Promise<Response> {
        return fetch("api/HealthCheckApi/HealthCheck", { method: "GET" });
    }
}