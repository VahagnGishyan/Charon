using System;
using System.Collections.Generic;
using System.Text;

using Utility;
using Output;
using System.Threading;

namespace Game
{

    public static class ConsoleMap
    {
        //private class ConsoleMapPoint : ConsolePoint
        //{
        //    public ConsoleMapPoint(char symbol, ConsoleColor foregroundColor = ConsoleColor.White,
        //                                 ConsoleColor backgroundColor = ConsoleColor.Black)
        //                                 : base(symbol, foregroundColor, backgroundColor)
        //    {

        //    }

        //    public ConsoleMapPoint(ConsolePoint point) : base(point)
        //    {

        //    }

        //    //public void ThreadUnsafeChangeSymbol(char symbol)
        //    //{
        //    //    this.Symbol = symbol;
        //    //    Console.Write()
        //    //}

        //    //public void ChangeSymbol(char symbol)
        //    //{
        //    //    while (!isPointFree) { Thread.Sleep(5); }
        //    //    isPointFree = false;

        //    //    ThreadUnsafeChangeSymbol(symbol);

        //    //    isPointFree = true;
        //    //}

        //    //private bool isPointFree = true;
        //}

        //////////////////////////////////////////////////////////////////////////////////////////

        private static void SetConsoleSize(Size size)
        {
            Output.Console.SetWindowSize(new Size(new Height(size.HeightValue.Value + 2),
                                      new Weight(size.WeightValue.Value + 2)));
        }

        private static void PrintHorizontalBorder(Size size)
        {
            int height = size.HeightValue.Value;
            int weight = size.WeightValue.Value;

            int upBorderOrd   = StartLocation.OrdinateValue.Value;
            int downBorderOrd = StartLocation.OrdinateValue.Value + weight + 1;
            for (Location loc = new Location(StartLocation);
                loc.AbscissaValue.Value < weight + 2;
                ++loc.AbscissaValue.Value)
            {
                loc.OrdinateValue.Value = upBorderOrd;
                Output.Console.Write(loc, (char)ConsoleSymbols.Border);
                loc.OrdinateValue.Value = downBorderOrd;
                Output.Console.Write(loc, (char)ConsoleSymbols.Border);
            }
        }

        private static void PrintVerticallBorder(Size size)
        {
            int height = size.HeightValue.Value;
            int weight = size.WeightValue.Value;

            int leftBorderAbs  = StartLocation.AbscissaValue.Value;
            int rightBorderAbs = StartLocation.AbscissaValue.Value + height + 1;
            Location loc = new Location(StartLocation);
            ++loc.OrdinateValue.Value;
            for (;
                loc.OrdinateValue.Value < height + 1;
                ++loc.OrdinateValue.Value)
            {
                loc.AbscissaValue.Value = leftBorderAbs;
                Output.Console.Write(loc, (char)ConsoleSymbols.Border);
                loc.AbscissaValue.Value = rightBorderAbs;
                Output.Console.Write(loc, (char)ConsoleSymbols.Border);
            }
        }

        private static void PrintBorder(Size size = null)
        {
            if (size == null)
            {
                size = GetSize();
            }

            PrintHorizontalBorder(size);
            PrintVerticallBorder(size);
        }

        public static void SetSize(Size size)
        {
            Points = new ConsolePoint[size.HeightValue.Value, size.WeightValue.Value];
            SetConsoleSize(size);
            PrintBorder(size);
        }

        public static Size GetSize()
        {
            return (new Size(new Height(Points.GetLength(0)), new Weight(Points.GetLength(1))));
        }

        public static Location LocInBorder(Location loc)
        {
            Location locInBorder = new Location(loc);
            locInBorder.OrdinateValue.Value += 1;
            locInBorder.AbscissaValue.Value += 1;
            return (locInBorder);
        }

        private static void PrintMap(Map map)
        {
            var size = GetSize();
            Location loc = Location.MakeZero();

            for (; loc.OrdinateValue.Value < size.HeightValue.Value; ++loc.OrdinateValue.Value)
            {
                for (; loc.AbscissaValue.Value < size.WeightValue.Value; ++loc.AbscissaValue.Value)
                {
                    Write(loc, map.GetPoint(loc));
                }
                loc.AbscissaValue.Value = 0;
            }
            Output.Console.SetDefaultState();
        }

        public static void SetMap(Map map, Location mapStartLocation = null)
        {
            if(mapStartLocation == null)
            {
                mapStartLocation = Location.MakeZero();
            }

            StartLocation      = mapStartLocation;
            HeroStartLocation  = map.HeroStartLocation;
            ZombyStartLocation = map.ZombyStartLocation;
            SetSize(map.GetSize());
            PrintMap(map);
        }

        public static void Write(ConsoleObject obj)
        {
            while (!isMapFree) { Thread.Sleep(5); } // see Write(List<Location> locs, char symbol)
            isMapFree = false;

            Points[obj.Loc.OrdinateValue.Value, obj.Loc.AbscissaValue.Value] = new ConsolePoint(obj);
            Output.Console.Write(LocInBorder(obj.Loc), obj);

            isMapFree = true;
        }

        public static void Write(Location loc, ConsolePoint point)
        {
            Write(new ConsoleObject(loc, point));
        }

        public static void Write(Location loc, char symbol)
        {
            Write(new ConsoleObject(loc, symbol));
        }

        public static void Write(List<Location> locs, ConsolePoint point)
        {
            while (!isMapFree) { Thread.Sleep(5); }
            isMapFree = false;

            for (int index = 0; index < locs.Count; ++index)
            {
                Points[locs[index].OrdinateValue.Value, locs[index].AbscissaValue.Value] = new ConsolePoint(point);
                Output.Console.Write(LocInBorder(locs[index]), point);
            }
            Output.Console.SetDefaultState();

            isMapFree = true;
        }

        public static void Write(List<Location> locs, char symbol)
        {
            Write(locs, new ConsolePoint(symbol));
        }

        public static ConsolePoint GetPoint(Location loc)
        {
            return (new ConsolePoint(Points[loc.OrdinateValue.Value, loc.AbscissaValue.Value]));
        }

        //////////////////////////////////////////////////////////////////////////////////////////

        public static bool IsSpace(Location loc)
        {
            var symbol = Points[loc.OrdinateValue.Value, loc.AbscissaValue.Value].Symbol;
            return (symbol == ((char)ConsoleSymbols.Space));
        }

        public static bool IsWithinBorder(Location loc)
        {
            var size = GetSize();
            return (Location.IsValid(loc) &&
                    (size.HeightValue.Value > loc.AbscissaValue.Value) &&
                    (size.WeightValue.Value > loc.OrdinateValue.Value));
        }

        public static bool IsPointMovable(Location loc)
        {
            var symbol = Points[loc.OrdinateValue.Value, loc.AbscissaValue.Value].Symbol;
            return (symbol == ((char)ConsoleSymbols.Space));
        }

        public static bool IsMovable(Location loc)
        {
            return (IsWithinBorder(loc) && IsPointMovable(loc));
        }

        public static bool IsBreakable(Location loc)
        {
            char symbol = GetPoint(loc).Symbol;
            if (symbol == (char)Output.ConsoleSymbols.Fence)
            {
                return true;
            }
            return false;
        }

        public static bool IsHeroCurrentLocation(Location loc)
        {
            if(!IsWithinBorder(loc))
            {
                return (false);
            }
            char symbol = GetPoint(loc).Symbol;
            return (symbol == (char)Output.ConsoleSymbols.Hero);
        }

        public static bool IsZombyCurrentLocation(Location loc)
        {
            if (!IsWithinBorder(loc))
            {
                return (false);
            }
            char symbol = GetPoint(loc).Symbol;
            return (symbol == (char)Output.ConsoleSymbols.Zomby);
        }

        public static bool IsZombyMovableLocation(Location loc)
        {
            return (IsMovable(loc) || IsHeroCurrentLocation(loc));
        }

        public static bool ZombyStartLocationFree()
        {
            return IsZombyMovableLocation(ConsoleMap.ZombyStartLocation);
        }

        /////////////////////////////////////////////////////////////////////////////////////

        public static void AddIfMovableLocations(ref List<Location> locations, Location loc)
        {
            if (loc != null)
            {
                locations.Add(loc);
            }
        }

        public static List<Location> MovableLocations(Location loc)
        {
            List<Location> locations = new List<Location>();
            Location.ShiftTo[] durs = { Location.ShiftTo.Right, Location.ShiftTo.Up,
                                        Location.ShiftTo.Left,  Location.ShiftTo.Down};

            for (int index = 0; index < durs.Length; ++index)
            {
                Location newloc = Shift(loc, durs[index]);
                if(newloc != null)
                {
                    locations.Add(newloc);
                }
            }
            return (locations);
        }

        public static List<Location> ChooseIfBreakable(List<Location> locations)
        {
            List<Location> newlocs = new List<Location>();
            for (int index = 0; index < locations.Count; ++index)
            {
                if (IsWithinBorder(locations[index]) && (IsBreakable(locations[index]) || IsSpace(locations[index])))
                {
                    newlocs.Add(locations[index]);
                }
                else
                {
                    locations[index] = null;
                }
            }
            return (newlocs);
        }

        ////////////////////////////////////////////////////////////////////

        public static void MoveCharector(Charector charector)
        {
            Write(charector.Loc, charector);
            Output.Console.SetDefaultState();
        }

        public static void MoveCharector(Charector charector, Location loc)
        {
            Output.Console.SetDefaultConsoleColor();
            Write(charector.Loc, charector.LastSymbol);
            charector.Loc = loc;
            MoveCharector(charector);
        }

        public static Utility.Location.ShiftTo GetDirection(System.ConsoleKeyInfo input)
        {
            switch (input.Key)
            {
                case ConsoleKey.W: return (Utility.Location.ShiftTo.Up);
                case ConsoleKey.D: return (Utility.Location.ShiftTo.Right);
                case ConsoleKey.S: return (Utility.Location.ShiftTo.Down);
                case ConsoleKey.A: return (Utility.Location.ShiftTo.Left);
                default: break;
            }
            return (Location.ShiftTo.Unset);
        }

        public static void MoveCharector(Charector charector, System.ConsoleKeyInfo input)
        {
            Location newLoc = null;
            switch (input.Key)
            {
                case ConsoleKey.W: newLoc = Utility.Location.ShiftUp(charector.Loc); break;
                case ConsoleKey.D: newLoc = Utility.Location.ShiftRight(charector.Loc); break;
                case ConsoleKey.S: newLoc = Utility.Location.ShiftDown(charector.Loc); break;
                case ConsoleKey.A: newLoc = Utility.Location.ShiftLeft(charector.Loc); break;
                default: break;
            }

            if (newLoc != null)
            {
                if (ConsoleMap.IsMovable(newLoc))
                {
                    ConsoleMap.MoveCharector(charector, newLoc);
                }
            }
        }

        public static Location ShiftRight(Location loc)
        {
            Location newLoc = Location.ShiftRight(loc);
            if (IsMovable(newLoc))
            {
                return newLoc;
            }
            return (null);
        }
        public static Location ShiftLeft(Location loc)
        {
            Location newLoc = Location.ShiftLeft(loc);
            if (IsMovable(newLoc))
            {
                return newLoc;
            }
            return (null);
        }
        public static Location ShiftUp(Location loc)
        {
            Location newLoc = Location.ShiftUp(loc);
            if (IsMovable(newLoc))
            {
                return newLoc;
            }
            return (null);
        }
        public static Location ShiftDown(Location loc)
        {
            Location newLoc = Location.ShiftDown(loc);
            if (IsMovable(newLoc))
            {
                return newLoc;
            }
            return (null);
        }

        public static Location ShiftRightUp(Location loc)
        {
            Location newLoc = Location.ShiftRightUp(loc);
            if (IsMovable(newLoc))
            {
                return newLoc;
            }
            return (null);
        }
        public static Location ShiftRightDown(Location loc)
        {
            Location newLoc = Location.ShiftRightDown(loc);
            if (IsMovable(newLoc))
            {
                return newLoc;
            }
            return (null);
        }
        public static Location ShiftLeftUp(Location loc)
        {
            Location newLoc = Location.ShiftLeftUp(loc);
            if (IsMovable(newLoc))
            {
                return newLoc;
            }
            return (null);
        }
        public static Location ShiftLeftDown(Location loc)
        {
            Location newLoc = Location.ShiftLeftDown(loc);
            if (IsMovable(newLoc))
            {
                return newLoc;
            }
            return (null);
        }

        public static Location Shift(Location loc, Location.ShiftTo direction)
        {
            Location newLoc = Location.Shift(loc, direction);
            if (IsMovable(newLoc))
            {
                return newLoc;
            }
            return (null);
        }

        ////////////////////////////////////////////////////////////////////


        public static void AddCharector(Location loc, Charector charector)
        {
            Write(loc, charector);
        }
        public static void AddCharector(Charector charector)
        {
            AddCharector(charector.Loc, charector);
        }

        public static void SetHero(Hero hero)
        {
            Write(hero);
        }

        public static void AddZomby(Zomby zomby)
        {
            Write(ZombyStartLocation, zomby);
        }

        //////////////////////////////////////////////////////////////////////////////////////////

        // for multithreading
        private static bool isMapFree = true;

        //////////////////////////////////////////////////////////////////////////////////////////

        public static Location ZombyStartLocation { get; set; }
        public static Location HeroStartLocation { get; set; }

        private static Location StartLocation { get; set; } = Location.MakeZero();
        private static ConsolePoint[,] Points { get; set; }
    }
}
