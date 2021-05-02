import * as ko from 'knockout';
import AppComponent from "./components/appComponent/AppComponent";
import BackendConnectionCheckComponent from './components/backendConnectionCheckComponent/BackendConnectionCheckComponent';

class ComponentRegistration {
  registerBindings() {
    ko.components.register('app', AppComponent);
    ko.components.register('backend-connection-check', BackendConnectionCheckComponent);
  }
}

export default ComponentRegistration;