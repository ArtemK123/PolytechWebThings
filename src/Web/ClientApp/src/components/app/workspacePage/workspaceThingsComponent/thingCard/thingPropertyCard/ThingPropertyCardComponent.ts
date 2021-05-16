import { IComponentDescriptor } from "src/componentsRegistration/IComponentDescriptor";
import { IComponent } from "src/componentsRegistration/IComponent";
import { ThingPropertyCardViewModel } from "src/components/app/workspacePage/workspaceThingsComponent/thingCard/thingPropertyCard/ThingPropertyCardViewModel";
import template from "./ThingPropertyCard.html";
import "./ThingPropertyCard.scss";

export class ThingPropertyCardComponent implements IComponent {
    public generateDescriptor(): IComponentDescriptor {
        return { viewModel: ThingPropertyCardViewModel, template } as IComponentDescriptor;
    }
}