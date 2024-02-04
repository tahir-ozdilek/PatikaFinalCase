
namespace PatikaFinalProject.Common
{
    public class CustomValidationError
    {
        public string? ErrorMessage { get; set; }

        public string? PropertyName { get; set; }

    }

    public enum ResponseType
    {
        Success,
        ValidationError,
        NotFound
    }

    public class Response
    {
        public string? Message { get; set; }

        public ResponseType? ResponseType { get; set; }

        public Response() { }

        public Response(ResponseType responseType)
        {
            ResponseType = responseType;
        }

        public Response(ResponseType responseType, string message)
        {
            ResponseType = responseType;
            Message = message;
        }
    }


    public class Response<T> : Response
    {
        public T? Data { get; set; }

        public List<CustomValidationError>? ValidationErrors { get; set; }

        public Response() { }

        public Response(ResponseType responseType, T data) : base(responseType)
        {
            Data = data;
        }

        public Response(ResponseType responseType, string message) : base(responseType, message)
        {
        }

        public Response(ResponseType responseType, T data, string message) : base(responseType)
        {
            Message = message;
            Data = data;
        }

        public Response(ResponseType responseType, T data, List<CustomValidationError>? errors) : base(responseType)
        {
            ValidationErrors = errors;
            Data = data;
        }
    }
}
