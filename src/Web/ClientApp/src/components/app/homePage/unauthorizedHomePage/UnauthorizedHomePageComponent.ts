import { IComponentDescriptor } from "src/componentsRegistration/IComponentDescriptor";
import { IComponent } from "src/componentsRegistration/IComponent";
import { UnauthorizedHomePageViewModel } from "src/components/app/homePage/unauthorizedHomePage/UnauthorizedHomePageViewModel";
import template from "./UnauthorizedHomePage.html";

export class UnauthorizedHomePageComponent implements IComponent {
    public generateDescriptor(): IComponentDescriptor {
        return { viewModel: UnauthorizedHomePageViewModel, template } as IComponentDescriptor;
    }
}