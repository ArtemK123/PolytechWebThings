import * as ko from "knockout";
import { IViewModel } from "src/componentsRegistration/IViewModel";
import { IRuleModel } from "src/components/app/workspacePage/models/IRuleModel";
import { IOperationResult } from "src/backendApi/models/entities/OperationResult/IOperationResult";
import { OperationStatus } from "src/backendApi/models/entities/OperationResult/OperationStatus";
import { IGetRulesResponse } from "src/components/app/workspacePage/models/IGetRulesResponse";
import { RedirectHandler } from "src/services/RedirectHandler";
import { StepType } from "src/components/app/workspacePage/models/StepType";
import { IExecuteRuleStepModel } from "src/components/app/workspacePage/models/IExecuteRuleStepModel";
import { IChangeThingStateStepModel } from "src/components/app/workspacePage/models/IChangeThingStateStepModel";

export class WorkspaceRulesComponentViewModel implements IViewModel {
    public readonly isLoading: ko.Observable<boolean> = ko.observable(true);
    public readonly rules: ko.ObservableArray<IRuleModel> = ko.observableArray([]);

    constructor() {
        this.getHardcodedRules()
            .then((result) => {
                if (result.status === OperationStatus.Success) {
                    this.rules(result.data.rules);
                }
                this.isLoading(false);
            });
    }

    public createRule(): void {
        RedirectHandler.redirect(`${window.location.pathname}/create`);
    }

    private getHardcodedRules(): Promise<IOperationResult<IGetRulesResponse>> {
        return Promise.resolve({
            status: OperationStatus.Success,
            message: "Success",
            data: {
                rules: [
                    {
                        name: "Rule1",
                        steps: [
                            { name: "Step1", stepType: StepType.ExecuteRule, ruleName: "Rule2" } as IExecuteRuleStepModel,
                            {
                                name: "Step2",
                                stepType: StepType.ChangeThingState,
                                thingName: "TestThing",
                                propertyName: "Property1",
                                newPropertyState: "true",
                            } as IChangeThingStateStepModel],
                    } as IRuleModel,
                    {
                        name: "Rule2",
                        steps: [
                            {
                                name: "StepA",
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