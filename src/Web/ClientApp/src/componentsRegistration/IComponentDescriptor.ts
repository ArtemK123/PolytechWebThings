import { IViewModel } from "./IViewModel";

export interface IComponentDescriptor {
    viewModel: IViewModel;
    template: string;
}