import * as ko from 'knockout';

import HelloWorldComponent from "./HelloWorldComponent/HelloWorldComponent";
import InputComponent from "./InputComponent/InputComponent";
import BackendConnectivityComponent from "./BackendConnectivityComponent/BackendConnectivityComponent";

class ComponentRegistration {
  registerBindings() {
    ko.components.register('input-component', InputComponent);
    ko.components.register('hello-world', HelloWorldComponent);
    ko.components.register('backend-connectivity', BackendConnectivityComponent);
  }
}

export default ComponentRegistration;