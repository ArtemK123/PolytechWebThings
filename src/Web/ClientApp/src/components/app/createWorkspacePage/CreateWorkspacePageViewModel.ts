import * as ko from "knockout";
import { ICreateWorkspaceRequest } from "src/backendApi/models/request/workspace/ICreateWorkspaceRequest";
import { RedirectHandler } from "src/services/RedirectHandler";
import { OperationStatus } from "src/backendApi/models/response/OperationResult/OperationStatus";
import { IViewModel } from "src/componentsRegistration/IViewModel";
import { IOperationResult } from "src/backendApi/models/response/OperationResult/IOperationResult";
import { WorkspaceApiClient } from "src/backendApi/clients/WorkspaceApiClient";

export class CreateWorkspacePageViewModel implements IViewModel {
    public readonly name: ko.Observable<string> = ko.observable("");
    public readonly url: ko.Observable<string> = ko.observable("");
    public readonly token: ko.Observable<string> = ko.observable("");

    private readonly apiClient: WorkspaceApiClient = new WorkspaceApiClient();

    public handleSubmit(): void {
        const requestModel: ICreateWorkspaceRequest = {
            name: this.name(),
            gatewayUrl: this.url(),
            accessToken: this.token(),
        } as ICreateWorkspaceRequest;

        this.apiClient
            .createWorkspace(requestModel)
            .then((result: IOperationResult<void>) => {
                if (result.status === OperationStatus.Success) {
                    RedirectHandler.redirect("/");
                }
            });
    }
}