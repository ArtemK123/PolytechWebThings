import { IViewModel } from "src/componentsRegistration/IViewModel";
import { HealthCheckApiClient } from "src/backendApi/clients/HealthCheckApiClient";
import { IOperationResult } from "src/backendApi/models/entities/OperationResult/IOperationResult";

export class BackendConnectionCheckViewModel implements IViewModel {
    private readonly apiClient = new HealthCheckApiClient();

    constructor() {
        this.apiClient
            .healthCheck()
            .then((operationResult: IOperationResult<string>) => console.log(operationResult.data));
    }
}