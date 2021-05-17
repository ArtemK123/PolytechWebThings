export interface IChangePropertyStateRequest {
    workspaceId: number;
    thingId: string;
    propertyName: string;
    newPropertyValue: string | null;
}