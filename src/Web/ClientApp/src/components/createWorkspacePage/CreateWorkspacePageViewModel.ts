import * as ko from "knockout";
import { IViewModel } from "../../componentsRegistration/IViewModel";
import { ICreateWorkspaceRequest } from "../../backendApi/models/request/workspace/ICreateWorkspaceRequest";
import { WorkspaceApiClient } from "../../backendApi/clients/WorkspaceApiClient";
import { RedirectHandler } from "../../services/RedirectHandler";

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
            .then(async (response) => {
                if (response.status === 200) {
                    alert("Created successfully");
                    RedirectHandler.redirect("/");
                } else {
                    const responseMessage: string = await response.text();
                    alert(`Error while creating workspace: ${responseMessage}`);
                }
            });
    }
}