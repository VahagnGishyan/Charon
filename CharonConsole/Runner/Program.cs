﻿
using System;
using System.Text;

using Loging;
using Game;
using UserInput;

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
            ConsoleKeyInfo input = UserInput.Keyboard.Input();
            Loger.WriteInputMessage(input);

            while (input.Key != ConsoleKey.Escape)
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

                input = UserInput.Keyboard.Input();
                Loger.WriteInputMessage(input);
            }

            Loger.WriteLineMessage("Close Main()");
            Loger.Close();
        }
    }
}

// TODO
// exits border, map
// charactor
// interactiv map
// 
// zombies
//
