namespace DesafioDataSystem.Exceptions.ExceptionBase
{
    

    public class ErrorOnValidationException : DesafioDataSystemException
    {
        public IList<string> ErrorMessages { get; set; }

        public ErrorOnValidationException(IList<string> errorMessages)
        {
            ErrorMessages = errorMessages;
        }
    }
}
