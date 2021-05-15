import * as ko from "knockout";

export interface ICreateStepModalParams {
    isVisible: ko.Observable<boolean>;
    steps: ko.ObservableArray<string>;
}