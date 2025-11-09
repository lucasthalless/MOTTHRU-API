namespace MOTTHRU.API.Domain.Errors
{
    public class NoContentException : Exception
    {
        public string Code { get; set; }

        public NoContentException()
        {
        }

        public NoContentException(string? message) : base(message)
        {
            Code = "ERRO 1234";
        }
    }

}