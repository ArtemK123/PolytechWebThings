import { HealthCheckApiClient } from "../../services/HealthCheckApiClient";

export class BackendConnectionCheckViewModel {
  private readonly apiClient = new HealthCheckApiClient();

  constructor() {
    this.apiClient.healthCheck()
      .then(response => response.text())
      .then(text =>  console.log(text));
  }
}