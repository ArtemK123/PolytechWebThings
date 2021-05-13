import * as ko from "knockout";
import { IWorkspaceApiModel } from "../../../../backendApi/models/response/IWorkspaceApiModel";
import { IGetUserWorkspacesResponse } from "../../../../backendApi/models/response/IGetUserWorkspacesResponse";
import { IViewModel } from "../../../../componentsRegistration/IViewModel";
import { IOperationResult } from "../../../../backendApi/models/response/OperationResult/IOperationResult";
import { WorkspaceApiClient } from "../../../../backendApi/clients/WorkspaceApiClient";

export class AuthorizedHomePageViewModel implements IViewModel {
    public readonly email: ko.Observable<string> = ko.observable<string>("");
    public readonly workspaces: ko.ObservableArray<IWorkspaceApiModel> = ko.observableArray([]);

    private readonly workspaceApiClient: WorkspaceApiClient = new WorkspaceApiClient();

    constructor() {
        const storedEmail: string | null | undefined = localStorage.getItem("email");
        if (!storedEmail) {
            throw new Error("User email is not found");
        }

        this.email(storedEmail);
        this.fetchWorkspaces();
    }

    private fetchWorkspaces(): void {
        this.workspaceApiClient.getUserWorkspaces().then((response: IOperationResult<IGetUserWorkspacesResponse>) => {
            this.workspaces(response.data.workspaces);
        });
    }
}