
using System;
using Utility;

namespace Output
{
    public enum ConsoleSymbols
    {
        Border = '#',
        Space  = ' ',
        Fence  = 'F'
    }
    public class ConsolePoint
    {
        public ConsolePoint(char symbol,
                            ConsoleColor foregroundColor = ConsoleColor.White,
                            ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            Symbol = symbol;
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
        }

        public ConsolePoint(ConsolePoint point) : this(point.Symbol, point.ForegroundColor, point.BackgroundColor)
        {

        }

        public char Symbol { get; set; }
        public ConsoleColor ForegroundColor { get; set; }
        public ConsoleColor BackgroundColor { get; set; }
    }

    public class ConsoleObject : ConsolePoint
    {
        public ConsoleObject(Utility.Location loc, char symbol, ConsoleColor foregroundColor, ConsoleColor backgroundColor) :
            base(symbol, foregroundColor, backgroundColor)
        {
            Loc = loc;
        }

        public Location Loc { get; set; }
    }

    public static class Console
    {
        public static void SetWindowSize(Utility.Size size)
        {
            System.Console.SetWindowSize(size.WeightValue.Value, size.HeightValue.Value);
        }

        public static void SetDefaultConsoleColor()
        {
            System.Console.ForegroundColor = ConsoleColor.White;
            System.Console.BackgroundColor = ConsoleColor.Black;
        }

        public static void SetConsoleColor(System.ConsoleColor backgroundColor,
                                                  System.ConsoleColor foregroundColor)
        {
            System.Console.BackgroundColor = backgroundColor;
            System.Console.ForegroundColor = foregroundColor;
        }


        public static void SetDefaultCursorPosition()
        {
            //if(Hero != null)
            //{
            //    SetCursorPositionInBorder(Hero.Loc);
            //    //Console.SetCursorPosition(Hero.Loc.AbscissaValue.Value, Hero.Loc.OrdinateValue.Value);
            //    return;
            //}
            // temp
            Location loc = new Location(new Ordinate(0), new Abscissa(0));
            Console.SetCursorPosition(loc);
            //Console.SetCursorPosition(Loc.AbscissaValue.Value, Loc.OrdinateValue.Value);
        }

        public static void SetDefaultState()
        {
            SetDefaultCursorPosition();
            Console.SetDefaultConsoleColor();
        }

        public static void Write(char symbol)
        {
            System.Console.Write(symbol);
        }
        public static void Write(Utility.Location loc, char symbol)
        {
            Console.SetCursorPosition(loc);
            System.Console.Write(symbol);
        }

        public static void Write(ConsolePoint point)
        {
            Output.Console.SetConsoleColor(point.BackgroundColor, point.ForegroundColor);
            Output.Console.Write(point.Symbol);
        }

        public static void SetCursorPosition(Location loc)
        {
            System.Console.SetCursorPosition(loc.AbscissaValue.Value, loc.OrdinateValue.Value);
        }
    }
}
