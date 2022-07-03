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

        public Utility.Ordinate OrdinateValue { get; set; }
        public Utility.Abscissa AbscissaValue { get; set; }
    }
}
