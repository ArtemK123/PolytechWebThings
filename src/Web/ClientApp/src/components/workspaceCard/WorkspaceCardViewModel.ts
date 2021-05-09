import * as ko from "knockout";
import { IViewModel } from "../../componentsRegistration/IViewModel";
import { IWorkspaceCardParams } from "./IWorkspaceCardParams";
import { IWorkspaceApiModel } from "../../backendApi/models/response/IWorkspaceApiModel";

export class WorkspaceCardViewModel implements IViewModel {
    public readonly href: ko.Observable<string> = ko.observable("test");
    public readonly text: ko.Observable<string> = ko.observable("test");

    constructor(params: IWorkspaceCardParams) {
        this.href(`workspaces/${params.workspaceApiModel.id.toString()}`);
        this.text(WorkspaceCardViewModel.generateLinkText(params.workspaceApiModel));
    }

    private static generateLinkText(workspaceApiModel: IWorkspaceApiModel): string {
        return `${workspaceApiModel.id}. ${workspaceApiModel.name} (${workspaceApiModel.gatewayUrl})`;
    }
}