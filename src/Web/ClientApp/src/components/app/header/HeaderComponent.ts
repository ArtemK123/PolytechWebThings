import { IComponentDescriptor } from "src/componentsRegistration/IComponentDescriptor";
import { IComponent } from "src/componentsRegistration/IComponent";
import { HeaderViewModel } from "src/components/app/header/HeaderViewModel";
import template from "./Header.html";

export class HeaderComponent implements IComponent {
    public generateDescriptor(): IComponentDescriptor {
        return { viewModel: HeaderViewModel, template } as IComponentDescriptor;
    }
}