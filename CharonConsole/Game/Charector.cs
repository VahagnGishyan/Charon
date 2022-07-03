using System;
using System.Collections.Generic;
using System.Text;

using Output;
using Utility;

namespace Game
{
    public class Charector :  ConsoleObject
    {
        public Charector(Location loc, char symbol, ConsoleColor foregroundColor, ConsoleColor backgroundColor) :
            base(loc, symbol, foregroundColor, backgroundColor)
        {
            LastSymbol      = ((char)Output.ConsoleSymbols.Space);
        }

        public Charector(Charector charector) : 
            this(charector.Loc, charector.Symbol, charector.ForegroundColor, charector.BackgroundColor)
        {
        }

        public char LastSymbol { get; set; }
    }

    public class Zomby : Charector
    {
        public Zomby(Location loc, char symbol, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
            : base(loc, symbol, foregroundColor, backgroundColor)
        {

        }

        static public Hero MakeDefault(Location loc)
        {
            return (new Hero(loc, 'Z', System.ConsoleColor.Red, System.ConsoleColor.Black));
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
            return (new Hero(loc, 'H', System.ConsoleColor.Green, System.ConsoleColor.Black)); ; ;
        }
    }
}
