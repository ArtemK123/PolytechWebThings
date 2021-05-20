import * as ko from "knockout";
import { IViewModel } from "src/componentsRegistration/IViewModel";
import { IEditableStepComponentParams } from "src/components/app/workspacePage/editRulePage/EditableStepComponent/IEditableStepComponentParams";
import {StepType} from "src/components/app/workspacePage/models/StepType";

export class EditableStepComponentViewModel implements IViewModel {
    public readonly params: IEditableStepComponentParams;
    public readonly inEditState: ko.Observable<boolean> = ko.observable(false);
    public readonly currentStepType: ko.Observable<string>;
    public readonly stepTypes: string[] = [StepType[StepType.ChangeThingState], StepType[StepType.ExecuteRule]];

    public readonly shouldShowChangeThingStateStepCard: ko.Computed<boolean>;
    public readonly shouldShowExecuteRuleStepCard: ko.Computed<boolean>;

    constructor(params: IEditableStepComponentParams) {
        this.params = params;
        this.currentStepType = ko.observable(StepType[params.step.stepType]);
        this.shouldShowChangeThingStateStepCard = ko.computed(() => this.currentStepType() === StepType[StepType.ChangeThingState]);
        this.shouldShowExecuteRuleStepCard = ko.computed(() => this.currentStepType() === StepType[StepType.ExecuteRule]);
    }

    public handleEditButtonClick(): void {
        this.inEditState(true);
    }

    public handleDeleteButtonClick(): void {
        this.params.deleteAction(this.params.index());
    }

    public confirmEditAction(): void {
        console.log("confirmEditAction");
    }

    public cancelEditAction(): void {
        this.inEditState(false);
    }
}