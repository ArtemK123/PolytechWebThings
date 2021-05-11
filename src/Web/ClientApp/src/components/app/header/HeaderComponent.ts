import template from "./Header.html";
import { HeaderViewModel } from "./HeaderViewModel";
import { IComponentDescriptor } from "../../../componentsRegistration/IComponentDescriptor";
import { IComponent } from "../../../componentsRegistration/IComponent";

export class HeaderComponent implements IComponent {
    public generateDescriptor(): IComponentDescriptor {
        return { viewModel: HeaderViewModel, template } as IComponentDescriptor;
    }
}