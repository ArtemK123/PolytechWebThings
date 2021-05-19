import * as ko from "knockout";
import { IEditRulePageParams } from "src/components/app/workspacePage/EditRulePage/IEditRulePageParams";
import { IViewModel } from "src/componentsRegistration/IViewModel";

export class EditRulePageViewModel implements IViewModel {
    public readonly params: IEditRulePageParams;
    public readonly ruleName: ko.Observable<string> = ko.observable("");

    constructor(params: IEditRulePageParams) {
        this.params = params;
        this.ruleName(params.rule().name);
    }

    public handleConfirm(): void {
        console.log("handleConfirm");
    }

    public handleCancel(): void {
        console.log("handleCancel");
    }
}