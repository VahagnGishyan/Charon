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

            //Process process = Game.Content.MakeEmpty();
            Process process = Game.Content.Smile();
            process.PrintBorder();
            process.AddMapInConsole();

            process.StartGame();
            ConsoleKeyInfo input = UserInput.Keyboard.Input();
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

                if (input.Key != ConsoleKey.Escape)
                {
                    Loger.WriteLineMessage($"You input {UserInput.Keyboard.CheckInput(input)}");
                }
                else
                {
                    Loger.WriteLineMessage("You input Escape");
                }
                input = UserInput.Keyboard.Input();
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
