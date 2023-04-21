
namespace GuessNumberLibrary.Exceptions
{
    public  class InvalidAttemptDataException : Exception
    {
        public int Attempt { get; private set; }
        public InvalidAttemptDataException(string message) : base(message)
        {

        }
        public InvalidAttemptDataException(string message, int attempt) : base(message)
        {
            Attempt = attempt;
        }
    }
}
