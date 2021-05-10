import * as ko from "knockout";
import { IViewModel } from "../../componentsRegistration/IViewModel";
import { WorkspaceApiClient } from "../../backendApi/clients/WorkspaceApiClient";
import { IUpdateWorkspacePageParams } from "./IUpdateWorkspacePageParams";
import { IGetWorkspaceByIdRequest } from "../../backendApi/models/request/workspace/IGetWorkspaceByIdRequest";
import { IWorkspaceApiModel } from "../../backendApi/models/response/IWorkspaceApiModel";
import { RedirectHandler } from "../../services/RedirectHandler";

export class UpdateWorkspacePageViewModel implements IViewModel {
    public readonly name: ko.Observable<string> = ko.observable("");
    public readonly url: ko.Observable<string> = ko.observable("");
    public readonly token: ko.Observable<string> = ko.observable("");

    private readonly apiClient: WorkspaceApiClient = new WorkspaceApiClient();

    constructor(params: IUpdateWorkspacePageParams) {
        this.apiClient
            .getWorkspaceById({ workspaceId: params.id } as IGetWorkspaceByIdRequest)
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
        throw new Error("Not implemented");
    }
}