import * as ko from "knockout";
import { IViewModel } from "../../componentsRegistration/IViewModel";
import { IWorkspacePageParams } from "./IWorkspacePageParams";

export class WorkspacePageViewModel implements IViewModel {
    public readonly id: ko.Observable<number> = ko.observable(-1);

    constructor(params: IWorkspacePageParams) {
        this.id(params.id);
    }
}