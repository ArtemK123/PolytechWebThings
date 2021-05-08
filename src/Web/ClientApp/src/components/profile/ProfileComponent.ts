import template from "./Profile.html";
import { ProfileViewModel } from "./ProfileViewModel";
import { IComponent } from "../../componentsRegistration/IComponent";
import { IComponentDescriptor } from "../../componentsRegistration/IComponentDescriptor";
import "./Profile.scss";

export class ProfileComponent implements IComponent {
    generateDescriptor(): IComponentDescriptor {
        return { viewModel: ProfileViewModel, template };
    }
}