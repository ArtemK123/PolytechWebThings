import * as ko from "knockout";
import { IViewModel } from "src/componentsRegistration/IViewModel";
import { IThingPropertyCardParams } from "src/components/app/workspacePage/workspaceThingsComponent/thingCard/thingPropertyCard/IThingPropertyCardParams";
import { IPropertyApiModel } from "src/backendApi/models/response/things/IPropertyApiModel";
import { ThingsApiClient } from "src/backendApi/clients/ThingsApiClient";
import { IChangePropertyStateRequest } from "src/backendApi/models/request/things/IChangePropertyStateRequest";
import { OperationStatus } from "src/backendApi/models/response/OperationResult/OperationStatus";

export class ThingPropertyCardViewModel implements IViewModel {
    public readonly inputValue: ko.Observable<string> = ko.observable("");
    public readonly isUpdateInProgress: ko.Observable<boolean> = ko.observable(false);
    private readonly model: IPropertyApiModel;
    private readonly thingsApiClient: ThingsApiClient = new ThingsApiClient();
    private readonly params: IThingPropertyCardParams;

    constructor(params: IThingPropertyCardParams) {
        this.params = params;
        this.model = params.model;
        this.inputValue(this.model.defaultValue);
    }

    public handleKeyUp(data: ThingPropertyCardViewModel, event: KeyboardEvent): void {
        if (event.key === "Enter") {
            event.preventDefault();
            this.handleValueUpdate();
        }
    }

    private handleValueUpdate(): void {
        this.isUpdateInProgress(true);
        const requestModel: IChangePropertyStateRequest = {
            workspaceId: this.params.workspaceId,
            thingId: this.params.thingId,
            propertyName: this.model.name,
            newPropertyValue: this.inputValue(),
        } as IChangePropertyStateRequest;
        this.thingsApiClient.changePropertyState(requestModel)
            .then((operationResult) => {
                this.isUpdateInProgress(false);
                if (operationResult.status === OperationStatus.Success) {
                    alert("Updated property successfully");
                }
            });
    }
}