namespace MentalHealthCompanion.Data.DTO.General
{
    public class ApiResponse<T>
    {
        public T? Data { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
        public bool IsSuccessful { get; set; } = false;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public ApiResponse(){}

        public ApiResponse(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public ApiResponse(T data, string code, string message)
        {
            Data = data;
            Code = code;
            Message = message;
            IsSuccessful = true;
        }
    }
}
