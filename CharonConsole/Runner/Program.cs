
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

            //Process.SetMap(Game.Content.MakeEmpty());
            Process.SetMap(Game.Content.Creeper());
            Process.PrintBorder();
            Process.PrintMap();
            //process.AddHeroInConsole();

            //process.StartGame();
            Input.Keyboard.UserPass();
            while (!Input.Keyboard.IsPressedEscape())
            {
                //process.MoveHeroPosition(input);
                //Console.SetCursorPosition(myCharectorLocatin.OrdinateValue.Value, myCharectorLocatin.AbscissaValue.Value);
                // Console.Write(' ');

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
