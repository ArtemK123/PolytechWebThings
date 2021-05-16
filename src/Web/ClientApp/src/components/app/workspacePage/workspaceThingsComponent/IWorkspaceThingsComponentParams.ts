import * as ko from "knockout";
import { IThingApiModel } from "src/backendApi/models/response/things/IThingApiModel";

export interface IWorkspaceThingsComponentParams {
    things: ko.ObservableArray<IThingApiModel>;
}