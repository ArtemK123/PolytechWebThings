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
import { IRuleModel } from "./models/IRuleModel";
import { RouteGenerator } from "../../common/router/RouteGenerator";
import { IRoute } from "../../common/router/IRoute";

export class WorkspacePageViewModel implements IViewModel {
    public readonly id: ko.Observable<number> = ko.observable(-1);
    public readonly workspaceName: ko.Observable<string> = ko.observable("");
    public readonly things: ko.ObservableArray<IThingApiModel> = ko.observableArray([]);
    public readonly isLoading: ko.Observable<boolean> = ko.observable(true);
    public readonly isCreateRuleFormOpened: ko.Observable<boolean> = ko.observable(false);

    public readonly routes: IRoute[] = [
        RouteGenerator.generate(/\/things/, () => "<workspace-things-component></workspace-things-component>"),
        RouteGenerator.generate(/\/rules/, () => "<workspace-rules-component></workspace-rules-component>"),
        RouteGenerator.generate(/\//, () => ""),
    ];

    constructor(params: IWorkspacePageParams) {
        this.id(params.id);
        this.isLoading(false);
    }

    public generateMenuHref(menuItem: string) {
        return `/workspaces/${this.id()}/${menuItem}`;
    }
}