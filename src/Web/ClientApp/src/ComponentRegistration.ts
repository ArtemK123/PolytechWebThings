import * as ko from 'knockout';
import AppComponent from "./components/app/AppComponent";
import BackendConnectionCheckComponent from './components/backendConnectionCheck/BackendConnectionCheckComponent';
import LoginComponent from "./components/login/LoginComponent";
import ProfileComponent from "./components/profile/ProfileComponent";
import RegisterComponent from "./components/register/RegisterComponent";

class ComponentRegistration {
  registerBindings() {
    ko.components.register('app', AppComponent);
    ko.components.register('backend-connection-check', BackendConnectionCheckComponent);
    ko.components.register('login', LoginComponent);
    ko.components.register('register', RegisterComponent);
    ko.components.register('profile', ProfileComponent);
  }
}

export default ComponentRegistration;