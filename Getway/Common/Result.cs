using System.Reflection.Metadata.Ecma335;

namespace Getway.Common
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; } = string.Empty;

        public T? Data { get; set; }

        public Result  (T? data,bool isSucces ,string? errorMessages ) 
        {
                   IsSuccess = isSucces;
                   Message = errorMessages ?? string.Empty;
                   Data = data;  
        }
        public static Result<T> Success(T data, string? messages = null ) 
        {

            return new Result<T>(data, true, messages);                
        }
        public static Result<T> Failure(string messages,T? data = default) {
            return new Result<T>(data, false, messages);
        }
    
            
    }
}
