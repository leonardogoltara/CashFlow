namespace CashFlow.Operation
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
