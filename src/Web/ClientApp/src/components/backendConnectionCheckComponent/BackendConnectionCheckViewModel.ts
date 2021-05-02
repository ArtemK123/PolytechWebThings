export class BackendConnectionCheckViewModel {
  constructor() {
    fetch("api/HealthCheckApi/HealthCheck", { method: "GET"} )
        .then(response => response.text())
        .then(text =>  console.log(text));
  }
}