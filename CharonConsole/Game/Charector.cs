using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Output;
using Utility;

namespace Game
{
    public class Charector :  ConsoleObject
    {
        public Charector(Location loc, char symbol, ConsoleColor foregroundColor, ConsoleColor backgroundColor) :
            base(loc, symbol, foregroundColor, backgroundColor)
        {
            Dead = false;
            LastSymbol      = ((char)Output.ConsoleSymbols.Space);
        }

        public Charector(Charector charector) : 
            this(charector.Loc, charector.Symbol, charector.ForegroundColor, charector.BackgroundColor)
        {
        }

        public char LastSymbol { get; set; } // always ' ' 
        public bool Dead       { get; set; }
    }

    public class Zomby : Charector
    {
        public Zomby(Location loc, char symbol = ((char)Output.ConsoleSymbols.Zomby), 
            ConsoleColor foregroundColor = ConsoleColor.Red, 
            ConsoleColor backgroundColor = ConsoleColor.Black)
            : base(loc, symbol, foregroundColor, backgroundColor)
        {

        }

        static public Hero MakeDefault(Location loc)
        {
            return (new Hero(loc, ((char)Output.ConsoleSymbols.Zomby), System.ConsoleColor.Red, System.ConsoleColor.Black));
        }
    }

    public class Hero : Charector
    {
        public Hero(Location loc, char symbol, ConsoleColor foregroundColor, ConsoleColor backgroundColor) 
            : base(loc, symbol, foregroundColor, backgroundColor)
        {

        }

        public Hero(Hero hero) : base(hero.Loc, hero.Symbol, hero.ForegroundColor, hero.BackgroundColor) { }

        static public Hero MakeDefault(Location loc = null)
        {
            return (new Hero(loc, ((char)Output.ConsoleSymbols.Hero), System.ConsoleColor.Green, System.ConsoleColor.Black));
        }

        ///////////////////////////////////////////////////////////////////////////////////////

        private void BoomChooseLocation(ref List<Location> locsLine, ref List<Location> locsDign)
        {
            //step 1, horizontal
            locsLine.Add(Utility.Location.Shift(Loc, Location.ShiftTo.Right));
            locsLine.Add(Utility.Location.Shift(Loc, Location.ShiftTo.Left));
            locsLine.Add(Utility.Location.Shift(Loc, Location.ShiftTo.Up));
            locsLine.Add(Utility.Location.Shift(Loc, Location.ShiftTo.Down));

            //step 2 verticall
            locsDign.Add(Utility.Location.Shift(Loc, Location.ShiftTo.RightUp));
            locsDign.Add(Utility.Location.Shift(Loc, Location.ShiftTo.RightDown));
            locsDign.Add(Utility.Location.Shift(Loc, Location.ShiftTo.LeftUp));
            locsDign.Add(Utility.Location.Shift(Loc, Location.ShiftTo.LeftDown));

            locsLine = ConsoleMap.ChooseIfBreakable(locsLine);
            locsDign = ConsoleMap.ChooseIfBreakable(locsDign);
        }

        private void BoomAnimation(List<Location> locsLine, List<Location> locsDign)
        {
            const char symbol = ((char)Output.ConsoleSymbols.Boom);

            ConsoleMap.Write(locsLine, symbol);
            Thread.Sleep(120);
            ConsoleMap.Write(locsDign, symbol);
            Thread.Sleep(120);
            ConsoleMap.Write(locsLine, (char)Output.ConsoleSymbols.Space);
            Thread.Sleep(120);
            ConsoleMap.Write(locsDign, (char)Output.ConsoleSymbols.Space);
        }

        public void Boom()
        {
            List<Location> locsLine = new List<Location>(); // lines :: horizontal, verticall
            List<Location> locsDign = new List<Location>(); // diagonal

            BoomChooseLocation(ref locsLine, ref locsDign);
            BoomAnimation(locsLine, locsDign);

            //ConsoleMap.Write(locsLine, (char)Output.ConsoleSymbols.Space);
            //ConsoleMap.Write(locsDign, (char)Output.ConsoleSymbols.Space);
        }

        public static void HeroBoom(dynamic dhero)
        {
            Hero hero = dhero;
            hero.Boom();
        }

        ///////////////////////////////////////////////////////////////////////////

        public List<Location> FireChooseLocDirections(Location.ShiftTo[] direction)
        {
            List<Location> locations = new List<Location>();
            for(int index = 0; index < direction.Length; ++index)
            {
                Location newloc = Location.Shift(Loc, direction[index]);
                
                if (ConsoleMap.IsMovable(newloc))
                {
                    locations.Add(newloc);
                }
                if(ConsoleMap.IsZombyCurrentLocation(newloc))
                {
                    Process.KillZomby(newloc);
                }
            }
            return (locations);
        }

        public List<Location> FireChooseLocDirectionUp()
        {
            Location.ShiftTo[] direction = { Location.ShiftTo.Up, Location.ShiftTo.RightUp, Location.ShiftTo.LeftUp };
            return (FireChooseLocDirections(direction));
            //ConsoleMap.AddIfMovableLocations(ref locations, ConsoleMap.ShiftUp     (Loc));
            //ConsoleMap.AddIfMovableLocations(ref locations, ConsoleMap.ShiftRightUp(Loc));
            //ConsoleMap.AddIfMovableLocations(ref locations, ConsoleMap.ShiftLeftUp (Loc));

            //return (locations);
        }

        public List<Location> FireChooseLocDirectionRight()
        {
            Location.ShiftTo[] direction = { Location.ShiftTo.Right, Location.ShiftTo.RightUp, Location.ShiftTo.RightDown };
            return (FireChooseLocDirections(direction));
            //List<Location> locations = new List<Location>();

            //ConsoleMap.AddIfMovableLocations(ref locations, ConsoleMap.ShiftRight    (Loc));
            //ConsoleMap.AddIfMovableLocations(ref locations, ConsoleMap.ShiftRightUp  (Loc));
            //ConsoleMap.AddIfMovableLocations(ref locations, ConsoleMap.ShiftRightDown(Loc));

            //return (locations);
        }

        public List<Location> FireChooseLocDirectionLeft()
        {
            Location.ShiftTo[] direction = { Location.ShiftTo.Left, Location.ShiftTo.LeftUp, Location.ShiftTo.LeftDown };
            return (FireChooseLocDirections(direction));
            //List<Location> locations = new List<Location>();
            
            //ConsoleMap.AddIfMovableLocations(ref locations, ConsoleMap.ShiftLeft    (Loc));
            //ConsoleMap.AddIfMovableLocations(ref locations, ConsoleMap.ShiftLeftUp  (Loc));
            //ConsoleMap.AddIfMovableLocations(ref locations, ConsoleMap.ShiftLeftDown(Loc));

            //return (locations);
        }

        public List<Location> FireChooseLocDirectionDown()
        {

            Location.ShiftTo[] direction = { Location.ShiftTo.Down, Location.ShiftTo.RightDown, Location.ShiftTo.LeftDown };
            return (FireChooseLocDirections(direction));
            //List<Location> locations = new List<Location>();

            //ConsoleMap.AddIfMovableLocations(ref locations, ConsoleMap.ShiftDown(Loc));
            //ConsoleMap.AddIfMovableLocations(ref locations, ConsoleMap.ShiftRightDown(Loc));
            //ConsoleMap.AddIfMovableLocations(ref locations, ConsoleMap.ShiftLeftDown(Loc));

            //return (locations);
        }

        public List<Location> FireChooseLocDirections(Utility.Location.ShiftTo direction)
        {
            List<Location> locations = null;
            switch (direction)
            {
                case Utility.Location.ShiftTo.Up: locations    = FireChooseLocDirectionUp   (); break;
                case Utility.Location.ShiftTo.Right: locations = FireChooseLocDirectionRight(); break;
                case Utility.Location.ShiftTo.Left: locations  = FireChooseLocDirectionLeft (); break;
                case Utility.Location.ShiftTo.Down: locations  = FireChooseLocDirectionDown (); break;
                default: locations = new List<Location>(); break;
            }
            return (locations);
        }

        public List<Location> FireUpdateLocDirections(List<Location> locations, Utility.Location.ShiftTo direction)
        {
            List<Location> newLocations = new List<Location>();
            for (int index = 0; index < locations.Count; ++index)
            {
                Location newloc = Location.Shift(locations[index], direction);
                if(ConsoleMap.IsWithinBorder(newloc))
                {
                    if(ConsoleMap.IsMovable(newloc) || ConsoleMap.IsZombyCurrentLocation(newloc))
                    {
                        newLocations.Add(newloc);
                    }
                }
            }

            return (newLocations);
        }

        public void FireAnimation(List<Location> locations, char symbol)
        {
            for(int index = 0; index < locations.Count; ++index)
            {
                if (ConsoleMap.IsZombyCurrentLocation(locations[index]))
                {
                    Process.KillZomby(locations[index]);
                }
            }
            ConsoleMap.Write(locations, symbol);
            Thread.Sleep(100);
            ConsoleMap.Write(locations, ((char)Output.ConsoleSymbols.Space));
        }

        public void Fire()
        {
            const char symbol = ((char)Output.ConsoleSymbols.Fire);

            System.ConsoleKeyInfo    input     = Input.Keyboard.LastPassedKey();
            Utility.Location.ShiftTo direction = ConsoleMap.GetDirection(input);
            List<Location>           locations = FireChooseLocDirections(direction);

            if (locations == null)
            {
                Loging.Loger.WriteWarning("[Game::Process], func HeroPowerFire(), location is null");
                return;
            }

            while (locations.Count != 0)
            {
                FireAnimation(locations, symbol);
                locations = FireUpdateLocDirections(locations, direction);
            }
        }

        public static void HeroFire(dynamic dhero)
        {
            Hero hero = dhero;
            hero.Fire();
        }
    }
}
