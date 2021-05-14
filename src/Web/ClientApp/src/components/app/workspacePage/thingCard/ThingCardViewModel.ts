import * as ko from "knockout";
import { IViewModel } from "../../../../componentsRegistration/IViewModel";
import { IThingCardParams } from "./IThingCardParams";
import { IPropertyApiModel } from "../../../../backendApi/models/response/things/IPropertyApiModel";

export class ThingCardViewModel implements IViewModel {
    public readonly title: ko.Observable<string> = ko.observable("");
    public readonly properties: ko.ObservableArray<IPropertyApiModel> = ko.observableArray([]);

    constructor(params: IThingCardParams) {
        this.title(params.model.title);
        this.properties(params.model.properties);
    }

    public generatePropertyText(property: IPropertyApiModel) {
        const propertyText: string = `${property.name}: ${property.value}`;
        if (property.unit) {
            return `${propertyText} ${property.unit}`;
        }
        return propertyText;
    }
}