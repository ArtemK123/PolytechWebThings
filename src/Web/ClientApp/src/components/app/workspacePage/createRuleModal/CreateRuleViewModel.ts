import * as ko from "knockout";
import { IViewModel } from "../../../../componentsRegistration/IViewModel";
import { ICreateRuleModalParams } from "./ICreateRuleModalParams";

export class CreateRuleModalViewModel implements IViewModel {
    public readonly isVisible: ko.Observable<boolean>;

    constructor(params: ICreateRuleModalParams) {
        this.isVisible = params.isVisible;
    }

    public closeModal(): void {
        this.isVisible(false);
    }
}