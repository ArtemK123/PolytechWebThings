import * as ko from 'knockout';
import ComponentRegistration from "./ComponentRegistration";
import HelloWorldViewModel from './HelloWorldComponent/HelloWorldViewModel';

function main() {
  new ComponentRegistration().registerBindings();
  const rootComponent = document.createElement("hello-world");
  document.body.appendChild(rootComponent);
  ko.applyBindings(new HelloWorldViewModel(), rootComponent);
}

main();