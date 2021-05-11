import * as ko from "knockout";
import { IViewModel } from "../../componentsRegistration/IViewModel";
import { ICreateWorkspaceRequest } from "../../backendApi/models/request/workspace/ICreateWorkspaceRequest";
import { WorkspaceApiClient } from "../../backendApi/clients/WorkspaceApiClient";
import { RedirectHandler } from "../../services/RedirectHandler";
import { OperationStatus } from "../../backendApi/models/response/OperationResult/OperationStatus";
import { IOperationResult } from "../../backendApi/models/response/OperationResult/IOperationResult";

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