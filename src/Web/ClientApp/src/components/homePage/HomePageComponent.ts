import template from "./BackendConnectionCheck.html";
import { HomePageViewModel } from "./HomePageViewModel";
import { IComponentDescriptor } from "../../componentsRegistration/IComponentDescriptor";
import { IComponent } from "../../componentsRegistration/IComponent";

export class HomePageComponent implements IComponent {
    public generateDescriptor(): IComponentDescriptor {
        return { viewModel: HomePageViewModel, template } as IComponentDescriptor;
    }
}