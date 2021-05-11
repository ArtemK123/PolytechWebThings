import { OperationStatus } from "./OperationStatus";

export interface IOperationResult<TData> {
    status: OperationStatus;
    data: TData;
}