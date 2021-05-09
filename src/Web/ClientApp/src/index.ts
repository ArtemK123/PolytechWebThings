import * as ko from "knockout";
import AppViewModel from "./components/app/AppViewModel";
import ComponentRegistration from "./componentsRegistration/ComponentRegistration";

function main() {
    new ComponentRegistration().registerBindings();
    const rootComponent = document.createElement("app");
    document.body.appendChild(rootComponent);
    ko.applyBindings(new AppViewModel(), rootComponent);
    window.addEventListener("popstate", () => history.go()); // reload elements when browser history item is changed
}

main();