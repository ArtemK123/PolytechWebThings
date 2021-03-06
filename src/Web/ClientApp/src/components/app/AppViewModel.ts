import { IRouterParams } from "src/components/common/router/IRouterParams";
import { RouteGenerator } from "src/components/common/router/RouteGenerator";
import { IViewModel } from "src/componentsRegistration/IViewModel";

export class AppViewModel implements IViewModel {
    public readonly routerParams: IRouterParams;

    constructor() {
        this.routerParams = {
            routes: [
                RouteGenerator.generate(/^\/$/, () => "<home-page></home-page>"),
                RouteGenerator.generate(/^\/login$/, () => "<login></login>"),
                RouteGenerator.generate(/^\/register$/, () => "<register></register>"),
                RouteGenerator.generate(/^\/create-workspace$/, () => "<create-workspace-page></create-workspace-page>"),
                RouteGenerator.generate(/^\/workspaces\/(\d+)\/edit$/, AppViewModel.generateEditWorkspacePage),
                RouteGenerator.generate(/^\/workspaces\/(\d+)/, AppViewModel.generateWorkspacePage),
            ],
            routerElementId: "root-router",
        };
    }

    private static generateWorkspacePage(path: string): string {
        const pathElems: string[] = path.split("/");
        const id: number = Number.parseInt(pathElems[2], 10);
        return `<workspace-page params='{id: ${id}}'></workspace-page>`;
    }

    private static generateEditWorkspacePage(path: string): string {
        const pathElems: string[] = path.split("/");
        const id: number = Number.parseInt(pathElems[2], 10);
        return `<update-workspace-page params='{id: ${id}}'></update-workspace-page>`;
    }
}