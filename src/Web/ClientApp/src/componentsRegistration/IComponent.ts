import {IComponentDescriptor} from "./IComponentDescriptor";

export interface IComponent {
    generateDescriptor(): IComponentDescriptor;
}