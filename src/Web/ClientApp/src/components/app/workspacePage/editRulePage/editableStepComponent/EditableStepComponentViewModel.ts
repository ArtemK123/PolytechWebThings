import * as ko from "knockout";
import { IViewModel } from "src/componentsRegistration/IViewModel";
import { IEditableStepComponentParams } from "src/components/app/workspacePage/editRulePage/EditableStepComponent/IEditableStepComponentParams";

export class EditableStepComponentViewModel implements IViewModel {
    public readonly params: IEditableStepComponentParams;
    public readonly inEditState: ko.Observable<boolean> = ko.observable(false);

    constructor(params: IEditableStepComponentParams) {
        this.params = params;
    }

    public handleEditButtonClick(): void {
        this.inEditState(true);
    }

    public handleDeleteButtonClick(): void {
        this.params.deleteAction(this.params.index());
    }

    public confirmEditAction(): void {
        console.log("confirmEditAction");
    }

    public cancelEditAction(): void {
        this.inEditState(false);
    }
}