using System;
using System.Collections.Generic;
using System.Text;

using Output;
using Utility;

namespace Game
{
    public static class Process
    {
        static Process() { }

        public static void SetMap(Game.Map map)
        {
            Map = map;
        }

        public static void SetHero(Game.Hero hero)
        {
            Hero = hero;
        }

        public static void AddCharector(Charector charector)
        {
            if(Charectors == null)
            {
                Charectors = new List<Charector>();
            }
            Charectors.Add(charector);
        }

        private static void SetWindowSizeForMapSize(Size size)
        {
            Output.Console.SetWindowSize(new Size(new Height(size.HeightValue.Value + 2), 
                                         new Weight(size.WeightValue.Value + 2)));
        }

        private static void PrintHorizontalBorder(Size size)
        {
            int height = size.HeightValue.Value;
            int weight = size.WeightValue.Value;

            int upBorderOrd   = Loc.OrdinateValue.Value;
            int downBorderOrd = Loc.OrdinateValue.Value + weight + 1;
            for (Location loc = new Location(new Ordinate(Loc.OrdinateValue.Value), new Abscissa(Loc.AbscissaValue.Value));
                loc.AbscissaValue.Value < weight + 2;
                ++loc.AbscissaValue.Value)
            {
                loc.OrdinateValue.Value = upBorderOrd;
                Output.Console.SetCursorPosition(loc);
                Output.Console.Write(((char)ConsoleSymbols.Border));
                loc.OrdinateValue.Value = downBorderOrd;
                Output.Console.SetCursorPosition(loc);
                Output.Console.Write(((char)ConsoleSymbols.Border));
            }
        }

        private static void PrintVerticallBorder(Size size)
        {
            int height = size.HeightValue.Value;
            int weight = size.WeightValue.Value;

            int leftBorderAbs = Loc.AbscissaValue.Value;
            int rightBorderAbs = Loc.AbscissaValue.Value + height + 1;
            for (Location loc = new Location(new Ordinate(Loc.OrdinateValue.Value + 1), new Abscissa(Loc.AbscissaValue.Value));
                loc.OrdinateValue.Value < height + 1;
                ++loc.OrdinateValue.Value)
            {
                loc.AbscissaValue.Value = leftBorderAbs;
                Output.Console.SetCursorPosition(loc);
                Output.Console.Write(((char)ConsoleSymbols.Border));
                loc.AbscissaValue.Value = rightBorderAbs;
                Output.Console.SetCursorPosition(loc);
                Output.Console.Write(((char)ConsoleSymbols.Border));
            }
        }

        public static void PrintBorder()
        {
            Size size = Map.GetSize();

            SetWindowSizeForMapSize(size);

            PrintHorizontalBorder  (size);
            PrintVerticallBorder   (size);

            Output.Console.SetDefaultCursorPosition();
        }

        public static void PrintMap()
        {
            var size = Map.GetSize();
            ConsolePoint cobj;
            Location loc = new Location(new Ordinate(0), new Abscissa(0));
            for (int iLine = 0; iLine < size.HeightValue.Value; ++iLine)
            {
                loc.OrdinateValue.Value = iLine;
                for (int iItr = 0; iItr < size.WeightValue.Value; ++iItr)
                {
                    loc.AbscissaValue.Value = iItr;
                    cobj = Map.GetCPoint(loc);
                    SetCursorPositionInBorder(loc);
                    Output.Console.Write(cobj);
                    Output.Console.SetDefaultState();
                }
            }
        }


        public static void StartGame()
        {
            // MainCharector-Hero
            PrintHero();
        }

        public static void PrintHero()
        {
            Hero.Loc = Map.HeroStartLocation;
            MoveCharectorInConsole(Hero);
            Output.Console.SetDefaultState();
        }

        public static bool IsWithinBorder(Location loc)
        {
            var size = Map.GetSize();
            return (Location.IsValid(loc) &&
                    (size.HeightValue.Value > loc.AbscissaValue.Value) && 
                    (size.WeightValue.Value > loc.OrdinateValue.Value));
        }

        public static bool IsMovable(Location loc)
        {
            return (Map.IsMovable(loc));
        }

        public static Location MoveRight(Location loc)
        {
            Location newLoc = new Location(loc);
            newLoc.AbscissaValue.Value += 1;
            return (newLoc);
        }
        public static Location MoveLeft(Location loc)
        {
            Location newLoc = new Location(loc);
            newLoc.AbscissaValue.Value -= 1;
            return (newLoc);
        }
        public static Location MoveUp(Location loc)
        {
            Location newLoc = new Location(loc);
            newLoc.OrdinateValue.Value -= 1;
            return (newLoc);
        }
        public static Location MoveDown(Location loc)
        {
            Location newLoc = new Location(loc);
            newLoc.OrdinateValue.Value += 1;
            return (newLoc);
        }

        public static void SetCursorPositionInBorder(Location loc)
        {
            Location locInBorder = new Location(loc);
            locInBorder.AbscissaValue.Value += 1;
            locInBorder.OrdinateValue.Value += 1;

            Output.Console.SetCursorPosition(locInBorder);
        }

        public static void MoveCharectorInConsole(Charector charector)
        {
            SetCursorPositionInBorder(charector.Loc);
            Output.Console.SetConsoleColor(charector.BackgroundColor, charector.ForegroundColor);
            Output.Console.Write(charector.Symbol);
            Output.Console.SetDefaultState();
        }

        public static void MoveCharectorInConsole(Charector charector, Location loc)
        {
            charector.Loc = loc;
            MoveCharectorInConsole(charector);
        }

        public static void MoveCharector(Charector charector, System.ConsoleKeyInfo input)
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

        public static void MoveHero(System.ConsoleKeyInfo input)
        {
            MoveCharector(Hero, input);
        }

        private static Location Loc        { get; set; } = new Location(new Ordinate(0), new Abscissa(0));
        static Game.Map         Map        { get; set; }
        static List<Charector>  Charectors { get; set; }
        static Charector        Hero       { get; set; }
    }
}
