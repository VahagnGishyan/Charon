using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

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

        public static void SetHero()
        {
            Hero = Game.Hero.MakeDefault(Map.HeroStartLocation);
        }

        public static void SetHero(Game.Hero hero)
        {
            Hero = hero;
        }

        public static void AddCharector(Charector charector)
        {
            if (Charectors == null)
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

            int upBorderOrd = Loc.OrdinateValue.Value;
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

            PrintHorizontalBorder(size);
            PrintVerticallBorder(size);

            Output.Console.SetDefaultCursorPosition();
        }

        public static void PrintMap()
        {
            var size = Map.GetSize();
            ConsolePoint cobj;
            Location loc = new Location(new Ordinate(0), new Abscissa(0));
            for (; loc.OrdinateValue.Value < size.HeightValue.Value; ++loc.OrdinateValue.Value)
            {
                for (; loc.AbscissaValue.Value < size.WeightValue.Value; ++loc.AbscissaValue.Value)
                {
                    cobj = Map.GetCPoint(loc);
                    if (cobj.Symbol == ((char)ConsoleSymbols.Space))
                    {
                        continue;
                    }
                    SetCursorPositionInBorder(loc);
                    Output.Console.Write(cobj);
                }
                loc.AbscissaValue.Value = 0;
            }
            Output.Console.SetDefaultState();
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
            return (IsWithinBorder(loc) && Map.IsMovable(loc));
        }

        public static bool IsBreakable(Location loc)
        {
            char symbol = Map.GetCPoint(loc).Symbol;
            if (symbol == ((char)Output.ConsoleSymbols.Fence) || symbol == ((char)Output.ConsoleSymbols.Space))
            {
                return true;
            }
            return false;
        }

        public static void SetCursorPositionInBorder(Location loc)
        {
            Location locInBorder = new Location(loc);
            locInBorder.AbscissaValue.Value += 1;
            locInBorder.OrdinateValue.Value += 1;
            
            Output.Console.SetCursorPosition(locInBorder);
        }

        public enum CtrlArg
        {
            Unset,
            DefaultState,
            Movable
        }

        public static void WriteInBorder(Location loc, char symbol, CtrlArg state = CtrlArg.Unset)
        {
            SetCursorPositionInBorder(loc);
            Output.Console.Write(symbol);
            if(state == CtrlArg.DefaultState)
            {
                Output.Console.SetDefaultState();
            }
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

        private static Utility.Location.ShiftTo MoveCharectorDirection(System.ConsoleKeyInfo input)
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
                if (IsWithinBorder(newLoc) && IsMovable(newLoc))
                {
                    SetCursorPositionInBorder(charector.Loc);
                    Output.Console.Write(charector.LastSymbol);
                    MoveCharectorInConsole(charector, newLoc);
                    Output.Console.SetDefaultState();
                }
            }
        }

        public static void MoveHero()
        {
            MoveCharector(Hero, Input.Keyboard.LastPassedKey());
        }

        private static void ShiftHorizontalVerticallAndWrite(Location loc, char symbol)
        {
            Location newloc = null;

            //substep 1, horizontal
            Output.Console.Write(symbol);
            newloc = Utility.Location.Shift(loc, Location.ShiftTo.Left);
            Output.Console.Write(symbol);

            //substep 2, verticall
            newloc = Utility.Location.Shift(loc, Location.ShiftTo.Up);
            Output.Console.Write(symbol);
            newloc = Utility.Location.Shift(loc, Location.ShiftTo.Down);
            Output.Console.Write(symbol);
        }

        private static List<Location> ChooseIfBreakable(List<Location> locations)
        {
            List<Location> newlocs = new List<Location>();
            for (int index = 0; index < locations.Count; ++index)
            {
                if (IsWithinBorder(locations[index]) && IsBreakable(locations[index]))
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

        public static void Write(List<Location> locs, char symbol)
        {
            for (int index = 0; index < locs.Count; ++index)
            {
                Process.WriteInBorder(locs[index], symbol);
            }
            Output.Console.SetDefaultState();
        }

        private static void HeroPowerBoomChooseLocation(ref List<Location> locsLine, ref List<Location> locsDign)
        {
            Location heroloc = Hero.Loc;

            //step 1, horizontal
            locsLine.Add(Utility.Location.Shift(heroloc, Location.ShiftTo.Right));
            locsLine.Add(Utility.Location.Shift(heroloc, Location.ShiftTo.Left ));
            locsLine.Add(Utility.Location.Shift(heroloc, Location.ShiftTo.Up   ));
            locsLine.Add(Utility.Location.Shift(heroloc, Location.ShiftTo.Down ));

            //step 2 verticall
            locsDign.Add(Utility.Location.Shift(heroloc, Location.ShiftTo.RightUp  ));
            locsDign.Add(Utility.Location.Shift(heroloc, Location.ShiftTo.RightDown));
            locsDign.Add(Utility.Location.Shift(heroloc, Location.ShiftTo.LeftUp   ));
            locsDign.Add(Utility.Location.Shift(heroloc, Location.ShiftTo.LeftDown ));

            locsLine = ChooseIfBreakable(locsLine);
            locsDign = ChooseIfBreakable(locsDign);
        }

        private static void HeroPowerBoomAnimation(List<Location> locsLine, List<Location> locsDign)
        {
            const char symbol = 'b';

            Write(locsLine, symbol);
            Thread.Sleep(120);
            Write(locsDign, symbol);
            Thread.Sleep(120);
            Write(locsLine, (char)Output.ConsoleSymbols.Space);
            Thread.Sleep(120);
            Write(locsDign, (char)Output.ConsoleSymbols.Space);
        }

        private static void HeroPowerChangeMap(Location loc, ConsolePoint point)
        {
            Map.ChangePoint(loc, point);
        }

        private static void HeroPowerChangeMap(Location loc, char symbol)
        {
            HeroPowerChangeMap(loc, new ConsolePoint(symbol));
        }

        private static void HeroPowerChangeMap(List<Location> locs, char symbol)
        {
            for (int index = 0; index < locs.Count; ++index)
            {
                Process.HeroPowerChangeMap(locs[index], symbol);
            }
        }

        private static void HeroPowerChangeMap(List<Location> locsLine, List<Location> locsDign, 
                                                char symbol = (char)Output.ConsoleSymbols.Space)
        {
            HeroPowerChangeMap(locsLine, symbol);
            HeroPowerChangeMap(locsDign, symbol);
        }


        public static void HeroPowerBoom()
        {
            List<Location> locsLine = new List<Location>(); // lines :: horizontal, verticall
            List<Location> locsDign = new List<Location>(); // diagonal

            HeroPowerBoomChooseLocation(ref locsLine, ref locsDign);
            HeroPowerBoomAnimation(locsLine, locsDign);
            HeroPowerChangeMap(locsLine, locsDign);
        }

        public static Location ShiftRight(Location loc)
        {
            Location newLoc = Location.ShiftRight(loc);
            if(Process.IsMovable(newLoc))
            {
                return newLoc;
            }
            return (null);
        }
        public static Location ShiftLeft(Location loc)
        {
            Location newLoc = Location.ShiftLeft(loc);
            if (Process.IsMovable(newLoc))
            {
                return newLoc;
            }
            return (null);
        }
        public static Location ShiftUp(Location loc)
        {
            Location newLoc = Location.ShiftUp(loc);
            if (Process.IsMovable(newLoc))
            {
                return newLoc;
            }
            return (null);
        }
        public static Location ShiftDown(Location loc)
        {
            Location newLoc = Location.ShiftDown(loc);
            if (Process.IsMovable(newLoc))
            {
                return newLoc;
            }
            return (null);
        }

        public static Location ShiftRightUp(Location loc)
        {
            Location newLoc = Location.ShiftRightUp(loc);
            if (Process.IsMovable(newLoc))
            {
                return newLoc;
            }
            return (null);
        }
        public static Location ShiftRightDown(Location loc)
        {
            Location newLoc = Location.ShiftRightDown(loc);
            if (Process.IsMovable(newLoc))
            {
                return newLoc;
            }
            return (null);
        }
        public static Location ShiftLeftUp(Location loc)
        {
            Location newLoc = Location.ShiftLeftUp(loc);
            if (Process.IsMovable(newLoc))
            {
                return newLoc;
            }
            return (null);
        }
        public static Location ShiftLeftDown(Location loc)
        {
            Location newLoc = Location.ShiftLeftDown(loc);
            if (Process.IsMovable(newLoc))
            {
                return newLoc;
            }
            return (null);
        }

        public static Location Shift(Location loc, Location.ShiftTo direction)
        {
            Location newLoc = Location.Shift(loc, direction);
            if (Process.IsMovable(newLoc))
            {
                return newLoc;
            }
            return (null);
        }

        private static void AddLocation(ref List<Location> locations, Location loc)
        {
            if (loc != null)
            {
                locations.Add(loc);
            }
        }

        public static List<Location> HeroPowerFireChooseLocDirectionUp()
        {
            Location loc = Hero.Loc;
            List<Location> locations = new List<Location>();

            AddLocation(ref locations, Process.ShiftUp(loc));
            AddLocation(ref locations, Process.ShiftRightUp(loc));
            AddLocation(ref locations, Process.ShiftLeftUp(loc));

            return (locations);
        }

        public static List<Location> HeroPowerFireChooseLocDirectionRight()
        {
            Location loc = Hero.Loc;
            List<Location> locations = new List<Location>();

            AddLocation(ref locations, Process.ShiftRight(loc));
            AddLocation(ref locations, Process.ShiftRightUp(loc));
            AddLocation(ref locations, Process.ShiftRightDown(loc));

            return (locations);
        }

        public static List<Location> HeroPowerFireChooseLocDirectionLeft()
        {
            Location loc = Hero.Loc;
            List<Location> locations = new List<Location>();

            AddLocation(ref locations, Process.ShiftLeft(loc));
            AddLocation(ref locations, Process.ShiftLeftUp(loc));
            AddLocation(ref locations, Process.ShiftLeftDown(loc));

            return (locations);
        }

        public static List<Location> HeroPowerFireChooseLocDirectionDown()
        {
            Location loc = Hero.Loc;
            List<Location> locations = new List<Location>();

            AddLocation(ref locations, Process.ShiftDown(loc));
            AddLocation(ref locations, Process.ShiftRightDown(loc));
            AddLocation(ref locations, Process.ShiftLeftDown(loc));

            return (locations);
        }

        public static List<Location> HeroPowerFireChooseLocDirections(Utility.Location.ShiftTo direction)
        {
            List<Location> locations = null;
            switch (direction)
            {
                case Utility.Location.ShiftTo.Up:    locations = Process.HeroPowerFireChooseLocDirectionUp();    break;
                case Utility.Location.ShiftTo.Right: locations = Process.HeroPowerFireChooseLocDirectionRight(); break;
                case Utility.Location.ShiftTo.Left:  locations = Process.HeroPowerFireChooseLocDirectionLeft();  break;
                case Utility.Location.ShiftTo.Down:  locations = Process.HeroPowerFireChooseLocDirectionDown();  break;
                default:                             locations = new List<Location>();                           break;
            }
            return (locations);
        }

        public static List<Location> HeroPowerFireUpdateLocDirections(List<Location> locations, Utility.Location.ShiftTo direction)
        {
            List<Location> newLocations = new List<Location>();
            for(int index = 0; index < locations.Count; ++index)
            {
                AddLocation(ref newLocations, Shift(locations[index], direction));
            }

            return (newLocations);
        }

        public static void HeroPowerFireAnimation(List<Location> locations, char symbol)
        {
            Write(locations, symbol);
            Thread.Sleep(100);
            Write(locations, ((char)Output.ConsoleSymbols.Space));
        }

        public static void HeroPowerFire()
        {
            const char symbol = 'f';
            //int length = 3;

            System.ConsoleKeyInfo input = Input.Keyboard.LastPassedKey();
            Utility.Location.ShiftTo direction = MoveCharectorDirection(input);
            List<Location> locations = HeroPowerFireChooseLocDirections(direction);

            if (locations == null)
            {
                Loging.Loger.WriteWarning("[Game::Process], func HeroPowerFire(), location is null");
                return;
            }

            while (locations.Count != 0)
            {
                HeroPowerFireAnimation(locations, symbol);
                locations = HeroPowerFireUpdateLocDirections(locations, direction);
            }
        }



        public static void HeroPowerSpace()
        {
            Location loc = Hero.Loc;
        }

        public static void PowerHero()
        {
            Charector charector         = Hero;
            System.ConsoleKeyInfo input = Input.Keyboard.LastPassedKey();

            switch (input.Key)
            {
                case ConsoleKey.B:        HeroPowerBoom (); break;
                case ConsoleKey.Spacebar: HeroPowerSpace(); break;
                default: break;
            }
        }

        public static bool IsMoveKey()
        {
            var symbol = Input.Keyboard.LastPassedKey().Key;
            return (symbol == ConsoleKey.W || symbol == ConsoleKey.A ||
                    symbol == ConsoleKey.S || symbol == ConsoleKey.D);
        }

        public static bool IsPowerKey()
        {
            var symbol = Input.Keyboard.LastPassedKey().Key;
            return (symbol == ConsoleKey.B || symbol == ConsoleKey.Separator);
        }

        public static void UserPass()
        {
            Input.Keyboard.UserPass();
            if (IsMoveKey())
            {
                if (Input.Keyboard.IsPressedShift())
                {
                    Process.HeroPowerFire();
                }
                else
                {
                    Process.MoveHero();
                }
            }
            else if(IsPowerKey())
            {
                Process.PowerHero();
            }
        }

        private static Location Loc        { get; set; } = new Location(new Ordinate(0), new Abscissa(0));
        static Game.Map         Map        { get; set; }
        static List<Charector>  Charectors { get; set; }
        static Charector        Hero       { get; set; }
    }
}

//TODO
// CleanCode #
// 
//