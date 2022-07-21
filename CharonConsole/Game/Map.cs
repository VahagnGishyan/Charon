using System;
using System.Collections.Generic;
using System.Text;

using Utility;
using Output;
using System.Threading;

namespace Game
{
    
    public class Map
    {
        public Map(Size size)
        {
            Points = new ConsolePoint[size.HeightValue.Value, size.WeightValue.Value];
        }
        public Map(Size size, Location heroLocation, Location zombyLocation) : this(size)
        {
            HeroStartLocation  = heroLocation;
            ZombyStartLocation = zombyLocation;

            for (int iLine = 0; iLine < size.HeightValue.Value; ++iLine)
            {
                for(int iItr = 0; iItr < size.WeightValue.Value; ++iItr)
                {
                    Points[iLine, iItr] = new ConsolePoint((char)ConsoleSymbols.Space);
                }
            }
        }

        private ConsolePoint GetPointFromContent(string[] content, Location loc)
        {
            char symbol = content[loc.OrdinateValue.Value][loc.AbscissaValue.Value];
            if (symbol == '-' || symbol == ((char)Output.ConsoleSymbols.Space))
            {
                return (new ConsolePoint((char)Output.ConsoleSymbols.Space));
            }
            return (new ConsolePoint(symbol));
        }

        public Map(string[] content, Location heroLocation, Location zombyLocation) 
            : this (new Size(new Height(content.Length), new Weight(content[0].Length)))
        {
            Size size          = GetSize();
            HeroStartLocation  = heroLocation;
            ZombyStartLocation = zombyLocation;

            Location loc = Location.MakeZero();
            for (; loc.OrdinateValue.Value < size.HeightValue.Value; ++loc.OrdinateValue.Value)
            {
                for (; loc.AbscissaValue.Value < size.WeightValue.Value; ++loc.AbscissaValue.Value)
                {
                    Points[loc.OrdinateValue.Value, loc.AbscissaValue.Value] = GetPointFromContent(content, loc);
                }
                loc.AbscissaValue.Value = 0;
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////

        public ConsolePoint GetPoint(Location loc)
        {
            return (new ConsolePoint(Points[loc.OrdinateValue.Value, loc.AbscissaValue.Value]));
        }

        //////////////////////////////////////////////////////////////////////////////////////////

        public void ChangePoint(Location loc, ConsolePoint point)
        {
            Points[loc.OrdinateValue.Value, loc.AbscissaValue.Value] = new ConsolePoint(point);
        }

        public void ChangePoint(Location loc, char point)
        {
            ChangePoint(loc, new ConsolePoint(point));
        }

        //////////////////////////////////////////////////////////////////////////////////////////

        public Size GetSize()
        {
            //temp
            return (new Size(new Height(Points.GetLength(0)), new Weight(Points.GetLength(1))));
        }

        //////////////////////////////////////////////////////////////////////////////////////////

        public Location ZombyStartLocation { get; }
        public Location HeroStartLocation  { get; }

        private ConsolePoint[,] Points { get; set; }

        //////////////////////////////////////////////////////////////////////
        
        //public static Map MakeEmpty()
        //{
        //    Size size = new Size(new Height(10), new Weight(10));
        //    //Size size = new Size(new Height(48), new Weight(140));

        //    //Size size = new Size(new Height(8), new Weight(8));
        //    Location heroStartLocation = new Location(new Ordinate(5), new Abscissa(5));
        //    Location zombyLocation = new Location(new Ordinate(0), new Abscissa(0));
        //    //Location heroStartLocation = new Location(new Ordinate(23), new Abscissa(69));

        //    return (new Map(size, heroStartLocation, zombyLocation));

        //}

        public static Map MakeCreeper()
        {
            string [] strmap =
            { // 12345678
                "        ", // 1
                "        ", // 2
                " FF  FF ", // 3
                " FF  FF ", // 4
                "   FF   ", // 5
                "  FFFF  ", // 6
                "  FFFF  ", // 7
                "  F  F  ", // 8
            };

            Loging.Loger.WriteLineMessage("Make Map");
            foreach (var line in strmap)
            {
                Loging.Loger.WriteLineMessage(line);
            }

            Location heroStartLocation  = new Location(new Ordinate(3), new Abscissa(4));
            Location zombyStartLocation = new Location(new Ordinate(0), new Abscissa(0));

            //Size size = new Size(new Height(strmap.Length), new Weight(strmap[0].Length));
            Map  map = new Map(strmap, heroStartLocation, zombyStartLocation);

            return (map);

        }
    }
}
