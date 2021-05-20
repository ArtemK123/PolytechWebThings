import * as ko from "knockout";
import { IViewModel } from "src/componentsRegistration/IViewModel";
import { StepType } from "src/components/app/workspacePage/models/StepType";
import { IStepEditCardParams } from "src/components/app/workspacePage/editRulePage/editableStepComponent/stepEditCard/IStepEditCardParams";

export class StepEditCardViewModel implements IViewModel {
    public readonly params: IStepEditCardParams;
    public readonly currentStepType: ko.Observable<string>;
    public readonly stepTypes: string[] = [StepType[StepType.ChangeThingState], StepType[StepType.ExecuteRule]];

    public readonly shouldShowChangeThingStateStepCard: ko.Computed<boolean>;
    public readonly shouldShowExecuteRuleStepCard: ko.Computed<boolean>;

    constructor(params: IStepEditCardParams) {
        this.params = params;
        this.currentStepType = ko.observable(StepType[params.step.stepType]);
        this.shouldShowChangeThingStateStepCard = ko.computed(() => this.currentStepType() === StepType[StepType.ChangeThingState]);
        this.shouldShowExecuteRuleStepCard = ko.computed(() => this.currentStepType() === StepType[StepType.ExecuteRule]);
    }
}