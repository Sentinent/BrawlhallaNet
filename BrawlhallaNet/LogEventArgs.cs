namespace BrawlhallaNet
{
    public class LogEventArgs
    {
        public string Message;
        public object Response;

        internal LogEventArgs(string message, object response)
        {
            this.Message = message;
            this.Response = response;
        }
    }
}
