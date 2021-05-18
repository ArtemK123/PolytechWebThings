import * as ko from "knockout";
import { IThingApiModel } from "src/backendApi/models/entities/IThingApiModel";

export interface IWorkspaceThingsComponentParams {
    things: ko.ObservableArray<IThingApiModel>;
    workspaceId: number;
}