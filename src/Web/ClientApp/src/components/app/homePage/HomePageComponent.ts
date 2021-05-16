import { IComponentDescriptor } from "src/componentsRegistration/IComponentDescriptor";
import { IComponent } from "src/componentsRegistration/IComponent";
import { HomePageViewModel } from "src/components/app/homePage/HomePageViewModel";
import template from "./HomePage.html";

export class HomePageComponent implements IComponent {
    public generateDescriptor(): IComponentDescriptor {
        return { viewModel: HomePageViewModel, template } as IComponentDescriptor;
    }
}