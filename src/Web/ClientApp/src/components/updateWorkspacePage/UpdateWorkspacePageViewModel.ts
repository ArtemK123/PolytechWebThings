import * as ko from "knockout";
import { IViewModel } from "../../componentsRegistration/IViewModel";
import { WorkspaceApiClient } from "../../backendApi/clients/WorkspaceApiClient";
import { IUpdateWorkspacePageParams } from "./IUpdateWorkspacePageParams";
import { IGetWorkspaceByIdRequest } from "../../backendApi/models/request/workspace/IGetWorkspaceByIdRequest";
import { IWorkspaceApiModel } from "../../backendApi/models/response/IWorkspaceApiModel";
import { RedirectHandler } from "../../services/RedirectHandler";
import { IUpdateWorkspaceRequest } from "../../backendApi/models/request/workspace/IUpdateWorkspaceRequest";

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
            .then((response) => response.json())
            .then((workspaceApiModel: IWorkspaceApiModel) => {
                this.name(workspaceApiModel.name);
                this.url(workspaceApiModel.gatewayUrl);
                this.token(workspaceApiModel.accessToken);
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
            .then(async (response) => {
                if (response.status === 200) {
                    alert("Updated successfully");
                    RedirectHandler.redirect("/");
                } else if (response.status !== 500) {
                    const responseText: string = await response.text();
                    alert(responseText);
                } else {
                    alert("Error while updating");
                }
            });
    }
}