import * as ko from "knockout";
import { IViewModel } from "src/componentsRegistration/IViewModel";
import { IThingPropertyCardParams } from "src/components/app/workspacePage/workspaceThingsComponent/thingCard/thingPropertyCard/IThingPropertyCardParams";
import { IPropertyApiModel } from "src/backendApi/models/response/things/IPropertyApiModel";

export class ThingPropertyCardViewModel implements IViewModel {
    public readonly inputValue: ko.Observable<string> = ko.observable("");
    private readonly model: IPropertyApiModel;

    constructor(params: IThingPropertyCardParams) {
        this.model = params.model;
        this.inputValue(this.model.value);
    }
}