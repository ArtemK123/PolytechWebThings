import { IComponentDescriptor } from "src/componentsRegistration/IComponentDescriptor";

export interface IComponent {
    generateDescriptor(): IComponentDescriptor;
}