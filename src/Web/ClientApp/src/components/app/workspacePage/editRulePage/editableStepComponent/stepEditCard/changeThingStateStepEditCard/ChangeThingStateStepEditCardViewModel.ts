import * as ko from "knockout";
import { IViewModel } from "src/componentsRegistration/IViewModel";
import { IChangeThingStateStepEditCardParams } from "src/components/app/workspacePage/editRulePage/editableStepComponent/stepEditCard/changeThingStateStepEditCard/IChangeThingStateStepEditCardParams";
import { IChangeThingStateStepModel } from "src/components/app/workspacePage/models/IChangeThingStateStepModel";
import { StepType } from "src/components/app/workspacePage/models/StepType";
import { IThingApiModel } from "src/backendApi/models/entities/IThingApiModel";
import { IPropertyApiModel } from "src/backendApi/models/entities/IPropertyApiModel";

export class ChangeThingStateStepEditCardViewModel implements IViewModel {
    public readonly params: IChangeThingStateStepEditCardParams;
    public readonly thingNames: ko.Computed<string[]>;
    public readonly propertyNames: ko.Observable<string[]>;
    public readonly selectedThingName: ko.Observable<string>;
    public readonly selectedPropertyName: ko.Observable<string>;
    public readonly newPropertyState: ko.Observable<string>;

    constructor(params: IChangeThingStateStepEditCardParams) {
        this.params = params;

        this.thingNames = ko.computed(() => params.things().map((thing) => thing.title));
        const currentThingName: string = params.step.thingName ? params.step.thingName : "";
        this.selectedThingName = ko.observable(currentThingName);

        const currentPropertyName = params.step.propertyName ? params.step.thingName : "";
        this.selectedPropertyName = ko.observable(currentPropertyName);
        this.propertyNames = ko.observable(this.getPropertyNames(this.getThing(this.selectedThingName())));

        this.newPropertyState = ko.observable("");

        if (params.step.newPropertyState) {
            this.newPropertyState(params.step.newPropertyState);
        } else if (this.getProperty(this.selectedPropertyName())) {
            this.newPropertyState(this.getProperty(this.selectedPropertyName()).defaultValue);
        }

        this.selectedThingName.subscribe((newThingName: string) => {
            this.propertyNames(this.getPropertyNames(this.getThing(newThingName)));
            this.selectedPropertyName(this.propertyNames()[0] ?? "");
        });

        this.selectedPropertyName.subscribe((newPropertyValue) => {
            this.newPropertyState(this.getProperty(newPropertyValue).defaultValue ?? "");
        });
    }

    public handleConfirm(): void {
        const updatedStep: IChangeThingStateStepModel = {
            stepType: StepType.ChangeThingState,
            thingName: this.selectedThingName(),
            propertyName: this.selectedPropertyName(),
            newPropertyState: this.newPropertyState(),
        } as IChangeThingStateStepModel;

        this.params.confirmEditAction(updatedStep);
    }

    public handleCancel(): void {
        this.params.cancelEditAction();
    }

    private getThing(thingName: string): IThingApiModel | undefined {
        return this.params.things().find((thing) => thing.title === thingName);
    }

    private getProperty(propertyName: string): IPropertyApiModel | undefined {
        const selectedThing = this.getThing(this.selectedThingName());
        if (!selectedThing) {
            return undefined;
        }
        return selectedThing.properties.find((property) => property.name === propertyName);
    }

    private getPropertyNames(thing: IThingApiModel | undefined): string[] {
        if (!thing) {
            return [];
        }

        return thing.properties.map((property) => property.name);
    }
}