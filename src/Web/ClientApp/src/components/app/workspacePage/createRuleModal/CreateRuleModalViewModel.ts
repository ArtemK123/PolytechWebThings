import * as ko from "knockout";
import { ICreateRuleModalParams } from "src/components/app/workspacePage/createRuleModal/ICreateRuleModalParams";
import { IThingApiModel } from "src/backendApi/models/response/things/IThingApiModel";
import { IRuleModel } from "src/components/app/workspacePage/models/IRuleModel";
import { IViewModel } from "src/componentsRegistration/IViewModel";

export class CreateRuleModalViewModel implements IViewModel {
    public readonly isVisible: ko.Observable<boolean>;
    public readonly ruleName: ko.Observable<string> = ko.observable("");
    public readonly steps: ko.ObservableArray<string> = ko.observableArray([]);
    public readonly isCreateStepModalVisible: ko.Observable<boolean> = ko.observable(false);
    public readonly things: ko.ObservableArray<IThingApiModel>;

    private readonly rules: ko.ObservableArray<IRuleModel>;

    constructor(params: ICreateRuleModalParams) {
        this.isVisible = params.isVisible;
        this.rules = params.rules;
        this.things = params.things;
    }

    public closeModal(): void {
        this.isVisible(false);
    }

    public addRule(): void {
        const newRule: IRuleModel = {
            name: this.ruleName(),
            steps: this.steps(),
        } as IRuleModel;
        this.rules.push(newRule);
        this.isVisible(false);
        this.resetField();
    }

    public addStep(): void {
        this.isCreateStepModalVisible(true);
    }

    public removeStep(step: string): void {
        this.steps.remove(step);
    }

    private resetField(): void {
        this.ruleName("");
        this.steps([]);
    }
}