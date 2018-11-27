namespace SevenTiny.Cloud.MultiTenantPlatform.Web.Models
{
    public class ResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public static ResponseModel Success(object data, string message = null)
            => new ResponseModel { IsSuccess = true, Data = data, Message = message };

        public static ResponseModel Error(string message)
            => new ResponseModel { IsSuccess = false, Message = message };
    }
}
