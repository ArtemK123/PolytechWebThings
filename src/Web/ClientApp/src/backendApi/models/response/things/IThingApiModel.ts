import { IPropertyApiModel } from "src/backendApi/models/response/things/IPropertyApiModel";

export interface IThingApiModel {
    title: string;
    properties: IPropertyApiModel[];
}