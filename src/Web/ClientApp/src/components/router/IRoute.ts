export interface IRoute {
    pattern: RegExp;
    generateHtmlElement: (route: string) => string;
}