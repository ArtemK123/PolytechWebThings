import template from "./UnauthorizedHomePage.html";
import { UnauthorizedHomePageViewModel } from "./UnauthorizedHomePageViewModel";
import { IComponentDescriptor } from "../../../../componentsRegistration/IComponentDescriptor";
import { IComponent } from "../../../../componentsRegistration/IComponent";

export class UnauthorizedHomePageComponent implements IComponent {
    public generateDescriptor(): IComponentDescriptor {
        return { viewModel: UnauthorizedHomePageViewModel, template } as IComponentDescriptor;
    }
}