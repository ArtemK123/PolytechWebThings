import * as ko from 'knockout';
import {AppComponent} from "../components/app/AppComponent";
import {BackendConnectionCheckComponent} from "../components/backendConnectionCheck/BackendConnectionCheckComponent";
import {LoginComponent} from "../components/login/LoginComponent";
import {RegisterComponent} from "../components/register/RegisterComponent";
import {ProfileComponent} from "../components/profile/ProfileComponent";
import {LogoutComponent} from "../components/logout/LogoutComponent";
import {RouterComponent} from "../components/router/RouterComponent";

class ComponentRegistration {
  registerBindings() {
    ko.components.register("app", new AppComponent().generateDescriptor());
    ko.components.register("backend-connection-check", new BackendConnectionCheckComponent().generateDescriptor());
    ko.components.register("login", new LoginComponent().generateDescriptor());
    ko.components.register("register", new RegisterComponent().generateDescriptor());
    ko.components.register("profile", new ProfileComponent().generateDescriptor());
    ko.components.register("logout", new LogoutComponent().generateDescriptor());
    ko.components.register("router", new RouterComponent().generateDescriptor());
  }
}

export default ComponentRegistration;