namespace GuessNumberLibrary
{
    public class Settings 
    {
        public const string SettingsKey = "gameSettings";
        public const int MaxOfAttempts = 10;

        public GameRange Range { get; set; }
        public int Attempts { get; set; }
        public bool AttemptsIsValid =>  Attempts > 0 && Attempts < MaxOfAttempts;
        public bool RangeIsValid => Range.Min <= Range.Max;

    }
}
