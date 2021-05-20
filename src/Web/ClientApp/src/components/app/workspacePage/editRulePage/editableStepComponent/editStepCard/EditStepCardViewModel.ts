import * as ko from "knockout";
import { IViewModel } from "src/componentsRegistration/IViewModel";
import { IEditStepCardParams } from "src/components/app/workspacePage/editRulePage/editableStepComponent/editStepCard/IEditStepCardParams";

export class EditStepCardViewModel implements IViewModel {
    public readonly params: IEditStepCardParams;

    constructor(params: IEditStepCardParams) {
        this.params = params;
    }

    public handleConfirm(): void {
        console.log("handleConfirm");
    }

    public handleCancel(): void {
        this.params.cancelAction();
    }
}