import { IComponentDescriptor } from "src/componentsRegistration/IComponentDescriptor";
import { AuthorizedHomePageViewModel } from "src/components/app/homePage/authorizedHomePage/AuthorizedHomePageViewModel";
import { IComponent } from "src/componentsRegistration/IComponent";
import template from "./AuthorizedHomePage.html";

export class AuthorizedHomePageComponent implements IComponent {
    public generateDescriptor(): IComponentDescriptor {
        return { viewModel: AuthorizedHomePageViewModel, template } as IComponentDescriptor;
    }
}