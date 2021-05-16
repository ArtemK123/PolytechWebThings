import { IRoute } from "src/components/common/router/IRoute";

export class RouteGenerator {
    public static generate(pattern: RegExp, generateHtmlElement: (route: string) => string): IRoute {
        return { pattern, generateHtmlElement };
    }
}