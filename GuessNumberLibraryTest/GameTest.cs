using GuessNumberLibrary;
using Moq;
using System.Reflection;
using System.Resources; 

namespace GuessNumberLibraryTest
{
    [TestClass]
    public class GameTest
    { 
        [TestMethod]
        public void Play_WinInOneTurn_Success()
        {
            Queue<string> messages = new();
            messages.Enqueue("Please enter a number:");
            messages.Enqueue("Congratulation, you guess!");
            Func<string> input = () => "1";
            Action<string> output = (string message) => {
                Assert.AreEqual(messages.Dequeue(), message);
            };

            Settings settings = new Settings { Attempts = 1, Range = new GameRange { Min = 1, Max = 2 } };
            ResourceManager rm = new ResourceManager("GuessNumberLibraryTest.Properties.Resources", Assembly.GetExecutingAssembly());
            Mock<IRandomGenerator> generator = new();
            generator
                .Setup(e => e.GetValue(It.IsAny<int>(), It.IsAny<int>()))
                .Callback<int, int>((min, max) => {
                    Assert.AreEqual(1, min);
                    Assert.AreEqual(2, max);
                })
                .Returns(1);
            Game game = new Game(settings, generator.Object, rm);
            game.OnNotify += (Game sender) => {  };
            game.Play();

        }

        [TestMethod]
        public void Play_WinInTwoTurn_Success()
        {
            Queue<int> answers = new();
            answers.Enqueue(1);
            answers.Enqueue(2);

            Queue<string> messages = new();
            messages.Enqueue("Please enter a number:");
            messages.Enqueue("My number is bigger.");
            messages.Enqueue("You do not guess, attempt left:");
            messages.Enqueue("2");
            messages.Enqueue("Congratulation, you guess!");
            Func<string> input = () => answers.Dequeue().ToString();
            Action<string> output = (string message) => {
                Assert.AreEqual(messages.Dequeue(), message);
            };

            Settings settings = new Settings { Attempts = 3, Range = new GameRange { Min = 1, Max = 2 } };
            ResourceManager rm = new ResourceManager("GuessNumberLibraryTest.Properties.Resources", Assembly.GetExecutingAssembly());
            Mock<IRandomGenerator> generator = new();
            generator
                .Setup(e => e.GetValue(It.IsAny<int>(), It.IsAny<int>()))
                .Callback<int, int>((min, max) => {
                    Assert.AreEqual(1, min);
                    Assert.AreEqual(2, max);
                })
                .Returns(2);
            Game game = new Game(settings, generator.Object, rm);
            game.OnNotify += (Game sender) => { };
            game.Play();

        }

        [TestMethod]
        public void Play_NotGuessBigger_Lose()
        {
            Queue<int> answers = new();
            answers.Enqueue(1);
            answers.Enqueue(1);

            Queue<string> messages = new();
            messages.Enqueue("Please enter a number:");
            messages.Enqueue("My number is bigger.");
            messages.Enqueue("You do not guess, attempt left:");
            messages.Enqueue("0");
            messages.Enqueue("You lose. ");
            messages.Enqueue("Press 'Y' to play again.");
            Func<string> input = () => answers.Dequeue().ToString();
            Action<string> output = (string message) => {
                Assert.AreEqual(messages.Dequeue(), message);
            };

            Settings settings = new Settings { Attempts = 1, Range = new GameRange { Min = 1, Max = 2 } };
            ResourceManager rm = new ResourceManager("GuessNumberLibraryTest.Properties.Resources", Assembly.GetExecutingAssembly());
            Mock<IRandomGenerator> generator = new();
            generator
                .Setup(e => e.GetValue(It.IsAny<int>(), It.IsAny<int>()))
                .Callback<int, int>((min, max) => {
                    Assert.AreEqual(1, min);
                    Assert.AreEqual(2, max);
                })
                .Returns(2);
            Game game = new Game(settings,generator.Object, rm);
            game.OnNotify += (Game sender) => { };
            game.Play();

        }

        [TestMethod]
        public void Play_NotGuessSmaller_Lose()
        {
            Queue<int> answers = new();
            answers.Enqueue(4);
            answers.Enqueue(4);

            Queue<string> messages = new();
            messages.Enqueue("Please enter a number:");
            messages.Enqueue("My number is smaller.");
            messages.Enqueue("You do not guess, attempt left:");
            messages.Enqueue("0");
            messages.Enqueue("You lose. ");
            messages.Enqueue("Press 'Y' to play again.");
            Func<string> input = () => answers.Dequeue().ToString();
            Action<string> output = (string message) => {
                Assert.AreEqual(messages.Dequeue(), message);
            };

            Settings settings = new Settings { Attempts = 1, Range = new GameRange { Min = 1, Max = 2 } };
            ResourceManager rm = new ResourceManager("GuessNumberLibraryTest.Properties.Resources", Assembly.GetExecutingAssembly());
            Mock<IRandomGenerator> generator = new();
            generator
                .Setup(e => e.GetValue(It.IsAny<int>(), It.IsAny<int>()))
                .Callback<int, int>((min, max) => {
                    Assert.AreEqual(1, min);
                    Assert.AreEqual(2, max);
                })
                .Returns(2);
            Game game = new Game(settings,generator.Object, rm);
            game.OnNotify += (Game sender) => { };
            game.Play();

        }


    }
}