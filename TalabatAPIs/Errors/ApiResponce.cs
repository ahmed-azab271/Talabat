
namespace TalabatAPIs.Errors
{
    // To Make My Own Errors Responce Messages
    public class ApiResponce
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public ApiResponce(int statusCode , string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(StatusCode);
        }

        private string? GetDefaultMessageForStatusCode(int statusCode)
        {
            return StatusCode switch
            {
                400 => "Bad Request",
                401 => "You Are Not Auhorized",
                404 => "Resource Not Found",
                500 => "Internal Server Error",
                _ => null
            };

            /*Same Same*/
            //switch (StatusCode)
            //{
            //    case 400 :
            //        return "Bad Request";
            //    case 401 :
            //        return "You Are Not Auhorized";
            //    case 404:
            //        return "Resource Not Found";
            //    case 500:
            //        return "Internal Server Error";
            //    default :
            //        return null;
            //}

        }
    }
}
