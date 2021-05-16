import { IViewModel } from "src/componentsRegistration/IViewModel";

export interface IComponentDescriptor {
    viewModel: IViewModel;
    template: string;
}