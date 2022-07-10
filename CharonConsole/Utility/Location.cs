using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    public class Ordinate
    {
        public Ordinate() { }
        public Ordinate(int value) { Value = value; }
        public Ordinate(Ordinate ord) : this(ord.Value) { }
        public int Value { get; set; }
    }
    public class Abscissa
    {
        public Abscissa() { }
        public Abscissa(int value) { Value = value; }
        public Abscissa(Abscissa abs) : this(abs.Value) { }
        public int Value { get; set; }
    }

    public class Location
    {
        public enum ShiftTo
        {
            Unset,

            Right,
            Left,
            Up,
            Down,

            RightUp,
            RightDown,
            LeftUp,
            LeftDown
        }

        public Location() { }
        public Location(Ordinate ordinate, Abscissa abscissa)
        {
            AbscissaValue = new Abscissa(abscissa);
            OrdinateValue = new Ordinate(ordinate);
        }

        public Location(Location loc) : this(loc.OrdinateValue, loc.AbscissaValue)
        {

        }

        public static bool IsValid(Location loc)
        {
            return ((loc.OrdinateValue.Value >= 0) && (loc.AbscissaValue.Value >= 0));
        }

        public static Location Shift(Location loc, ShiftTo key)
        {
            Location newloc = loc;
            switch (key)
            {
                case ShiftTo.Right : newloc = ShiftRight(loc); break;
                case ShiftTo.Left  : newloc = ShiftLeft (loc); break;
                case ShiftTo.Up    : newloc = ShiftUp   (loc); break;
                case ShiftTo.Down  : newloc = ShiftDown (loc); break;

                case ShiftTo.RightUp   : newloc = ShiftRightUp  (loc); break;
                case ShiftTo.RightDown : newloc = ShiftRightDown(loc); break;
                case ShiftTo.LeftUp    : newloc = ShiftLeftUp   (loc); break;
                case ShiftTo.LeftDown  : newloc = ShiftLeftDown (loc); break;

                default          :                          break;
            }
            return (newloc);
        }

        public static Location ShiftRight(Location loc)
        {
            Location newLoc = new Location(loc);
            newLoc.AbscissaValue.Value += 1;
            return (newLoc);
        }
        public static Location ShiftLeft(Location loc)
        {
            Location newLoc = new Location(loc);
            newLoc.AbscissaValue.Value -= 1;
            return (newLoc);
        }
        public static Location ShiftUp(Location loc)
        {
            Location newLoc = new Location(loc);
            newLoc.OrdinateValue.Value -= 1;
            return (newLoc);
        }
        public static Location ShiftDown(Location loc)
        {
            Location newLoc = new Location(loc);
            newLoc.OrdinateValue.Value += 1;
            return (newLoc);
        }

        public static Location ShiftRightUp(Location loc)
        {
            Location newLoc = new Location(loc);
            newLoc = ShiftRight(newLoc);
            newLoc = ShiftUp(newLoc);
            return (newLoc);
        }

        public static Location ShiftRightDown(Location loc)
        {
            Location newLoc = new Location(loc);
            newLoc = ShiftRight(newLoc);
            newLoc = ShiftDown(newLoc);
            return (newLoc);
        }

        public static Location ShiftLeftUp(Location loc)
        {
            Location newLoc = new Location(loc);
            newLoc = ShiftLeft(newLoc);
            newLoc = ShiftUp(newLoc);
            return (newLoc);
        }

        public static Location ShiftLeftDown(Location loc)
        {
            Location newLoc = new Location(loc);
            newLoc = ShiftLeft(newLoc);
            newLoc = ShiftDown(newLoc);
            return (newLoc);
        }

        public Utility.Ordinate OrdinateValue { get; set; }
        public Utility.Abscissa AbscissaValue { get; set; }
    }
}
