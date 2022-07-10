using System;
using System.Collections.Generic;
using System.Text;

using Utility;
using Output;

namespace Game
{

    public class Map
    {
        public Map(Size size, Location heroLocation) 
        {
            HeroStartLocation = heroLocation;
            Points = new ConsolePoint[size.HeightValue.Value, size.WeightValue.Value];

            for (int iLine = 0; iLine < size.HeightValue.Value; ++iLine)
            {
                for(int iItr = 0; iItr < size.WeightValue.Value; ++iItr)
                {
                    Points[iLine, iItr] = new ConsolePoint((char)ConsoleSymbols.Space);
                }
            }
        }

        public Size GetSize()
        {
            //temp
            return (new Size(new Height(Points.GetLength(0)), new Weight(Points.GetLength(1))));
        }

        public bool IsSpace(Location loc)
        {
            var symbol = Points[loc.OrdinateValue.Value, loc.AbscissaValue.Value].Symbol;
            return (symbol == ((char)ConsoleSymbols.Space));
        }
        
        public bool IsMovable(Location loc)
        {
            return (IsSpace(loc));
        }

        public ConsolePoint GetCPoint(Location loc)
        {
            return (Points[loc.OrdinateValue.Value, loc.AbscissaValue.Value]);
        }

        public void ChangePoint(Location loc, ConsolePoint point)
        {
            Points[loc.OrdinateValue.Value, loc.AbscissaValue.Value] = new ConsolePoint(point);
        }

        public Location HeroStartLocation { get; }

        private ConsolePoint[,] Points { get; set; }

        //////////////////////////////////////////////////////////////////////
        
        public static Map MakeEmpty()
        {
            Size size = new Size(new Height(10), new Weight(10));
            //Size size = new Size(new Height(48), new Weight(140));

            //Size size = new Size(new Height(8), new Weight(8));
            Location heroStartLocation = new Location(new Ordinate(5), new Abscissa(5));
            //Location heroStartLocation = new Location(new Ordinate(23), new Abscissa(69));

            return (new Map(size, heroStartLocation));

        }

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

            Location heroStartLocation = new Location(new Ordinate(3), new Abscissa(4));
            Size size = new Size(new Height(strmap.Length), new Weight(strmap[0].Length));
            Map map = new Map(size, heroStartLocation);

            for (int iLine = 0; iLine < size.HeightValue.Value; ++iLine)
            {
                for(int iItr = 0; iItr < size.WeightValue.Value; ++iItr)
                {
                    char symbol = strmap[iLine][iItr];
                    if (symbol == '-')
                    {
                        map.Points[iLine, iItr].Symbol = ' ';
                        continue;
                    }
                    map.Points[iLine, iItr].Symbol = strmap[iLine][iItr];
                }
            }

            return (map);

        }
    }
}
