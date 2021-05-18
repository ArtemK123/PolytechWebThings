import { OperationStatus } from "src/backendApi/models/entities/OperationResult/OperationStatus";

export interface IOperationResult<TData> {
    status: OperationStatus;
    message: string;
    data: TData;
}