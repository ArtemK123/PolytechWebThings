export class RedirectHandler {
    public static redirect(relativeUrl: string, data: any = null): void {
        history.pushState(data, null, relativeUrl);
        RedirectHandler.reloadCurrentPage();
    }

    public static reloadCurrentPage(): void {
        history.go();
    }
}