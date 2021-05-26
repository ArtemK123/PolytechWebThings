import * as ko from "knockout";
import { IWorkspaceApiModel } from "src/backendApi/models/entities/IWorkspaceApiModel";
import { IGetUserWorkspacesResponse } from "src/backendApi/models/response/IGetUserWorkspacesResponse";
import { IViewModel } from "src/componentsRegistration/IViewModel";
import { IOperationResult } from "src/backendApi/models/entities/OperationResult/IOperationResult";
import { WorkspaceApiClient } from "src/backendApi/clients/WorkspaceApiClient";

export class AuthorizedHomePageViewModel implements IViewModel {
    public readonly workspaces: ko.ObservableArray<IWorkspaceApiModel> = ko.observableArray([]);
    private readonly workspaceApiClient: WorkspaceApiClient = new WorkspaceApiClient();

    constructor() {
        this.fetchWorkspaces();
    }

    private fetchWorkspaces(): void {
        this.workspaceApiClient.getUserWorkspaces().then((response: IOperationResult<IGetUserWorkspacesResponse>) => {
            this.workspaces(response.data.workspaces);
        });
    }
}