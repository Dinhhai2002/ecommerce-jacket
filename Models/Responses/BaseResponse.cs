namespace webecommerce.Models.Response
{
    public class BaseResponse<T>
    {
        public int Status { get; set; } = 200;
        public string MessageError { get; set; }
        public T Data { get; set; }
        public BaseResponse() { }
        public BaseResponse(T data)
        {
            Data = data;
        }
    }
} 