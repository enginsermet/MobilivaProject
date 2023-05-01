using System.Net;

namespace MobilivaProject.Models
{
    public class ApiResponse<T>
    {
        public HttpStatusCode ErrorCode { get; set; }
        public string ResultMessage { get; set; }

        public T Data { get; set; }

        //Status (Success,Failed enum) , ResultMessage,ErrorCode,Data (GenericType)
        public enum Status
        {
            Success,
            Failed
        }

        public ApiResponse()
        {
        }

        public ApiResponse(Status status, string resultMessage, T data)
        {
            ResultMessage = resultMessage;
            Data = data;
        }

        public ApiResponse(Status status, string resultMessage, HttpStatusCode errorCode)
        {
            ResultMessage = resultMessage;
            ErrorCode = errorCode;
        }

        public ApiResponse<T> Success(T data)
        {
            ApiResponse<T> apiResponse = new ApiResponse<T>(Status.Success, "Data loaded successfully", data);

            return apiResponse;
        }

        public ApiResponse<T> Failed(string resultMessage, HttpStatusCode errorCode)
        {
            ApiResponse<T> apiResponse = new ApiResponse<T>(Status.Failed, resultMessage, errorCode);

            return apiResponse;
        }


    }
}
