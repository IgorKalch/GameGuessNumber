namespace GuessNumberLibrary
{
    public class RandomGenerator : IRandomGenerator
    {
        Random _random;

        public RandomGenerator()
        {
            _random = new Random();
        }

        public int GetValue(int minValue, int maxValue)
        {
            int number = _random.Next(minValue, maxValue + 1);

            return number;
        }
    }
}