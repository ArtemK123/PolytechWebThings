import { IRouterParams } from "./IRouterParams";
import { IRoute } from "./IRoute";
import { IViewModel } from "../../../componentsRegistration/IViewModel";

export class RouterViewModel implements IViewModel {
    private readonly routes: IRoute[];
    private readonly routerElementId: string;

    constructor(params: IRouterParams) {
        this.routes = params.routes;
        this.routerElementId = params.routerElementId;

        this.updateRouterDiv();
    }

    private updateRouterDiv(): void {
        const routerDiv: HTMLElement = this.getRouterDiv();
        routerDiv.innerHTML = this.generateRouteHtml();
    }

    private getRouterDiv(): HTMLElement {
        return document.getElementById(this.routerElementId);
    }

    private generateRouteHtml(): string {
        const currentPath: string = window.location.pathname;

        const route: IRoute = this.getTargetRoute(currentPath);
        return route.generateHtmlElement(currentPath);
    }

    private getTargetRoute(location: string): IRoute {
        const targetRoute: IRoute | undefined = this.routes.find((route: IRoute) => route.pattern.test(location));
        if (!targetRoute) {
            throw new Error("Can not find route for given path");
        }
        return targetRoute;
    }
}