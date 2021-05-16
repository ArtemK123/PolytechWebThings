import * as ko from "knockout";
import { IThingApiModel } from "src/backendApi/models/response/things/IThingApiModel";
import { IWorkspaceThingsComponentParams } from "src/components/app/workspacePage/workspaceThingsComponent/IWorkspaceThingsComponentParams";
import { IViewModel } from "src/componentsRegistration/IViewModel";

export class WorkspaceThingsComponentViewModel implements IViewModel {
    public readonly things: ko.ObservableArray<IThingApiModel>;

    constructor(params: IWorkspaceThingsComponentParams) {
        this.things = params.things;
    }
}