import * as ko from "knockout";
import { IViewModel } from "src/componentsRegistration/IViewModel";
import { IThingPropertyCardParams } from "src/components/app/workspacePage/workspaceThingsComponent/thingCard/thingPropertyCard/IThingPropertyCardParams";
import { IPropertyApiModel } from "src/backendApi/models/entities/IPropertyApiModel";
import { ThingsApiClient } from "src/backendApi/clients/ThingsApiClient";
import { IChangePropertyStateRequest } from "src/backendApi/models/request/things/IChangePropertyStateRequest";
import { OperationStatus } from "src/backendApi/models/entities/OperationResult/OperationStatus";
import { IThingStateApiModel } from "src/backendApi/models/entities/IThingStateApiModel";

export class ThingPropertyCardViewModel implements IViewModel {
    public readonly inputValue: ko.Observable<string> = ko.observable("");
    public readonly isUpdateInProgress: ko.Observable<boolean> = ko.observable(false);

    private readonly model: IPropertyApiModel;
    private readonly params: IThingPropertyCardParams;
    private readonly thingId: ko.Observable<string> = ko.observable("");
    private readonly thingsApiClient: ThingsApiClient = new ThingsApiClient();

    constructor(params: IThingPropertyCardParams) {
        this.params = params;
        this.model = params.model;
        this.inputValue(this.model.defaultValue);
        this.params.thingState.subscribe((newState: IThingStateApiModel) => {
            this.thingId(newState.thingId);
            const currentPropertyRecord: string | undefined = newState.propertyStates[this.model.name];
            if (currentPropertyRecord === undefined) {
                throw new Error(`Can not find state of property ${this.model.name}`);
            }
            this.inputValue(currentPropertyRecord);
        });
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
            thingId: this.params.thingState().thingId,
            propertyName: this.model.name,
            newPropertyValue: this.inputValue(),
        } as IChangePropertyStateRequest;
        this.thingsApiClient.changePropertyState(requestModel)
            .then((operationResult) => {
                this.isUpdateInProgress(false);
                if (operationResult.status === OperationStatus.Success) {
                    alert("Property is successfully updated");
                }
            });
    }
}