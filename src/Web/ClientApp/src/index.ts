import * as ko from "knockout";
import { ComponentRegistration } from "./componentsRegistration/ComponentRegistration";
import { AppViewModel } from "./components/app/AppViewModel";
import "@fortawesome/fontawesome-free/js/fontawesome";
import "@fortawesome/fontawesome-free/js/solid";
import "@fortawesome/fontawesome-free/js/regular";
import "@fortawesome/fontawesome-free/js/brands";

function main() {
    new ComponentRegistration().registerBindings();
    const rootComponent = document.createElement("app");
    document.body.appendChild(rootComponent);
    ko.applyBindings(new AppViewModel(), rootComponent);
    window.addEventListener("popstate", () => history.go()); // reload elements when browser history item is changed
}

main();