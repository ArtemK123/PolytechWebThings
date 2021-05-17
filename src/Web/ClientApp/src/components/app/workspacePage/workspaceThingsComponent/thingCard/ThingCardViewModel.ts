import * as ko from "knockout";
import { IPropertyApiModel } from "src/backendApi/models/response/things/IPropertyApiModel";
import { IThingCardParams } from "src/components/app/workspacePage/workspaceThingsComponent/thingCard/IThingCardParams";
import { IViewModel } from "src/componentsRegistration/IViewModel";
import { IThingApiModel } from "src/backendApi/models/response/things/IThingApiModel";
import { IThingStateApiModel } from "src/backendApi/models/response/things/IThingStateApiModel";
import { IOperationResult } from "src/backendApi/models/response/OperationResult/IOperationResult";
import { OperationStatus } from "src/backendApi/models/response/OperationResult/OperationStatus";

export class ThingCardViewModel implements IViewModel {
    public readonly title: ko.Observable<string> = ko.observable("");
    public readonly properties: ko.ObservableArray<IPropertyApiModel> = ko.observableArray([]);
    public readonly isCollapsed: ko.Observable<boolean> = ko.observable(true);
    public readonly model: IThingApiModel;
    public readonly params: IThingCardParams;
    public readonly thingState: ko.Observable<IThingStateApiModel> = ko.observable();

    constructor(params: IThingCardParams) {
        this.params = params;
        this.model = params.model;
        this.title(params.model.title);
        this.properties(params.model.properties);
    }

    public changeCollapseState(): void {
        const isCollapsed: boolean = this.isCollapsed();
        this.isCollapsed(!isCollapsed);

        if (isCollapsed) {
            this.fetchThingState();
        }
    }

    private fetchThingState(): void {
        this.getHardcodedThingState()
            .then((result) => {
                if (result.status === OperationStatus.Success) {
                    this.thingState(result.data);
                    this.thingState.notifySubscribers(result.data);
                }
            });
    }

    private getHardcodedThingState(): Promise<IOperationResult<IThingStateApiModel>> {
        return Promise.resolve({
            status: OperationStatus.Success,
            data: {
                thingId: "thingId",
                propertyStates: {
                    on: "true",
                    color: "#ffff00",
                },
            } as IThingStateApiModel,
        } as IOperationResult<IThingStateApiModel>);
    }
}