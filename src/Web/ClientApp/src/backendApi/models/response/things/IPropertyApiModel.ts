import { ILinkApiModel } from "src/backendApi/models/response/things/ILinkApiModel";
import { GatewayValueType } from "src/backendApi/models/response/things/GatewayValueType";

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