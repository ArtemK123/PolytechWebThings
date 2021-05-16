import { IComponentDescriptor } from "src/componentsRegistration/IComponentDescriptor";
import { IComponent } from "src/componentsRegistration/IComponent";
import { ThingCardViewModel } from "src/components/app/workspacePage/workspaceThingsComponent/thingCard/ThingCardViewModel";
import template from "./ThingCard.html";
import "./ThingCard.scss";

export class ThingCardComponent implements IComponent {
    public generateDescriptor(): IComponentDescriptor {
        return { viewModel: ThingCardViewModel, template } as IComponentDescriptor;
    }
}