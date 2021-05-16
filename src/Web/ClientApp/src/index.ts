import * as ko from "knockout";
import "@fortawesome/fontawesome-free/js/fontawesome";
import "@fortawesome/fontawesome-free/js/solid";
import "@fortawesome/fontawesome-free/js/regular";
import "@fortawesome/fontawesome-free/js/brands";
import { ComponentRegistration } from "src/componentsRegistration/ComponentRegistration";
import { AppViewModel } from "src/components/app/AppViewModel";

function main() {
    new ComponentRegistration().registerBindings();
    const rootComponent = document.createElement("app");
    document.body.appendChild(rootComponent);
    ko.applyBindings(new AppViewModel(), rootComponent);
    window.addEventListener("popstate", () => history.go()); // reload elements when browser history item is changed
}

main();