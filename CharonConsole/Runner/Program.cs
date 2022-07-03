
using System;
using System.Text;

using Loging;
using Game;
using Input;

namespace Runner
{
    class Program
    {
        public static string InputAsString(ConsoleKeyInfo input)
        {
            string message = "You input " + input.KeyChar;

            bool alt     = Input.Keyboard.IsPressedAlt(input);
            bool control = Input.Keyboard.IsPressedControl(input);
            bool shift   = Input.Keyboard.IsPressedShift(input);

            if (alt || control || shift)
            {
                message += " with";
                message += alt ? " alt" : "";

                message += control ? alt ? ", control" : " control" : "";
                message += shift ? alt || control ? ", shift" : " shift" : "";
            }
            return (message);
        }

        static void Main(string[] args)
        {
            Loger.WriteLineMessage("Start Main()");
            // temp
            System.Console.WriteLine("Start Main()");

            //Process process = Game.Content.MakeEmpty();
            //Process process;
            //process.SetMap(Game.Content.CreateMap.Smile());
            //process.SetConsoleSize();
            //process.AddBorderInConsole();
            //process.AddMapInConsole();
            //process.AddHeroInConsole();

            //process.StartGame();
            ConsoleKeyInfo input = Input.Keyboard.UserPass();
            Loger.WriteMessage(InputAsString(input));

            while (!Input.Keyboard.IsPressedEnter(input))
            {
                //process.MoveHeroPosition(input);
                //Console.SetCursorPosition(myCharectorLocatin.OrdinateValue.Value, myCharectorLocatin.AbscissaValue.Value);
                // Console.Write(' ');

                //switch (input.Key)
                //{
                //    case ConsoleKey.W: --myCharectorLocatin.AbscissaValue.Value; break;
                //    case ConsoleKey.D: ++myCharectorLocatin.OrdinateValue.Value; break;
                //    case ConsoleKey.S: ++myCharectorLocatin.AbscissaValue.Value; break;
                //    case ConsoleKey.A: --myCharectorLocatin.OrdinateValue.Value; break;
                //    default: break;
                //}

                input = Input.Keyboard.UserPass();
                Loger.WriteMessage(InputAsString(input));
            }

            Loger.WriteLineMessage("Close Main()");
            Loger.Close();
        }
    }
}

// TODO
// Input, W,D,S,A, ctrl, shift, alt
// Game.Map       <= Console
// Game.Charactor <= Hero
// Game.Map       <= Interactive
// Game.Charactor <= Zombies
// Game.Charactor <= Interactive Hero
//
