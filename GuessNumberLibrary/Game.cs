using GuessNumberLibrary.Exceptions;
using System.Resources;

namespace GuessNumberLibrary
{
    public class Game
    {
        private const string RangeExceptionKey = "RangeException";
        private const string AttemptExceptionKey = "AttemptException";

        private readonly Settings _settings;
        private readonly IRandomGenerator _generator;
        private readonly ResourceManager _rm;
        private string _userInput;

        public delegate void Notify(Game sender);
        public Notify OnNotify;

        public GameStep Step { get; private set; } = GameStep.Unknown;
        public int AttemptsLeft { get; private set; }

        public Game(Settings settings,IRandomGenerator generator, ResourceManager rm)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));

            Attemptslidation();
            RangeValidation();

            _generator = generator ?? throw new ArgumentNullException(nameof(generator));
            _rm = rm ?? throw new ArgumentNullException(nameof(rm));
        }       

        public void Play()
        {
            ArgumentNullException.ThrowIfNull(OnNotify); 

            AttemptsLeft = _settings.Attempts;
            var numberToGuess = _generator.GetValue(_settings.Range.Min, _settings.Range.Max);

            NotifyStep(GameStep.Start);
            do
            {
                NotifyStep(GameStep.Turn);

                GameStep nextStep = GameStep.Unknown;
                if (!int.TryParse(_userInput, out int userNumber))
                {
                    nextStep = GameStep.AnswerWrongData;
                }
                else if (userNumber < numberToGuess)
                {
                    nextStep = GameStep.AnswerSmoller;
                }
                else if (userNumber > numberToGuess)
                {
                    nextStep = GameStep.AnswerBigger;
                }
                else if (userNumber == numberToGuess)
                {
                    nextStep = GameStep.Win;
                }

                if(userNumber != numberToGuess && --AttemptsLeft <= 0 )
                {
                    nextStep = GameStep.Lose;
                }

                NotifyStep(nextStep);

            } while (AttemptsLeft > 0 && Step != GameStep.Win);

            NotifyStep(GameStep.End);
        }

        public void SetUserGuess(string guess)
        {
            _userInput = guess;
        }

        private void RangeValidation()
        {
            if (!_settings.RangeIsValid)
            {
               throw new InvalidRangeDataException(_rm.GetString(RangeExceptionKey), _settings.Range);
            }

        }
        private void Attemptslidation()
        {
            if (!_settings.AttemptsIsValid)
            {
                throw new InvalidAttemptDataException(_rm.GetString(AttemptExceptionKey), _settings.Attempts);
            }
        }

        private void NotifyStep(GameStep step)
        {
            Step = step;
            OnNotify.Invoke(this);
        }
    }
}
