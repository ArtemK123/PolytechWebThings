import * as ko from "knockout";
import { IViewModel } from "../../../../componentsRegistration/IViewModel";
import { ICreateRuleModalParams } from "./ICreateRuleModalParams";
import { IRuleModel } from "../models/IRuleModel";

export class CreateRuleModalViewModel implements IViewModel {
    public readonly isVisible: ko.Observable<boolean>;
    public readonly ruleName: ko.Observable<string> = ko.observable("");
    public readonly steps: ko.ObservableArray<string> = ko.observableArray([]);

    private readonly rules: ko.ObservableArray<IRuleModel>;

    constructor(params: ICreateRuleModalParams) {
        this.isVisible = params.isVisible;
        this.rules = params.rules;
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
        this.steps.push(`Step ${this.steps().length + 1}`);
    }

    public removeStep(step: string): void {
        this.steps.remove(step);
    }

    private resetField(): void {
        this.ruleName("");
        this.steps([]);
    }
}