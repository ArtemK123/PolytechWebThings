import template from "./ThingCard.html";
import { IComponentDescriptor } from "../../../../componentsRegistration/IComponentDescriptor";
import { IComponent } from "../../../../componentsRegistration/IComponent";
import { ThingCardViewModel } from "./ThingCardViewModel";

export class ThingCardComponent implements IComponent {
    public generateDescriptor(): IComponentDescriptor {
        return { viewModel: ThingCardViewModel, template } as IComponentDescriptor;
    }
}