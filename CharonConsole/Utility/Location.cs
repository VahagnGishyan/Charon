using System;
using System.Collections.Generic;
using System.Linq;
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
            Invalid,
            Unset,
            Stay,

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

        public static Location MakeZero()
        {
            return (new Location(new Ordinate(0), new Abscissa(0)));
        }

        public static bool IsValid(Location loc)
        {
            return ((loc.OrdinateValue.Value >= 0) && (loc.AbscissaValue.Value >= 0));
        }

        public static bool IsEqual(Location left, Location right)
        {
            return (left.OrdinateValue.Value == right.OrdinateValue.Value &&
                    left.AbscissaValue.Value == right.AbscissaValue.Value);
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

        public static ShiftTo SolveDirection(Location locFrom, Location locTo)
        {
            int ordDiff = locTo.OrdinateValue.Value - locFrom.OrdinateValue.Value;
            int absDiff = locTo.AbscissaValue.Value - locFrom.AbscissaValue.Value;
            if (Enumerable.Range(-1, 1).Contains(ordDiff) && 
                Enumerable.Range(-1, 1).Contains(absDiff) &&
                (ordDiff != absDiff)                      &&
                (ordDiff == 0 || absDiff == 0)
                ) 
            {
                return (ShiftTo.Invalid);
            }
            else if(ordDiff == 0)
            {
                if(absDiff == 1)
                {
                    return (ShiftTo.Right);
                }
                //else if(absDiff == -1)
                //{
                    return (ShiftTo.Left);
                //}
            }
            else
            {
                if (ordDiff == 1)
                {
                    return (ShiftTo.Down);
                }
                //else if(ordDiff == -1)
                //{
                    return (ShiftTo.Up);
                //}
            }
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
