import * as ko from "knockout";
import { IThingApiModel } from "src/backendApi/models/entities/IThingApiModel";
import { IRuleModel } from "src/components/app/workspacePage/models/IRuleModel";
import { IPropertyApiModel } from "src/backendApi/models/entities/IPropertyApiModel";
import { IViewModel } from "src/componentsRegistration/IViewModel";
import { ICreateStepModalParams } from "src/components/app/workspacePage/createRuleModal/CreateStepModal/ICreateStepModalParams";

export class CreateStepModalViewModel implements IViewModel {
    public readonly isVisible: ko.Observable<boolean>;
    public readonly actionTypes: string[] = ["Execute other rule", "Change thing state"];
    public readonly selectedActionType: ko.Observable<string> = ko.observable("Execute other rule");
    public readonly ruleNames: ko.Computed<string[]>;
    public readonly thingNames: ko.Computed<string[]>;
    public readonly propertyNames: ko.Computed<string[]>;
    public readonly selectedRuleName: ko.Observable<string>;
    public readonly selectedThingName: ko.Observable<string>;
    public readonly selectedPropertyName: ko.Observable<string>;
    public readonly newPropertyValue: ko.Observable<string> = ko.observable("");

    private readonly steps: ko.ObservableArray<string>;
    private readonly rules: ko.ObservableArray<IRuleModel>;
    private readonly things: ko.ObservableArray<IThingApiModel>;
    private readonly properties: ko.Computed<IPropertyApiModel[]>;

    constructor(params: ICreateStepModalParams) {
        this.isVisible = params.isVisible;
        this.steps = params.steps;
        this.rules = params.rules;
        this.things = params.things;

        this.ruleNames = ko.computed(() => this.rules().map((rule) => rule.name));
        this.thingNames = ko.computed(() => this.things().map((thing) => thing.title));
        this.selectedThingName = ko.observable(this.thingNames()[0]);

        this.properties = ko.computed(() => this.things().find((thing) => thing.title === this.selectedThingName()).properties);
        this.propertyNames = ko.computed(() => this.properties().map((property) => property.name));

        this.selectedRuleName = ko.observable(this.ruleNames()[0]);
        this.selectedPropertyName = ko.observable(this.propertyNames()[0]);
    }

    public add(): void {
        const actionType: string = this.selectedActionType();
        if (this.selectedActionType() === this.actionTypes[0]) {
            this.steps.push(`Execute rule ${this.selectedRuleName()}`);
        } else if (this.selectedActionType() === this.actionTypes[1]) {
            this.steps.push(`Change property's value ${this.selectedThingName()}.${this.selectedPropertyName()} -> ${this.newPropertyValue()}`);
        } else {
            throw new Error(`Action type ${actionType} is not supported`);
        }

        this.isVisible(false);
    }

    public cancel(): void {
        this.isVisible(false);
    }
}