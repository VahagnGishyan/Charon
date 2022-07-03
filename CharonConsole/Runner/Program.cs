
using System;
using System.Text;

using Loging;
using Game;
using Input;

namespace Runner
{
    class Program
    {
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
            Input.Keyboard.UserPass();
            while (!Input.Keyboard.IsPressedEscape())
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

                Input.Keyboard.UserPass();
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
