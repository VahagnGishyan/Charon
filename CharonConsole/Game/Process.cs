using System;
using System.Collections.Generic;
using System.Text;

using Output;
using Utility;

namespace Game
{
    public class Process
    {
        public Process() { }

        public void SetMap(Game.Map map)
        {
            Map = map;
        }

        public void AddHero(Game.Hero hero)
        {
            Hero = new Hero(hero);
        }

        public void AddCharector(Charector charector)
        {
            if(Charectors == null)
            {
                Charectors = new List<Charector>();
            }
            Charectors.Add(new Charector(charector));
        }


        public void PrintBorder()
        {
            Size size = Map.GetSize();
            int height = size.HeightValue.Value + 2;
            int weight = size.WeightValue.Value + 2;

            Output.Console.SetWindowSize(new Size(new Height(height), new Weight(weight)));

            // print horizontal
            int upBorderOrd = Loc.OrdinateValue.Value;
            int downBorderOrd = Loc.OrdinateValue.Value + weight - 1;
            for (Location loc = new Location(new Ordinate(0), new Abscissa(Loc.AbscissaValue.Value));
                loc.AbscissaValue.Value < height; 
                ++loc.AbscissaValue.Value)
            {


                loc.OrdinateValue.Value = upBorderOrd;
                Output.Console.SetCursorPosition(loc);
                Output.Console.Write(((char)ConsoleSymbols.Border));
                loc.OrdinateValue.Value = downBorderOrd;
                Output.Console.SetCursorPosition(loc);
                Output.Console.Write(((char)ConsoleSymbols.Border));
            }

            // print vertical
            int leftBorderAbs  = Loc.AbscissaValue.Value;
            int rightBorderAbs = Loc.AbscissaValue.Value + height - 1;
            for (Location loc = new Location(new Ordinate(Loc.OrdinateValue.Value), new Abscissa(0));
                loc.OrdinateValue.Value < weight;
                ++loc.OrdinateValue.Value)
            {
                loc.AbscissaValue.Value = leftBorderAbs;
                Output.Console.SetCursorPosition(loc);
                Output.Console.Write(((char)ConsoleSymbols.Border));
                loc.AbscissaValue.Value = rightBorderAbs;
                Output.Console.SetCursorPosition(loc);
                Output.Console.Write(((char)ConsoleSymbols.Border));
            }

            Output.Console.SetDefaultCursorPosition();
        }

        public void AddMapInConsole()
        {
            var size = Map.GetSize();
            ConsolePoint cobj;
            for (int iLine = 0; iLine < size.HeightValue.Value; ++iLine)
            {
                for (int iItr = 0; iItr < size.WeightValue.Value; ++iItr)
                {
                    Location loc = new Location(new Ordinate(iLine), new Abscissa(iItr));
                    cobj = Map.GetCPoint(loc);
                    SetCursorPositionInBorder(loc);
                    Output.Console.Write(cobj);
                    Output.Console.SetDefaultState();
                }
            }
        }


        public void StartGame()
        {
            // MainCharector-Hero
            AddHeroInConsole();
        }

        public void AddHeroInConsole()
        {
            Hero.Loc = Map.HeroStartLocation;
            MoveCharectorInConsole(Hero);
            Output.Console.SetDefaultState();
        }

        public bool IsWithinBorder(Location loc)
        {
            var size = Map.GetSize();
            return (Location.IsValid(loc) &&
                    (size.HeightValue.Value > loc.AbscissaValue.Value) && 
                    (size.WeightValue.Value > loc.OrdinateValue.Value));
        }

        public bool IsMovable(Location loc)
        {
            return (Map.IsMovable(loc));
        }

        public Location MoveRight(Location loc)
        {
            Location newLoc = new Location(loc);
            newLoc.AbscissaValue.Value += 1;
            return (newLoc);
        }
        public Location MoveLeft(Location loc)
        {
            Location newLoc = new Location(loc);
            newLoc.AbscissaValue.Value -= 1;
            return (newLoc);
        }
        public Location MoveUp(Location loc)
        {
            Location newLoc = new Location(loc);
            newLoc.OrdinateValue.Value -= 1;
            return (newLoc);
        }
        public Location MoveDown(Location loc)
        {
            Location newLoc = new Location(loc);
            newLoc.OrdinateValue.Value += 1;
            return (newLoc);
        }

        public void SetCursorPositionInBorder(Location loc)
        {
            Location locInBorder = new Location(loc);
            locInBorder.AbscissaValue.Value += 1;
            locInBorder.OrdinateValue.Value += 1;

            Output.Console.SetCursorPosition(locInBorder);
        }

        public void MoveCharectorInConsole(Charector charector)
        {
            SetCursorPositionInBorder(charector.Loc);
            Output.Console.SetConsoleColor(charector.BackgroundColor, charector.ForegroundColor);
            Output.Console.Write(charector.Symbol);
            Output.Console.SetDefaultState();
        }

        public void MoveCharectorInConsole(Charector charector, Location loc)
        {
            charector.Loc = loc;
            MoveCharectorInConsole(charector);
        }

        public void MoveCharector(Charector charector, System.ConsoleKeyInfo input)
        {
            Location newLoc = null;
            switch (input.Key)
            {
                case ConsoleKey.W: newLoc = MoveUp   (charector.Loc); break;
                case ConsoleKey.D: newLoc = MoveRight(charector.Loc); break;
                case ConsoleKey.S: newLoc = MoveDown (charector.Loc); break;
                case ConsoleKey.A: newLoc = MoveLeft (charector.Loc); break;
                default: break;
            }

            if(newLoc != null)
            {
                if (IsWithinBorder(newLoc) && IsMovable(newLoc))
                {
                    SetCursorPositionInBorder(charector.Loc);
                    Output.Console.Write(charector.LastSymbol);
                    MoveCharectorInConsole(charector, newLoc);
                }
            }
        }

        public void MoveHeroPosition(System.ConsoleKeyInfo input)
        {
            MoveCharector(Hero, input);
        }

        private Location Loc        { get; set; } = new Location(new Ordinate(0), new Abscissa(0));
        Game.Map         Map        { get; set; }
        List<Charector>  Charectors { get; set; }
        Charector        Hero       { get; set; }
    }
}
