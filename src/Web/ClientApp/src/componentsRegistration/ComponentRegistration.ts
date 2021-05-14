import * as ko from "knockout";
import { AppComponent } from "../components/app/AppComponent";
import { HeaderComponent } from "../components/app/header/HeaderComponent";
import { UnauthorizedHomePageComponent } from "../components/app/homePage/unauthorizedHomePage/UnauthorizedHomePageComponent";
import { LoginComponent } from "../components/app/login/LoginComponent";
import { WorkspaceCardComponent } from "../components/app/homePage/authorizedHomePage/workspaceCard/WorkspaceCardComponent";
import { BackendConnectionCheckComponent } from "../components/app/backendConnectionCheck/BackendConnectionCheckComponent";
import { CreateWorkspacePageComponent } from "../components/app/createWorkspacePage/CreateWorkspacePageComponent";
import { AuthorizedHomePageComponent } from "../components/app/homePage/authorizedHomePage/AuthorizedHomePageComponent";
import { RegisterComponent } from "../components/app/register/RegisterComponent";
import { HomePageComponent } from "../components/app/homePage/HomePageComponent";
import { WorkspacePageComponent } from "../components/app/workspacePage/WorkspacePageComponent";
import { UpdateWorkspacePageComponent } from "../components/app/updateWorkspacePage/UpdateWorkspacePageComponent";
import { ThingCardComponent } from "../components/app/workspacePage/thingCard/ThingCardComponent";
import { RouterComponent } from "../components/common/router/RouterComponent";
import { LoaderComponent } from "../components/common/loader/LoaderComponent";

export class ComponentRegistration {
    public registerBindings() {
        ComponentRegistration.registerAppComponents();
        ComponentRegistration.registerCommonComponents();
    }

    private static registerAppComponents() {
        ko.components.register("app", new AppComponent().generateDescriptor());
        ko.components.register("backend-connection-check", new BackendConnectionCheckComponent().generateDescriptor());
        ko.components.register("login", new LoginComponent().generateDescriptor());
        ko.components.register("register", new RegisterComponent().generateDescriptor());
        ko.components.register("home-page", new HomePageComponent().generateDescriptor());
        ko.components.register("unauthorized-home-page", new UnauthorizedHomePageComponent().generateDescriptor());
        ko.components.register("authorized-home-page", new AuthorizedHomePageComponent().generateDescriptor());
        ko.components.register("create-workspace-page", new CreateWorkspacePageComponent().generateDescriptor());
        ko.components.register("header-component", new HeaderComponent().generateDescriptor());
        ko.components.register("workspace-card", new WorkspaceCardComponent().generateDescriptor());
        ko.components.register("workspace-page", new WorkspacePageComponent().generateDescriptor());
        ko.components.register("update-workspace-page", new UpdateWorkspacePageComponent().generateDescriptor());
        ko.components.register("thing-card", new ThingCardComponent().generateDescriptor());
    }

    private static registerCommonComponents() {
        ko.components.register("router", new RouterComponent().generateDescriptor());
        ko.components.register("loader", new LoaderComponent().generateDescriptor());
    }
}