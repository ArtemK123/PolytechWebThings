import * as ko from "knockout";
import { IViewModel } from "src/componentsRegistration/IViewModel";
import { ICollapseArrowParams } from "src/components/common/collapseArrow/ICollapseArrowParams";

export class CollapseArrowViewModel implements IViewModel {
    public readonly isCollapsed: ko.Observable<boolean>;

    constructor(params: ICollapseArrowParams) {
        this.isCollapsed = params.isCollapsed;
    }
}