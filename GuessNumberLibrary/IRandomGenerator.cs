namespace GuessNumberLibrary
{
    public interface IRandomGenerator
    {
        public int GetValue(int minValue, int maxValue);
    }
}