import * as ko from "knockout";
import { IEditRulePageParams } from "src/components/app/workspacePage/EditRulePage/IEditRulePageParams";
import { IViewModel } from "src/componentsRegistration/IViewModel";
import { IOperationResult } from "src/backendApi/models/entities/OperationResult/IOperationResult";
import { IRuleModel } from "src/components/app/workspacePage/models/IRuleModel";
import { StepType } from "src/components/app/workspacePage/models/StepType";
import { IExecuteRuleStepModel } from "src/components/app/workspacePage/models/IExecuteRuleStepModel";
import { IChangeThingStateStepModel } from "src/components/app/workspacePage/models/IChangeThingStateStepModel";
import { OperationStatus } from "src/backendApi/models/entities/OperationResult/OperationStatus";
import { IStepModel } from "src/components/app/workspacePage/models/IStepModel";

export class EditRulePageViewModel implements IViewModel {
    public readonly params: IEditRulePageParams;
    public readonly ruleName: ko.Observable<string> = ko.observable("");
    public readonly steps: ko.ObservableArray<IStepModel> = ko.observableArray([]);
    public readonly availableRuleNames: ko.Computed<string[]>;
    public readonly shouldShowNewStepInputs: ko.Observable<boolean> = ko.observable(false);

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
        this.availableRuleNames = ko.computed(() => this.params.rules().map((rule: IRuleModel) => rule.name));
    }

    public handleConfirm(): void {
        const newRule: IRuleModel = {
            id: this.ruleId,
            name: this.ruleName(),
            steps: this.steps(),
        };
        this.params.confirmAction(newRule);
    }

    public handleCancel(): void {
        this.params.cancelAction();
    }

    public addStep(): void {
        this.shouldShowNewStepInputs(true);
    }

    public updateStep(stepIndex: number, updatedStep: IStepModel): void {
        const currentSteps: IStepModel[] = this.steps();
        currentSteps[stepIndex] = updatedStep;
        this.steps(currentSteps);
    }

    public deleteStep(stepIndex: number):void {
        const removedStep: IStepModel = this.steps()[stepIndex];
        if (!removedStep) {
            throw new Error("Can not find step to remove");
        }
        this.steps.remove(this.steps()[stepIndex]);
    }

    private getHardcodedApiResponse(): Promise<IOperationResult<IRuleModel>> {
        return Promise.resolve({
            status: OperationStatus.Success,
            data: {
                id: 1,
                name: "Rule1",
                steps: [
                    { stepType: StepType.ExecuteRule, ruleName: "Rule2" } as IExecuteRuleStepModel,
                    {
                        stepType: StepType.ChangeThingState,
                        thingName: "Virtual Dimmable Color Light",
                        propertyName: "on",
                        newPropertyState: "true",
                    } as IChangeThingStateStepModel],
            } as IRuleModel,
        } as IOperationResult<IRuleModel>);
    }
}