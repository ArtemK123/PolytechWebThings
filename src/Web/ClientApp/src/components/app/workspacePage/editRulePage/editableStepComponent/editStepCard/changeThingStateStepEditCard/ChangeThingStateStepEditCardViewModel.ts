import * as ko from "knockout";
import { IViewModel } from "src/componentsRegistration/IViewModel";
import { IChangeThingStateStepEditCardParams }
    from "src/components/app/workspacePage/editRulePage/editableStepComponent/editStepCard/changeThingStateStepEditCard/IChangeThingStateStepEditCardParams";

export class ChangeThingStateStepEditCardViewModel implements IViewModel {
    public readonly params: IChangeThingStateStepEditCardParams;
    public readonly titleText: ko.Computed<string>;

    constructor(params: IChangeThingStateStepEditCardParams) {
        this.params = params;
        this.titleText = ko.computed(() => "Change thing state");
    }
}