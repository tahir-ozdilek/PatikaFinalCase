using FluentValidation.Results;

namespace PatikaFinalProject.Common
{
    public class CustomValidationError
    {
        public string? ErrorMessage { get; set; }

        public string? PropertyName { get; set; }

    }

    public class Response : IResponse
    {
        public Response(ResponseType responseType)
        {
            ResponseType = responseType;
        }

        public Response(ResponseType responseType, string message)
        {
            ResponseType = responseType;
            Message = message;
        }

        public string? Message { get; set; }

        public ResponseType ResponseType { get; set; }
    }


    public class Response<T> : Response, IResponse<T>
    {
        public T Data { get; set; }

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

        public Response(ResponseType responseType, T data, List<CustomValidationError> errors) : base(responseType)
        {
            ValidationErrors = errors;
            Data = data;
        }

        public List<CustomValidationError> ValidationErrors { get; set; }
    }
}
