namespace FCG_PAYMENTAPI.Models
{
    public sealed class ServiceResult
    {
        public bool Success { get; init; }
        public string? Error { get; init; }
        public object? Data { get; init; }

        public static ServiceResult Ok(object? data = null) => new() { Success = true, Data = data };
        public static ServiceResult Fail(string error) => new() { Success = false, Error = error };
    }
}
