import * as ko from "knockout";
import { IViewModel } from "src/componentsRegistration/IViewModel";
import { IChangeThingStateStepEditCardParams }
    from "src/components/app/workspacePage/editRulePage/editableStepComponent/changeThingStateStepEditCard/IChangeThingStateStepEditCardParams";

export class ChangeThingStateStepEditCardViewModel implements IViewModel {
    public readonly params: IChangeThingStateStepEditCardParams;

    constructor(params: IChangeThingStateStepEditCardParams) {
        this.params = params;
    }

    public handleConfirm(): void {
        console.log("handleConfirm");
    }

    public handleCancel(): void {
        this.params.cancelAction();
    }
}