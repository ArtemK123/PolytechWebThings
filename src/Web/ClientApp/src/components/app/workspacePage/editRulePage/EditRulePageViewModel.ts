import * as ko from "knockout";
import { IEditRulePageParams } from "src/components/app/workspacePage/EditRulePage/IEditRulePageParams";
import { IViewModel } from "src/componentsRegistration/IViewModel";

export class EditRulePageViewModel implements IViewModel {
    public readonly params: IEditRulePageParams;

    constructor(params: IEditRulePageParams) {
        this.params = params;
    }
}