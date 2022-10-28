namespace CashFlow.Common.API.Responses
{
    public class DefaultResponse<T>
    {
        public DefaultResponse(T data)
        {
            Data = data;
        }

        public T Data { get; set; }
    }
}
