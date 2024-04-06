namespace FiyiStore.Middlewares
{
    public class ExceptionResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }

        public string NewPathOrQuestion { get; set; }
    }
}
