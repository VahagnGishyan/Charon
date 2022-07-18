
using System;
using System.Threading;
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
        private static void ThreadUnsafeSetWindowSize(Utility.Size size)
        {
            System.Console.SetWindowSize(size.WeightValue.Value, size.HeightValue.Value);
        }

        private static void ThreadUnsafeSetDefaultConsoleColor()
        {
            System.Console.ForegroundColor = ConsoleColor.White;
            System.Console.BackgroundColor = ConsoleColor.Black;
        }

        private static void ThreadUnsafeSetConsoleColor(System.ConsoleColor backgroundColor,
                                                        System.ConsoleColor foregroundColor)
        {
            System.Console.BackgroundColor = backgroundColor;
            System.Console.ForegroundColor = foregroundColor;
        }

        private static void ThreadUnsafeSetDefaultCursorPosition()
        {
            Location loc = new Location(new Ordinate(0), new Abscissa(0));
            ThreadUnsafeSetCursorPosition(loc);
        }

        private static void ThreadUnsafeWrite(char symbol)
        {
            System.Console.Write(symbol);
        }

        private static void ThreadUnsafeWrite(ConsolePoint point)
        {
            ThreadUnsafeSetConsoleColor(point.BackgroundColor, point.ForegroundColor);
            ThreadUnsafeWrite(point.Symbol);
        }

        private static void ThreadUnsafeSetCursorPosition(Location loc) // Safe
        {
            System.Console.SetCursorPosition(loc.AbscissaValue.Value, loc.OrdinateValue.Value);
        }

        private static void ThreadUnsafeSetDefaultState() // UnSafe, Impl
        {
            ThreadUnsafeSetDefaultCursorPosition();
            ThreadUnsafeSetDefaultConsoleColor();
        }

        /////////////////////////////////////////////////////////////////////////////////////////
        
        public static void SetWindowSize(Utility.Size size)
        {
            while (!isConsoleFree) { Thread.Sleep(5); }
            isConsoleFree = false;

            System.Console.SetWindowSize(size.WeightValue.Value, size.HeightValue.Value);

            isConsoleFree = true;
        }

        public static void SetDefaultConsoleColor()
        {
            while (!isConsoleFree) { Thread.Sleep(5); }
            isConsoleFree = false;

            ThreadUnsafeSetDefaultConsoleColor();

            isConsoleFree = true;
        }

        public static void SetConsoleColor(System.ConsoleColor backgroundColor,
                                           System.ConsoleColor foregroundColor)
        {
            while (!isConsoleFree) { Thread.Sleep(5); }
            isConsoleFree = false;

            ThreadUnsafeSetConsoleColor(backgroundColor, foregroundColor);

            isConsoleFree = true;
        }

        public static void SetDefaultCursorPosition()
        {
            while (!isConsoleFree) { Thread.Sleep(5); }
            isConsoleFree = false;

            ThreadUnsafeSetDefaultCursorPosition();

            isConsoleFree = true;
        }

        public static void SetCursorPosition(Location loc) // Safe
        {
            while (!isConsoleFree) { Thread.Sleep(5); }
            isConsoleFree = false;

            ThreadUnsafeSetCursorPosition(loc);

            isConsoleFree = true;
        }

        public static void SetDefaultState() // Safe
        {
            while (!isConsoleFree) { Thread.Sleep(5); }
            isConsoleFree = false;

            ThreadUnsafeSetDefaultState();

            isConsoleFree = true;
        }

        public static void Write(Utility.Location loc, ConsolePoint point) // Safe
        {
            while (!isConsoleFree) { Thread.Sleep(5); }
            isConsoleFree = false;

            ThreadUnsafeSetConsoleColor(point.BackgroundColor, point.ForegroundColor);
            ThreadUnsafeSetCursorPosition(loc);
            ThreadUnsafeWrite(point);
            ThreadUnsafeSetDefaultState();

            isConsoleFree = true;
        }

        public static void Write(Utility.Location loc, char symbol) // Safe
        {
            Write(loc, new ConsolePoint(symbol));
        }

        //temp
        private static bool isConsoleFree = true;
}
}
