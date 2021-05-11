import { OperationStatus } from "./OperationStatus";

export interface IOperationResult<TData> {
    status: OperationStatus;
    message: string;
    data: TData;
}