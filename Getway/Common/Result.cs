using System.Reflection.Metadata.Ecma335;

namespace Getway.Common
{
    public class Result
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; } = string.Empty;

        public object? Data { get; set; }

        public Result(object data,bool isSucces ,string? errorMessages ) { 
        
                   IsSuccess = isSucces;
                   Message = errorMessages;
                   Data = data;  }



        public static Result Success(string? messages = null, object data) 
        {

            return new Result(data, true, messages);                
        }

        public static Result Failure(string messages,object? data = null) {
            return new Result(data, false, messages);
        }
    
            
    }
}
