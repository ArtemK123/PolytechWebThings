import * as ko from "knockout";
import { HeaderComponent } from "src/components/app/header/HeaderComponent";
import { UnauthorizedHomePageComponent } from "src/components/app/homePage/unauthorizedHomePage/UnauthorizedHomePageComponent";
import { CreateRuleModalComponent } from "src/components/app/workspacePage/createRuleModal/CreateRuleModalComponent";
import { LoaderComponent } from "src/components/common/loader/LoaderComponent";
import { ThingCardComponent } from "src/components/app/workspacePage/workspaceThingsComponent/thingCard/ThingCardComponent";
import { LoginComponent } from "src/components/app/login/LoginComponent";
import { WorkspaceCardComponent } from "src/components/app/homePage/authorizedHomePage/workspaceCard/WorkspaceCardComponent";
import { BackendConnectionCheckComponent } from "src/components/app/backendConnectionCheck/BackendConnectionCheckComponent";
import { CreateWorkspacePageComponent } from "src/components/app/createWorkspacePage/CreateWorkspacePageComponent";
import { AuthorizedHomePageComponent } from "src/components/app/homePage/authorizedHomePage/AuthorizedHomePageComponent";
import { WorkspaceThingsComponent } from "src/components/app/workspacePage/workspaceThingsComponent/WorkspaceThingsComponent";
import { AppComponent } from "src/components/app/AppComponent";
import { RouterComponent } from "src/components/common/router/RouterComponent";
import { RegisterComponent } from "src/components/app/register/RegisterComponent";
import { CreateStepModalComponent } from "src/components/app/workspacePage/createRuleModal/CreateStepModal/CreateStepModalComponent";
import { HomePageComponent } from "src/components/app/homePage/HomePageComponent";
import { WorkspacePageComponent } from "src/components/app/workspacePage/WorkspacePageComponent";
import { UpdateWorkspacePageComponent } from "src/components/app/updateWorkspacePage/UpdateWorkspacePageComponent";
import { WorkspaceRulesComponent } from "src/components/app/workspacePage/workspaceRulesComponent/WorkspaceRulesComponent";
import { ThingPropertyCardComponent } from "src/components/app/workspacePage/workspaceThingsComponent/thingCard/thingPropertyCard/ThingPropertyCardComponent";
import { RuleCardComponent } from "src/components/app/workspacePage/workspaceRulesComponent/ruleCard/RuleCardComponent";
import {RuleStepCardComponent} from "src/components/app/workspacePage/workspaceRulesComponent/ruleCard/ruleStepCard/RuleStepCardComponent";

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
        ko.components.register("create-rule-modal", new CreateRuleModalComponent().generateDescriptor());
        ko.components.register("create-step-modal", new CreateStepModalComponent().generateDescriptor());
        ko.components.register("workspace-things-component", new WorkspaceThingsComponent().generateDescriptor());
        ko.components.register("workspace-rules-component", new WorkspaceRulesComponent().generateDescriptor());
        ko.components.register("thing-property-card", new ThingPropertyCardComponent().generateDescriptor());
        ko.components.register("rule-card", new RuleCardComponent().generateDescriptor());
        ko.components.register("rule-step-card", new RuleStepCardComponent().generateDescriptor());
    }

    private static registerCommonComponents() {
        ko.components.register("router", new RouterComponent().generateDescriptor());
        ko.components.register("loader", new LoaderComponent().generateDescriptor());
    }
}