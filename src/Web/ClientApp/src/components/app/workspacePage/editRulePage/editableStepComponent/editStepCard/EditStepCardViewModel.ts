import * as ko from "knockout";
import { IViewModel } from "src/componentsRegistration/IViewModel";
import { IEditStepCardParams } from "src/components/app/workspacePage/editRulePage/editableStepComponent/editStepCard/IEditStepCardParams";
import { StepType } from "src/components/app/workspacePage/models/StepType";

export class EditStepCardViewModel implements IViewModel {
    public readonly params: IEditStepCardParams;
    public readonly currentStepType: ko.Observable<string>;
    public readonly stepTypes: string[] = [StepType[StepType.ChangeThingState], StepType[StepType.ExecuteRule]];

    public readonly shouldShowChangeThingStateStepCard: ko.Computed<boolean>;
    public readonly shouldShowExecuteRuleStepCard: ko.Computed<boolean>;

    constructor(params: IEditStepCardParams) {
        this.params = params;
        this.currentStepType = ko.observable(StepType[params.step.stepType]);
        this.shouldShowChangeThingStateStepCard = ko.computed(() => this.currentStepType() === StepType[StepType.ChangeThingState]);
        this.shouldShowExecuteRuleStepCard = ko.computed(() => this.currentStepType() === StepType[StepType.ExecuteRule]);
    }

    public handleConfirm(): void {
        console.log("handleConfirm");
    }

    public handleCancel(): void {
        this.params.cancelAction();
    }
}