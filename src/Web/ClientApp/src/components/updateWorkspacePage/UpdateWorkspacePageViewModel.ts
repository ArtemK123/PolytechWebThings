import * as ko from "knockout";
import { IViewModel } from "../../componentsRegistration/IViewModel";
import { WorkspaceApiClient } from "../../backendApi/clients/WorkspaceApiClient";
import { IUpdateWorkspacePageParams } from "./IUpdateWorkspacePageParams";
import { IGetWorkspaceByIdRequest } from "../../backendApi/models/request/workspace/IGetWorkspaceByIdRequest";

export class UpdateWorkspacePageViewModel implements IViewModel {
    public readonly name: ko.Observable<string> = ko.observable("");
    public readonly url: ko.Observable<string> = ko.observable("");
    public readonly token: ko.Observable<string> = ko.observable("");

    private readonly apiClient: WorkspaceApiClient = new WorkspaceApiClient();

    constructor(params: IUpdateWorkspacePageParams) {
        this.apiClient.getWorkspaceById({ workspaceId: params.id } as IGetWorkspaceByIdRequest).then(() => { throw new Error("Not implemented"); });
    }

    public handleSubmit(): void {
        throw new Error("Not implemented");
    }
}