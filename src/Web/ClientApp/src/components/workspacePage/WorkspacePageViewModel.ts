import * as ko from "knockout";
import { IViewModel } from "../../componentsRegistration/IViewModel";
import { IWorkspacePageParams } from "./IWorkspacePageParams";

export class WorkspacePageViewModel implements IViewModel {
    public readonly id: ko.Observable<number> = ko.observable(-1);
    public readonly workspaceName: ko.Observable<string> = ko.observable("Test");
    public readonly things: ko.ObservableArray<string> = ko.observableArray(["Test"]);

    constructor(params: IWorkspacePageParams) {
        this.id(params.id);
    }
}