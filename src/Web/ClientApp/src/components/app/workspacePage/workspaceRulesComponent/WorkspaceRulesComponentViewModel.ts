import * as ko from "knockout";
import { IViewModel } from "src/componentsRegistration/IViewModel";
import { IRuleModel } from "src/components/app/workspacePage/models/IRuleModel";
import { IOperationResult } from "src/backendApi/models/entities/OperationResult/IOperationResult";
import { OperationStatus } from "src/backendApi/models/entities/OperationResult/OperationStatus";
import { IGetRulesResponse } from "src/components/app/workspacePage/models/IGetRulesResponse";
import { IStepModel } from "src/components/app/workspacePage/models/IStepModel";
import { RedirectHandler } from "src/services/RedirectHandler";

export class WorkspaceRulesComponentViewModel implements IViewModel {
    public readonly isLoading: ko.Observable<boolean> = ko.observable(true);
    public readonly rules: ko.ObservableArray<IRuleModel> = ko.observableArray([]);

    constructor() {
        this.getHardcodedRules()
            .then((result) => {
                if (result.status === OperationStatus.Success) {
                    this.rules(result.data.rules);
                }
                this.isLoading(false);
            });
    }

    public createRule(): void {
        RedirectHandler.redirect(`${window.location.pathname}/create`);
    }

    private getHardcodedRules(): Promise<IOperationResult<IGetRulesResponse>> {
        return Promise.resolve({
            status: OperationStatus.Success,
            message: "Success",
            data: {
                rules: [
                    {
                        name: "Rule1",
                        steps: [{ description: "Step1" } as IStepModel, { description: "Step2" } as IStepModel],
                    } as IRuleModel,
                    {
                        name: "Rule2",
                        steps: [{ description: "StepA" } as IStepModel, { description: "StepB" } as IStepModel],
                    } as IRuleModel,
                ],
            } as IGetRulesResponse,
        } as IOperationResult<IGetRulesResponse>);
    }
}