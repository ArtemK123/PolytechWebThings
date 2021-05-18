import * as ko from "knockout";
import { IUpdateWorkspaceRequest } from "src/backendApi/models/request/workspace/IUpdateWorkspaceRequest";
import { RedirectHandler } from "src/services/RedirectHandler";
import { OperationStatus } from "src/backendApi/models/entities/OperationResult/OperationStatus";
import { IWorkspaceApiModel } from "src/backendApi/models/entities/IWorkspaceApiModel";
import { IViewModel } from "src/componentsRegistration/IViewModel";
import { IGetWorkspaceByIdRequest } from "src/backendApi/models/request/workspace/IGetWorkspaceByIdRequest";
import { IUpdateWorkspacePageParams } from "src/components/app/updateWorkspacePage/IUpdateWorkspacePageParams";
import { IOperationResult } from "src/backendApi/models/entities/OperationResult/IOperationResult";
import { WorkspaceApiClient } from "src/backendApi/clients/WorkspaceApiClient";

export class UpdateWorkspacePageViewModel implements IViewModel {
    public readonly name: ko.Observable<string> = ko.observable("");
    public readonly url: ko.Observable<string> = ko.observable("");
    public readonly token: ko.Observable<string> = ko.observable("");

    private readonly workspaceId: number;

    private readonly apiClient: WorkspaceApiClient = new WorkspaceApiClient();

    constructor(params: IUpdateWorkspacePageParams) {
        this.workspaceId = params.id;
        this.apiClient
            .getWorkspaceById({ id: params.id } as IGetWorkspaceByIdRequest)
            .then((operationResult: IOperationResult<IWorkspaceApiModel>) => {
                this.name(operationResult.data.name);
                this.url(operationResult.data.gatewayUrl);
                this.token(operationResult.data.accessToken);
            })
            .catch((error) => {
                console.error(error);
                alert("Error while fetching the workspace model");
                RedirectHandler.redirect("/");
            });
    }

    public handleSubmit(): void {
        const requestModel: IUpdateWorkspaceRequest = {
            id: this.workspaceId,
            name: this.name(),
            gatewayUrl: this.url(),
            accessToken: this.token(),
        } as IUpdateWorkspaceRequest;

        this.apiClient
            .updateWorkspace(requestModel)
            .then((result) => {
                if (result.status === OperationStatus.Success) {
                    alert("Updated successfully");
                    RedirectHandler.redirect("/");
                }
            });
    }
}