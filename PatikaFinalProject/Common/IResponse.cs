
namespace PatikaFinalProject.Common
{
    public enum ResponseType
    {
        Success,
        ValidationError,
        NotFound
    }


    public interface IResponse
    {
        string? Message { get; set; }

        ResponseType ResponseType { get; set; }
    }


    public interface IResponse<T> : IResponse
    {
        T Data { get; set; }

        List<CustomValidationError> ValidationErrors { get; set; }
    }
}
