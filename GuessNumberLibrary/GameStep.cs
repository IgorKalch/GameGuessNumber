using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessNumberLibrary
{
    public enum GameStep
    {
        Unknown,
        Start,
        Turn,
        AnswerWrongData,
        AnswerSmoller,
        AnswerBigger,
        Lose,
        Win,
        End
    }
}
