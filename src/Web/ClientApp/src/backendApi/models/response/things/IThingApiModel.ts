import { IPropertyApiModel } from "src/backendApi/models/response/things/IPropertyApiModel";

export interface IThingApiModel {
    id: string;
    title: string;
    properties: IPropertyApiModel[];
}