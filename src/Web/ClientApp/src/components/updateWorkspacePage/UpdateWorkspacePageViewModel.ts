import * as ko from "knockout";
import { IViewModel } from "../../componentsRegistration/IViewModel";
import { WorkspaceApiClient } from "../../backendApi/clients/WorkspaceApiClient";
import { IUpdateWorkspacePageParams } from "./IUpdateWorkspacePageParams";
import { IGetWorkspaceByIdRequest } from "../../backendApi/models/request/workspace/IGetWorkspaceByIdRequest";
import { IWorkspaceApiModel } from "../../backendApi/models/response/IWorkspaceApiModel";
import { RedirectHandler } from "../../services/RedirectHandler";
import { IUpdateWorkspaceRequest } from "../../backendApi/models/request/workspace/IUpdateWorkspaceRequest";
import { IOperationResult } from "../../backendApi/models/response/OperationResult/IOperationResult";
import { OperationStatus } from "../../backendApi/models/response/OperationResult/OperationStatus";

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