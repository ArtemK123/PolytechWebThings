namespace Web.Models.OperationResults
{
    public record OperationResult
    {
        public OperationResult(OperationStatus status, string? message)
        {
            Status = status;
            Message = message;
        }

        public OperationResult(OperationStatus status)
        {
            Status = status;
            Message = status.ToString();
        }

        // for json serializer
        public OperationResult()
        {
        }

        public OperationStatus Status { get; set; }

        public string? Message { get; set; }
    }

    public record OperationResult<TData> : OperationResult
    {
        public OperationResult(OperationStatus status, string message, TData? data)
            : base(status, message)
        {
            Data = data;
        }

        public OperationResult(OperationStatus status, TData? data)
            : base(status)
        {
            Data = data;
        }

        // for json serializer
        public OperationResult()
        {
        }

        public TData? Data { get; set; }
    }
}