import * as ko from "knockout";
import { IViewModel } from "../../../../../componentsRegistration/IViewModel";
import { ICreateStepModalParams } from "./ICreateStepModalParams";

export class CreateStepModalViewModel implements IViewModel {
    public readonly isVisible: ko.Observable<boolean>;

    private readonly steps: ko.ObservableArray<string>;

    constructor(params: ICreateStepModalParams) {
        this.isVisible = params.isVisible;
        this.steps = params.steps;
    }

    public add(): void {
        this.steps.push(`Step ${this.steps().length + 1}`);
    }

    public cancel(): void {
        this.isVisible(false);
    }
}