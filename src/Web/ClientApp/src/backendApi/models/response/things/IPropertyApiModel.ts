import { GatewayValueType } from "./GatewayValueType";
import { ILinkApiModel } from "./ILinkApiModel";

export interface IPropertyApiModel {
    name: string | null;
    value: string | null;
    visible: boolean;
    title: string | null;
    valueType: GatewayValueType;
    propertyType: string | null;
    links: ILinkApiModel[];
    readOnly: boolean;
    unit: string | null;
    minimum: number | null;
    maximum: number | null;
    allowedValues: string[] | null;
}