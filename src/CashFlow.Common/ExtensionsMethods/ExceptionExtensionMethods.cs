using System.Text;

namespace CashFlow.Common.ExtensionsMethods
{
    public static class ExceptionExtensionMethods
    {
        public static string GetCompleteMessage(this Exception exception)
        {
            StringBuilder errorMessage = new StringBuilder();
            errorMessage.AppendLine(exception.Message);

            while (exception.InnerException != null)
            {
                exception = exception.InnerException;
                errorMessage.AppendLine(exception.Message);
            }

            return errorMessage.ToString();
        }
    }
}
