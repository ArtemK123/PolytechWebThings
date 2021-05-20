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
import { IGetRulesResponse } from "src/components/app/workspacePage/models/IGetRulesResponse";
import { StepType } from "src/components/app/workspacePage/models/StepType";
import { IExecuteRuleStepModel } from "src/components/app/workspacePage/models/IExecuteRuleStepModel";
import { IChangeThingStateStepModel } from "src/components/app/workspacePage/models/IChangeThingStateStepModel";

export class WorkspacePageViewModel implements IViewModel {
    public readonly id: number;
    public readonly workspaceName: ko.Observable<string> = ko.observable("");
    public readonly things: ko.ObservableArray<IThingApiModel> = ko.observableArray([]);
    public readonly isLoading: ko.Computed<boolean>;
    public readonly isCreateRuleFormOpened: ko.Observable<boolean> = ko.observable(false);
    public readonly rules: ko.ObservableArray<IRuleModel> = ko.observableArray([]);

    private readonly loadingProcessCounter: ko.Observable<number> = ko.observable(0);

    private readonly thingsApiClient: ThingsApiClient = new ThingsApiClient();

    public readonly routes: IRoute[] = [
        RouteGenerator.generate(/\/things$/, () => "<workspace-things-component params='{ things: $parent.things, workspaceId: $parent.id }'></workspace-things-component>"),
        RouteGenerator.generate(
            /\/rules$/,
            () => "<workspace-rules-component"
                + " params='{ rules: $parent.rules, editRuleAction: $parent.editRule.bind($parent), deleteRuleAction: $parent.deleteRule.bind($parent) }'></workspace-rules-component>",
        ),
        RouteGenerator.generate(/\/rules\/create$/, this.generateCreateRulePageTag),
        RouteGenerator.generate(/\/rules\/\d+\/edit$/, this.generateEditRulePageTag),
        RouteGenerator.generate(/\//, () => ""),
    ];

    constructor(params: IWorkspacePageParams) {
        this.id = params.id;
        this.isLoading = ko.computed(() => this.loadingProcessCounter() > 0);
        this.fetchThings();
        this.fetchRules();
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

    public generateCreateRulePageTag(): string {
        return `
            <edit-rule-page params='{
                confirmAction: $parent.confirmEditRuleAction.bind($parent),
                rules: $parent.rules,
                things: $parent.things,
                cancelAction: $parent.cancelEditRuleAction.bind($parent)
            }'></edit-rule-page>`;
    }

    public generateEditRulePageTag(route: string): string {
        const chunks: string[] = route.split("/");
        const ruleId: string = chunks[4];
        return `
            <edit-rule-page params='{
                ruleId: ${ruleId},
                rules: $parent.rules,
                things: $parent.things,
                confirmAction: $parent.confirmEditRuleAction.bind($parent), 
                cancelAction: $parent.cancelEditRuleAction.bind($parent)
            }'></edit-rule-page>`;
    }

    public confirmEditRuleAction(updatedRule: IRuleModel): void {
        if (!updatedRule.name) {
            alert("Please, provide rule name");
        }
        if (updatedRule.steps.length === 0) {
            alert("Please, add at least one step");
        }
        alert("confirmEditRuleAction");
    }

    public cancelEditRuleAction(): void {
        RedirectHandler.redirect(`/workspaces/${this.id}/rules`);
    }

    public editRule(rule: IRuleModel): void {
        RedirectHandler.redirect(`/workspaces/${this.id}/rules/${rule.id}/edit`);
    }

    public deleteRule(rule: IRuleModel): void {
        const confirmation: boolean = confirm("Do you want to delete this rule?");
        if (confirmation) {
            this.rules.remove(rule);
            console.log(`Deleted rule ${rule}`);
        }
    }

    private fetchThings() {
        this.loadingProcessCounter(this.loadingProcessCounter() + 1);
        this.thingsApiClient.getWorkspaceWithThingsRequest({ workspaceId: this.id } as IGetWorkspaceWithThingsRequest)
            .then((response: IOperationResult<IGetWorkspaceWithThingsResponse>) => {
                if (response.status !== OperationStatus.Success) {
                    RedirectHandler.redirect("/");
                }
                this.loadingProcessCounter(this.loadingProcessCounter() - 1);
                this.workspaceName(response.data.workspace.name);
                this.things(response.data.things);
            });
    }

    private fetchRules() {
        this.loadingProcessCounter(this.loadingProcessCounter() + 1);
        this.getHardcodedRules()
            .then((result) => {
                if (result.status === OperationStatus.Success) {
                    this.rules(result.data.rules);
                }
                this.loadingProcessCounter(this.loadingProcessCounter() - 1);
            });
    }

    private getHardcodedRules(): Promise<IOperationResult<IGetRulesResponse>> {
        return Promise.resolve({
            status: OperationStatus.Success,
            message: "Success",
            data: {
                rules: [
                    {
                        id: 1,
                        name: "Rule1",
                        steps: [
                            { stepType: StepType.ExecuteRule, ruleName: "Rule2" } as IExecuteRuleStepModel,
                            {
                                stepType: StepType.ChangeThingState,
                                thingName: "TestThing",
                                propertyName: "Property1",
                                newPropertyState: "true",
                            } as IChangeThingStateStepModel],
                    } as IRuleModel,
                    {
                        id: 2,
                        name: "Rule2",
                        steps: [
                            {
                                stepType: StepType.ChangeThingState,
                                thingName: "OtherThing",
                                propertyName: "On/Off",
                                newPropertyState: "true",
                            } as IChangeThingStateStepModel],
                    } as IRuleModel,
                ],
            } as IGetRulesResponse,
        } as IOperationResult<IGetRulesResponse>);
    }
}