import { IViewModel } from "../../componentsRegistration/IViewModel";
import { HealthCheckApiClient } from "../../backendApi/clients/HealthCheckApiClient";
import { IOperationResult } from "../../backendApi/models/response/OperationResult/IOperationResult";

export class BackendConnectionCheckViewModel implements IViewModel {
    private readonly apiClient = new HealthCheckApiClient();

    constructor() {
        this.apiClient
            .healthCheck()
            .then((operationResult: IOperationResult<string>) => console.log(operationResult.data));
    }
}