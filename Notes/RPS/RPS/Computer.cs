using System;
using System.Collections.Generic;
using System.Text;

namespace RockPaperScissors.ConsoleApp
{
    class Computer
    {
        public Computer()
        {

        }

        public Option GetOption()
        {
            Option option = Option.Rock;

            Random rnd = new Random();
            int r = rnd.Next(1, 4);

            switch(r)
            {
                case 1:
                    option = Option.Rock;
                    break;
                case 2:
                    option = Option.Paper;
                    break;
                case 3:
                    option = Option.Scissors;
                    break;
            }

            return option;
        }
    }
}
