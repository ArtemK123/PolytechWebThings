import {IRouterParams} from "./IRouterParams";
import {IViewModel} from "../../componentsRegistration/IViewModel";

export class RouterViewModel implements IViewModel {
  constructor(params: IRouterParams) {
    const currentPath: string = window.location.pathname;
    const componentTag: string | undefined = params.routes[currentPath] ?? params.routes["/"];
    if (componentTag === undefined) {
      throw new Error("Component for given tag is not found");
    }
    const routerDiv: HTMLElement = document.getElementById("router");
    routerDiv.innerHTML = componentTag;
  }
}