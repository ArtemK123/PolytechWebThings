import { OperationStatus } from "src/backendApi/models/response/OperationResult/OperationStatus";

export interface IOperationResult<TData> {
    status: OperationStatus;
    message: string;
    data: TData;
}