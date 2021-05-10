import * as ko from "knockout";
import { IViewModel } from "../../componentsRegistration/IViewModel";
import { IWorkspaceCardParams } from "./IWorkspaceCardParams";
import { IWorkspaceApiModel } from "../../backendApi/models/response/IWorkspaceApiModel";
import { RedirectHandler } from "../../services/RedirectHandler";

export class WorkspaceCardViewModel implements IViewModel {
    public readonly workspaceLink: ko.Observable<string> = ko.observable("test");
    public readonly workspaceDescriptor: ko.Observable<string> = ko.observable("test");

    constructor(params: IWorkspaceCardParams) {
        this.workspaceLink(`workspaces/${params.workspaceApiModel.id.toString()}`);
        this.workspaceDescriptor(WorkspaceCardViewModel.generateLinkText(params.workspaceApiModel));
    }

    public handleEdit(): void {
        const editLink: string = `${this.workspaceLink()}/edit`;
        RedirectHandler.redirect(editLink);
    }

    public handleDelete(): void {
        const confirmDelete: boolean = confirm(`Are you sure to delete this workspace - ${this.workspaceDescriptor()}`);
        if (confirmDelete) {
            throw new Error("Not implemented");
        }
    }

    private static generateLinkText(workspaceApiModel: IWorkspaceApiModel): string {
        return `${workspaceApiModel.id}. ${workspaceApiModel.name} (${workspaceApiModel.gatewayUrl})`;
    }
}