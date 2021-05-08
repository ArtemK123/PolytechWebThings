import { IRouterParams } from "./IRouterParams";
import { IViewModel } from "../../componentsRegistration/IViewModel";

export class RouterViewModel implements IViewModel {
    private readonly routes: Record<string, string>;

    constructor(params: IRouterParams) {
        this.routes = params.routes;

        this.updateRouterDiv();

        window.addEventListener("popstate", () => {
            this.updateRouterDiv();
            history.go();
        });
    }

    private updateRouterDiv(): void {
        const routerDiv: HTMLElement = RouterViewModel.getRouterDiv();
        routerDiv.innerHTML = this.getTag();
    }

    private static getRouterDiv(): HTMLElement {
        return document.getElementById("router");
    }

    private getTag(): string {
        const currentPath: string = window.location.pathname;
        const componentTag: string | undefined = this.routes[currentPath] ?? this.routes["/"];
        if (componentTag === undefined) {
            throw new Error("Component for given tag is not found");
        }
        return componentTag;
    }
}