import * as ko from "knockout";
import { IWorkspaceCardParams } from "src/components/app/homePage/authorizedHomePage/workspaceCard/IWorkspaceCardParams";
import { RedirectHandler } from "src/services/RedirectHandler";
import { IWorkspaceApiModel } from "src/backendApi/models/entities/IWorkspaceApiModel";
import { IViewModel } from "src/componentsRegistration/IViewModel";
import { IDeleteWorkspaceRequest } from "src/backendApi/models/request/workspace/IDeleteWorkspaceRequest";
import { WorkspaceApiClient } from "src/backendApi/clients/WorkspaceApiClient";

export class WorkspaceCardViewModel implements IViewModel {
    public readonly workspaceLink: ko.Observable<string> = ko.observable("test");
    public readonly workspaceDescriptor: ko.Observable<string> = ko.observable("test");

    private readonly workspaceId: number;
    private readonly workspaceApiClient: WorkspaceApiClient = new WorkspaceApiClient();

    constructor(params: IWorkspaceCardParams) {
        this.workspaceId = params.workspaceApiModel.id;
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
            this.workspaceApiClient
                .deleteWorkspace({ id: this.workspaceId } as IDeleteWorkspaceRequest)
                .then(() => RedirectHandler.reloadCurrentPage());
        }
    }

    private static generateLinkText(workspaceApiModel: IWorkspaceApiModel): string {
        return `${workspaceApiModel.id}. ${workspaceApiModel.name} (${workspaceApiModel.gatewayUrl})`;
    }
}