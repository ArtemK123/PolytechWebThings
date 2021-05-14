import * as ko from "knockout";
import { IThingApiModel } from "../../../backendApi/models/response/things/IThingApiModel";
import { IWorkspacePageParams } from "./IWorkspacePageParams";
import { IViewModel } from "../../../componentsRegistration/IViewModel";
import { IOperationResult } from "../../../backendApi/models/response/OperationResult/IOperationResult";
import { IGetWorkspaceWithThingsRequest } from "../../../backendApi/models/request/workspace/IGetWorkspaceWithThingsRequest";
import { IGetWorkspaceWithThingsResponse } from "../../../backendApi/models/response/IGetWorkspaceWithThingsResponse";
import { WorkspaceApiClient } from "../../../backendApi/clients/WorkspaceApiClient";
import { OperationStatus } from "../../../backendApi/models/response/OperationResult/OperationStatus";
import { RedirectHandler } from "../../../services/RedirectHandler";

export class WorkspacePageViewModel implements IViewModel {
    public readonly id: ko.Observable<number> = ko.observable(-1);
    public readonly workspaceName: ko.Observable<string> = ko.observable("");
    public readonly things: ko.ObservableArray<IThingApiModel> = ko.observableArray([]);
    public readonly isLoading: ko.Observable<boolean> = ko.observable(true);

    private readonly workspaceApiClient: WorkspaceApiClient = new WorkspaceApiClient();

    constructor(params: IWorkspacePageParams) {
        this.id(params.id);
        this.workspaceApiClient.getWorkspaceWithThingsRequest({ workspaceId: this.id() } as IGetWorkspaceWithThingsRequest)
            .then((response: IOperationResult<IGetWorkspaceWithThingsResponse>) => {
                if (response.status !== OperationStatus.Success) {
                    RedirectHandler.redirect("/");
                }
                this.isLoading(false);
                this.workspaceName(response.data.workspace.name);
                this.things(response.data.things);
            });
    }
}