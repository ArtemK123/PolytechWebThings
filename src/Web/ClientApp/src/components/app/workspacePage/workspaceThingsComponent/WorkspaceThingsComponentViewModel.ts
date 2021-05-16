import * as ko from "knockout";
import { IViewModel } from "../../../../componentsRegistration/IViewModel";
import { IWorkspaceThingsComponentParams } from "./IWorkspaceThingsComponentParams";
import { IThingApiModel } from "../../../../backendApi/models/response/things/IThingApiModel";

export class WorkspaceThingsComponentViewModel implements IViewModel {
    public readonly things: ko.ObservableArray<IThingApiModel>;

    constructor(params: IWorkspaceThingsComponentParams) {
        this.things = params.things;
    }
}