import * as ko from "knockout";
import { IThingApiModel } from "src/backendApi/models/entities/IThingApiModel";
import { IWorkspaceThingsComponentParams } from "src/components/app/workspacePage/workspaceThingsComponent/IWorkspaceThingsComponentParams";
import { IViewModel } from "src/componentsRegistration/IViewModel";

export class WorkspaceThingsComponentViewModel implements IViewModel {
    public readonly things: ko.ObservableArray<IThingApiModel>;
    public readonly params: IWorkspaceThingsComponentParams;

    constructor(params: IWorkspaceThingsComponentParams) {
        this.params = params;
        this.things = params.things;
    }
}