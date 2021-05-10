import { IViewModel } from "../../componentsRegistration/IViewModel";
import { IRouterParams } from "../router/IRouterParams";
import { RouteGenerator } from "../router/RouteGenerator";

export default class AppViewModel implements IViewModel {
    public readonly routerParams: IRouterParams;

    constructor() {
        this.routerParams = {
            routes: [
                RouteGenerator.generate(/^\/$/, () => "<home-page></home-page>"),
                RouteGenerator.generate(/^\/login$/, () => "<login></login>"),
                RouteGenerator.generate(/^\/register$/, () => "<register></register>"),
                RouteGenerator.generate(/^\/create-workspace$/, () => "<create-workspace-page></create-workspace-page>"),
                RouteGenerator.generate(/^\/workspaces\/(\d+)$/, AppViewModel.generateWorkspacePage),
                RouteGenerator.generate(/^\/workspaces\/(\d+)\/edit$/, AppViewModel.generateEditWorkspacePage),
            ],
            routerElementId: "root-router",
        };
    }

    private static generateWorkspacePage(path: string): string {
        const id = path.replace(/^\/workspaces\//, "");
        return `<workspace-page params='{id: ${id}}'></workspace-page>`;
    }

    private static generateEditWorkspacePage(path: string): string {
        const id: number = Number.parseInt(path.replace("/workspaces/", "").replace("/edit", ""), 10);
        return `<update-workspace-page params='{id: ${id}}'></update-workspace-page>`;
    }
}