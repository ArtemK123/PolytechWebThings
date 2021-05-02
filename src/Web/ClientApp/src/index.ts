import * as ko from 'knockout';
import ComponentRegistration from "./ComponentRegistration";
import AppViewModel from "./components/appComponent/AppViewModel";

function main() {
  new ComponentRegistration().registerBindings();
  const rootComponent = document.createElement("app");
  document.body.appendChild(rootComponent);
  ko.applyBindings(new AppViewModel(), rootComponent);
}

main();