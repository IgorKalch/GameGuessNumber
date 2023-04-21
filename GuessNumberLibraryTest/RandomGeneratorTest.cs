
using GuessNumberLibrary;

namespace GuessNumberLibraryTest
{
    [TestClass]
    public class RandomGeneratorTest
    {
        [TestMethod]
        public void ChooseInt_RangeOfValues()
        {
            const int value = 1;

            IRandomGenerator _generator = new RandomGenerator();

            var result = _generator.GetValue(value, value);

            Assert.AreEqual(value, result);
        }
    }
}
    