import * as ko from "knockout";
import { AppComponent } from "../components/app/AppComponent";
import { BackendConnectionCheckComponent } from "../components/backendConnectionCheck/BackendConnectionCheckComponent";
import { LoginComponent } from "../components/login/LoginComponent";
import { RegisterComponent } from "../components/register/RegisterComponent";
import { ProfileComponent } from "../components/profile/ProfileComponent";
import { LogoutComponent } from "../components/logout/LogoutComponent";
import { RouterComponent } from "../components/router/RouterComponent";
import { UnauthorizedHomePageComponent } from "../components/unauthorizedHomePage/UnauthorizedHomePageComponent";
import { AuthorizedHomePageComponent } from "../components/authorizedHomePage/AuthorizedHomePageComponent";
import { HomePageComponent } from "../components/homePage/HomePageComponent";
import { CreateWorkspacePageComponent } from "../components/createWorkspacePage/CreateWorkspacePageComponent";
import { HeaderComponent } from "../components/header/HeaderComponent";
import { WorkspaceCardComponent } from "../components/authorizedHomePage/workspaceCard/WorkspaceCardComponent";
import { WorkspacePageComponent } from "../components/workspacePage/WorkspacePageComponent";
import { UpdateWorkspacePageComponent } from "../components/updateWorkspacePage/UpdateWorkspacePageComponent";

class ComponentRegistration {
    registerBindings() {
        ko.components.register("app", new AppComponent().generateDescriptor());
        ko.components.register("backend-connection-check", new BackendConnectionCheckComponent().generateDescriptor());
        ko.components.register("login", new LoginComponent().generateDescriptor());
        ko.components.register("register", new RegisterComponent().generateDescriptor());
        ko.components.register("profile", new ProfileComponent().generateDescriptor());
        ko.components.register("logout", new LogoutComponent().generateDescriptor());
        ko.components.register("router", new RouterComponent().generateDescriptor());
        ko.components.register("home-page", new HomePageComponent().generateDescriptor());
        ko.components.register("unauthorized-home-page", new UnauthorizedHomePageComponent().generateDescriptor());
        ko.components.register("authorized-home-page", new AuthorizedHomePageComponent().generateDescriptor());
        ko.components.register("create-workspace-page", new CreateWorkspacePageComponent().generateDescriptor());
        ko.components.register("header-component", new HeaderComponent().generateDescriptor());
        ko.components.register("workspace-card", new WorkspaceCardComponent().generateDescriptor());
        ko.components.register("workspace-page", new WorkspacePageComponent().generateDescriptor());
        ko.components.register("update-workspace-page", new UpdateWorkspacePageComponent().generateDescriptor());
    }
}

export default ComponentRegistration;