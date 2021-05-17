import * as ko from "knockout";
import { IPropertyApiModel } from "src/backendApi/models/response/things/IPropertyApiModel";
import { IThingCardParams } from "src/components/app/workspacePage/workspaceThingsComponent/thingCard/IThingCardParams";
import { IViewModel } from "src/componentsRegistration/IViewModel";
import { IThingApiModel } from "src/backendApi/models/response/things/IThingApiModel";

export class ThingCardViewModel implements IViewModel {
    public readonly title: ko.Observable<string> = ko.observable("");
    public readonly properties: ko.ObservableArray<IPropertyApiModel> = ko.observableArray([]);
    public readonly isCollapsed: ko.Observable<boolean> = ko.observable(true);
    public readonly model: IThingApiModel;
    public readonly params: IThingCardParams;

    constructor(params: IThingCardParams) {
        this.params = params;
        this.model = params.model;
        this.title(params.model.title);
        this.properties(params.model.properties);
    }

    public changeCollapseState(): void {
        this.isCollapsed(!this.isCollapsed());
    }
}