namespace Web.Models.OperationResults
{
    public record OperationResult
    {
        public OperationResult(OperationStatus status)
        {
            Status = status;
        }

        public OperationStatus Status { get; init; }
    }

    public record OperationResult<TData> : OperationResult
    {
        public OperationResult(OperationStatus status, TData data)
            : base(status)
        {
            Data = data;
        }

        public TData Data { get; init; }
    }
}