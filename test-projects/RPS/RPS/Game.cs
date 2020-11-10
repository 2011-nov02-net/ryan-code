using System;
using System.Collections.Generic;
using System.Text;

namespace RockPaperScissors.ConsoleApp
{
    public class Game
    {
        private int gameID;
        private string winner;
        private string loser;
        private Option winnerOption;
        private Option loserOption;
        private List<Game> gameLog = new List<Game>();

        public Game()
        {
            
        }

        public Game(int id, string winner, string loser, Option winnerOption, Option loserOption)
        {
            this.gameID = id;
            this.winner = winner;
            this.loser = loser;
            this.winnerOption = winnerOption;
            this.loserOption = loserOption;
        }

        public string PlayGame(int id, Option userInput, Option aiInput)
        {
            string result;
            string w;
            string l;
            Option winOption;
            Option loseOption;

            if(userInput == Option.Rock)
            {
                if(aiInput == Option.Rock)
                {
                    w = "Tie";
                    l = "Tie";
                    winOption = Option.Rock;
                    loseOption = Option.Rock;
                    result = ("TIE!");
                }
                else if (aiInput == Option.Paper)
                {
                    w = "AI";
                    l = "User";
                    winOption = Option.Paper;
                    loseOption = Option.Rock;
                    result = ("AI has beat User with paper against rock!");
                }
                else
                {
                    w = "User";
                    l = "AI";
                    winOption = Option.Rock;
                    loseOption = Option.Scissors;
                    result = ("User had beat AI with rock against scissors!");
                }
            }
            else if(userInput == Option.Paper)
            {
                if (aiInput == Option.Paper)
                {
                    w = "Tie";
                    l = "Tie";
                    winOption = Option.Paper;
                    loseOption = Option.Paper;
                    result = ("TIE!");
                    result = ("TIE!");
                }
                else if (aiInput == Option.Rock)
                {
                    w = "User";
                    l = "AI";
                    winOption = Option.Paper;
                    loseOption = Option.Rock;
                    result = ("User has beat AI with paper against rock!");
                }
                else
                {
                    w = "AI";
                    l = "User";
                    winOption = Option.Scissors;
                    loseOption = Option.Paper;
                    result = ("AI had beat User with scissors against paper!");
                }
            }
            else
            {
                if (aiInput == Option.Scissors)
                {
                    w = "TIE";
                    l = "TIE";
                    winOption = Option.Scissors;
                    loseOption = Option.Scissors;
                    result = ("TIE!");
                }
                else if (aiInput == Option.Rock)
                {
                    w = "AI";
                    l = "User";
                    winOption = Option.Rock;
                    loseOption = Option.Scissors;
                    result = ("AI has beat User with rock against scissors!");
                }
                else
                {
                    w = "User";
                    l = "AI";
                    winOption = Option.Scissors;
                    loseOption = Option.Paper;
                    result = ("User had beat AI with scissors against paper!");
                }
            }

            LogGame(id, w, l, winOption, loseOption);
            return "\n" + result;
        }

        public void LogGame(int id, string winner, string loser, Option winnerOption, Option loserOption)
        {
            gameLog.Add(new Game(id, winner, loser, winnerOption, loserOption));
        }

        public string DisplayLog()
        {
            var report = new System.Text.StringBuilder();
            report.AppendLine("\nID\t\tWinner\t\tLoser\t\tWinner Option\t\tLoser Option");

            foreach (var item in gameLog)
            {
                report.AppendLine($"{item.gameID}\t\t{item.winner}\t\t{item.loser}\t\t{item.winnerOption}\t\t\t{item.loserOption}");
            }

            return report.ToString();
        }
    }
}
