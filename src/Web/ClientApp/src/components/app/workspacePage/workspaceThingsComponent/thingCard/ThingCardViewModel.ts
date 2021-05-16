import * as ko from "knockout";
import { IPropertyApiModel } from "src/backendApi/models/response/things/IPropertyApiModel";
import { IThingCardParams } from "src/components/app/workspacePage/workspaceThingsComponent/thingCard/IThingCardParams";
import { IViewModel } from "src/componentsRegistration/IViewModel";

export class ThingCardViewModel implements IViewModel {
    public readonly title: ko.Observable<string> = ko.observable("");
    public readonly properties: ko.ObservableArray<IPropertyApiModel> = ko.observableArray([]);
    public readonly isCollapsed: ko.Observable<boolean> = ko.observable(true);

    constructor(params: IThingCardParams) {
        this.title(params.model.title);
        this.properties(params.model.properties);
    }

    public changeCollapseState(): void {
        this.isCollapsed(!this.isCollapsed());
    }
}