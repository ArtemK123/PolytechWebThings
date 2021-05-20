import * as ko from "knockout";
import { IViewModel } from "src/componentsRegistration/IViewModel";
import { IChangeThingStateStepCardParams } from "src/components/app/workspacePage/workspaceRulesComponent/ruleCard/ruleStepCard/ChangeThingStateStepCard/IChangeThingStateStepCardParams";

export class ChangeThingStateStepCardViewModel implements IViewModel {
    public readonly params: IChangeThingStateStepCardParams;
    public readonly titleText: ko.Computed<string>;

    constructor(params: IChangeThingStateStepCardParams) {
        this.params = params;
        this.titleText = ko.computed(() => `${params.index()}. Change thing state`);
    }
}