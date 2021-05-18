import * as ko from "knockout";
import { IThingApiModel } from "src/backendApi/models/entities/IThingApiModel";
import { IRuleModel } from "src/components/app/workspacePage/models/IRuleModel";
import { RedirectHandler } from "src/services/RedirectHandler";
import { IWorkspacePageParams } from "src/components/app/workspacePage/IWorkspacePageParams";
import { OperationStatus } from "src/backendApi/models/entities/OperationResult/OperationStatus";
import { RouteGenerator } from "src/components/common/router/RouteGenerator";
import { IRoute } from "src/components/common/router/IRoute";
import { IViewModel } from "src/componentsRegistration/IViewModel";
import { IOperationResult } from "src/backendApi/models/entities/OperationResult/IOperationResult";
import { ThingsApiClient } from "src/backendApi/clients/ThingsApiClient";
import { IGetWorkspaceWithThingsResponse } from "src/backendApi/models/response/IGetWorkspaceWithThingsResponse";
import { IGetWorkspaceWithThingsRequest } from "src/backendApi/models/request/things/IGetWorkspaceWithThingsRequest";

export class WorkspacePageViewModel implements IViewModel {
    public readonly id: number;
    public readonly workspaceName: ko.Observable<string> = ko.observable("");
    public readonly things: ko.ObservableArray<IThingApiModel> = ko.observableArray([]);
    public readonly isLoading: ko.Observable<boolean> = ko.observable(true);
    public readonly isCreateRuleFormOpened: ko.Observable<boolean> = ko.observable(false);
    public readonly rules: ko.ObservableArray<IRuleModel> = ko.observableArray([]);

    private readonly thingsApiClient: ThingsApiClient = new ThingsApiClient();

    public readonly routes: IRoute[] = [
        RouteGenerator.generate(/\/things/, () => "<workspace-things-component params=\"{ things: $parent.things, workspaceId: $parent.id }\"></workspace-things-component>"),
        RouteGenerator.generate(/\/rules/, () => "<workspace-rules-component></workspace-rules-component>"),
        RouteGenerator.generate(/\//, () => ""),
    ];

    constructor(params: IWorkspacePageParams) {
        this.id = params.id;
        this.thingsApiClient.getWorkspaceWithThingsRequest({ workspaceId: this.id } as IGetWorkspaceWithThingsRequest)
            .then((response: IOperationResult<IGetWorkspaceWithThingsResponse>) => {
                if (response.status !== OperationStatus.Success) {
                    RedirectHandler.redirect("/");
                }
                this.isLoading(false);
                this.workspaceName(response.data.workspace.name);
                this.things(response.data.things);
            });
    }

    public generateMenuHref(menuItem: string) {
        return `/workspaces/${this.id}/${menuItem}`;
    }

    public generateRuleElement(rule: IRuleModel) {
        const ruleElement = rule.steps.reduce((prev, current, index) => `${prev}<br/><span>${index + 1}. ${current}</span>`, `<span>${rule.name}:</span>`);
        return `${ruleElement}<hr/>`;
    }

    public createRule(): void {
        this.isCreateRuleFormOpened(true);
    }
}