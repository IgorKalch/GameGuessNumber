
namespace GuessNumberLibrary.Exceptions
{
    public class InvalidRangeDataException : Exception
    {
        public GameRange Range { get; private set; }
        public InvalidRangeDataException(string message) : base(message)
        {

        }
        public InvalidRangeDataException(string message, GameRange range) : base(message)
        {
            Range = range;
        }
    }
}
