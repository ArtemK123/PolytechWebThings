import * as ko from "knockout";
import { IEditRulePageParams } from "src/components/app/workspacePage/EditRulePage/IEditRulePageParams";
import { IViewModel } from "src/componentsRegistration/IViewModel";
import { IOperationResult } from "src/backendApi/models/entities/OperationResult/IOperationResult";
import { IRuleModel } from "src/components/app/workspacePage/models/IRuleModel";
import { StepType } from "src/components/app/workspacePage/models/StepType";
import { IExecuteRuleStepModel } from "src/components/app/workspacePage/models/IExecuteRuleStepModel";
import { IChangeThingStateStepModel } from "src/components/app/workspacePage/models/IChangeThingStateStepModel";
import { OperationStatus } from "src/backendApi/models/entities/OperationResult/OperationStatus";
import {IStepModel} from "src/components/app/workspacePage/models/IStepModel";

export class EditRulePageViewModel implements IViewModel {
    public readonly params: IEditRulePageParams;
    public readonly ruleName: ko.Observable<string> = ko.observable("");
    public readonly steps: ko.ObservableArray<IStepModel> = ko.observableArray([]);

    private ruleId: number;

    constructor(params: IEditRulePageParams) {
        this.params = params;
        if (params.ruleId) {
            this.getHardcodedApiResponse()
                .then((result) => {
                    this.ruleName(result.data.name);
                    this.steps(result.data.steps);
                    this.ruleId = result.data.id;
                });
        }
        this.ruleName();
    }

    public handleConfirm(): void {
        console.log("handleConfirm");
    }

    public handleCancel(): void {
        this.params.cancelAction();
    }

    private generateEmptyRule(): IRuleModel {
        return {
            name: "",
            steps: [],
        } as IRuleModel;
    }

    private getHardcodedApiResponse(): Promise<IOperationResult<IRuleModel>> {
        return Promise.resolve({
            status: OperationStatus.Success,
            data: {
                id: this.params.ruleId,
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
        } as IOperationResult<IRuleModel>);
    }
}