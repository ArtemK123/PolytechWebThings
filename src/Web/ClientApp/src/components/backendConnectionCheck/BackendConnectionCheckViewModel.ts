import { HealthCheckApiClient } from "../../services/HealthCheckApiClient";
import { IViewModel } from "../../componentsRegistration/IViewModel";

export class BackendConnectionCheckViewModel implements IViewModel {
    private readonly apiClient = new HealthCheckApiClient();

    constructor() {
        this.apiClient.healthCheck()
            .then((response) => response.text())
            // eslint-disable-next-line no-console
            .then((text) => console.log(text));
    }
}