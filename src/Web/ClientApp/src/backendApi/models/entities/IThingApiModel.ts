import { IPropertyApiModel } from "./IPropertyApiModel";

export interface IThingApiModel {
    id: string;
    title: string;
    properties: IPropertyApiModel[];
}