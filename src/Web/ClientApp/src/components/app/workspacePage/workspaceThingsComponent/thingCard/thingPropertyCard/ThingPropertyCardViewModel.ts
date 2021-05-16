import { IViewModel } from "src/componentsRegistration/IViewModel";
import { IThingPropertyCardParams } from "src/components/app/workspacePage/workspaceThingsComponent/thingCard/thingPropertyCard/IThingPropertyCardParams";
import { IPropertyApiModel } from "src/backendApi/models/response/things/IPropertyApiModel";

export class ThingPropertyCardViewModel implements IViewModel {
    private readonly model: IPropertyApiModel;

    constructor(params: IThingPropertyCardParams) {
        this.model = params.model;
    }
}