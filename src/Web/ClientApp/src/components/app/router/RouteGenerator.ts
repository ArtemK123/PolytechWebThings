import { IRoute } from "./IRoute";

export class RouteGenerator {
    public static generate(pattern: RegExp, generateHtmlElement: (route: string) => string): IRoute {
        return { pattern, generateHtmlElement };
    }
}