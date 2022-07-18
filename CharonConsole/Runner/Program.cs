
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
            Process.SetHero();
            Process.PrintHero();

            //Process.StartGame();
            Process.LetOutZombies();
            Process.UserPass();
            while (!Input.Keyboard.IsPressedEscape() && !Process.IsGameOver())
            {
                Process.UserPass();
            }

            if(!Process.IsGameOver())
            {
                System.Console.ReadKey();
            }
            if(Input.Keyboard.IsPressedEscape())
            {
                Process.GameOver();
            }

            Loger.WriteLineMessage("Close Main()");
            Loger.Close();
        }
    }
}

// TODO
// Input, W,D,S,A, ctrl, shift, alt
// Game.Map       <= Console          # Done
// Game.Charactor <= Hero             # Done
// Game.Map       <= Interactive      # ~Done
// Game.Charactor <= Zombies          #
// Game.Charactor <= Interactive Hero # Done
// Game.Process   <= ReDesign         # 
//
