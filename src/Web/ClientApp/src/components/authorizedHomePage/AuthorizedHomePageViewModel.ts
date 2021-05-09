import * as ko from "knockout";
import { IViewModel } from "../../componentsRegistration/IViewModel";
import { IGetUserWorkspacesResponse } from "../../backendApi/models/response/IGetUserWorkspacesResponse";
import { WorkspaceApiClient } from "../../backendApi/clients/WorkspaceApiClient";
import { IWorkspaceApiModel } from "../../backendApi/models/response/IWorkspaceApiModel";

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
        this.workspaceApiClient.getUserWorkspaces().then((response: IGetUserWorkspacesResponse) => {
            this.workspaces(response.workspaces);
        });
    }
}