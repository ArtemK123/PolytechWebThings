import { IPropertyApiModel } from "./IPropertyApiModel";

export interface IThingApiModel {
    title: string;
    properties: IPropertyApiModel[];
}