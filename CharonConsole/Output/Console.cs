
using System;
using System.Threading;
using Utility;

namespace Output
{
    public enum ConsoleSymbols
    {
        Border = '#',
        Space = ' ',
        Boom  = 'b',
        Fire  = 'f',
        Fence = 'F',
        Hero  = 'H',
        Zomby = 'Z'
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

        public ConsoleObject(Utility.Location loc, ConsolePoint point) 
            : this(loc, point.Symbol, point.ForegroundColor, point.BackgroundColor)
        {
        }

        public ConsoleObject(Utility.Location loc, char symbol)
            : this(loc, symbol, ConsoleColor.White, ConsoleColor.Black)
        {
        }

        public Location Loc { get; set; }
    }

    public static class Console
    {
        /////////////////////////////////////////////////////////////////////////////////////////

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
            SetCursorPosition(Location.MakeZero());
        }

        public static void SetCursorPosition(Location loc) // Safe
        {
            System.Console.SetCursorPosition(loc.AbscissaValue.Value, loc.OrdinateValue.Value);
        }

        public static void SetDefaultState() // Safe
        {
            SetDefaultCursorPosition();
            SetDefaultConsoleColor();
        }

        public static void Write(Utility.Location loc, ConsolePoint point) // Safe
        {
            SetConsoleColor(point.BackgroundColor, point.ForegroundColor);
            SetCursorPosition(loc);
            System.Console.Write(point.Symbol);
            SetDefaultState();
        }

        public static void Write(Utility.Location loc, char symbol) // Safe
        {
            Write(loc, new ConsolePoint(symbol));
        }
    }
}
