import * as ko from "knockout";
import { IViewModel } from "src/componentsRegistration/IViewModel";
import { IChangeThingStateStepEditCardParams } from "src/components/app/workspacePage/editRulePage/editableStepComponent/changeThingStateStepEditCard/IChangeThingStateStepEditCardParams";
import { IChangeThingStateStepModel } from "src/components/app/workspacePage/models/IChangeThingStateStepModel";
import { StepType } from "src/components/app/workspacePage/models/StepType";

export class ChangeThingStateStepEditCardViewModel implements IViewModel {
    public readonly params: IChangeThingStateStepEditCardParams;
    public readonly thingName: ko.Observable<string>;
    public readonly propertyName: ko.Observable<string>;
    public readonly newPropertyState: ko.Observable<string>;

    constructor(params: IChangeThingStateStepEditCardParams) {
        this.params = params;
        this.thingName = ko.observable(params.step.thingName);
        this.propertyName = ko.observable(params.step.propertyName);
        this.newPropertyState = ko.observable(params.step.newPropertyState);
    }

    public handleConfirm(): void {
        const updatedStep: IChangeThingStateStepModel = {
            stepType: StepType.ChangeThingState,
            thingName: this.thingName(),
            propertyName: this.propertyName(),
            newPropertyState: this.newPropertyState(),
        } as IChangeThingStateStepModel;

        console.log(`handleConfirm ${JSON.stringify(updatedStep)}`);
    }

    public handleCancel(): void {
        this.params.cancelAction();
    }
}