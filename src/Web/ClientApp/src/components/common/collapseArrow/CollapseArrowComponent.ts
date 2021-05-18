import { IComponentDescriptor } from "src/componentsRegistration/IComponentDescriptor";
import { IComponent } from "src/componentsRegistration/IComponent";
import { CollapseArrowViewModel } from "src/components/common/collapseArrow/CollapseArrowViewModel";
import template from "./CollapseArrow.html";

export class CollapseArrowComponent implements IComponent {
    generateDescriptor(): IComponentDescriptor {
        return { viewModel: CollapseArrowViewModel, template };
    }
}