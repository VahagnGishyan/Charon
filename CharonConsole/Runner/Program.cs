
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
            Process.SetHero();
            Process.LetOutZombies();
            
            do
            {
                Process.UserPass();
            }
            while (!Input.Keyboard.IsPressedEscape() && !Process.IsGameOver());

            //if (Process.IsGameOver())
            //{
                //System.Console.ReadKey(); 
            //}
            if (Input.Keyboard.IsPressedEscape())
            {
                Process.GameOver();
            }

            System.Console.ReadKey();
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
// Game.Charactor <= Zombies          # Done
// Game.Charactor <= Interactive Hero # Done
// Game.Process   <= ReDesign         # Done
//
