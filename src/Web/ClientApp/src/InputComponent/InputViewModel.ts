import IInputViewModelParams from "./IInputViewModelParams";

export default class InputViewModel {
  public text: KnockoutObservable<string>;

  constructor(params: IInputViewModelParams) {
    this.text = params.text;
  }
}