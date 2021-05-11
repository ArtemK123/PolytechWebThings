import template from "./AuthorizedHomePage.html";
import { AuthorizedHomePageViewModel } from "./AuthorizedHomePageViewModel";
import { IComponentDescriptor } from "../../../../componentsRegistration/IComponentDescriptor";
import { IComponent } from "../../../../componentsRegistration/IComponent";

export class AuthorizedHomePageComponent implements IComponent {
    public generateDescriptor(): IComponentDescriptor {
        return { viewModel: AuthorizedHomePageViewModel, template } as IComponentDescriptor;
    }
}