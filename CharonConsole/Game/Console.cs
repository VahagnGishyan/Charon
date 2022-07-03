
using System;

namespace Game
{
    enum ConsoleSymbols
    {
        Border = '#',
        Space = ' ',
        Fence = 'F'
    }
    public class ConsolePoint
    {
        public ConsolePoint(char         symbol,
                            ConsoleColor foregroundColor = ConsoleColor.White,
                            ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            Symbol = symbol;
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
        }

        public char Symbol { get; set; }
        public ConsoleColor ForegroundColor { get; set; }
        public ConsoleColor BackgroundColor { get; set; }
    }

    public class ConsoleObject : ConsolePoint
    {
        public ConsoleObject(Location loc, char symbol, ConsoleColor foregroundColor, ConsoleColor backgroundColor) : 
            base(symbol, foregroundColor, backgroundColor)
        {
            Loc = loc;
        }

        public Location Loc { get; set; }
    }

    public static class Console
    {
        public static void SetWindowSize(Game.Size size)
        {
            System.Console.SetWindowSize(size.HeightValue.Value, size.WeightValue.Value);
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
            Game.Console.SetCursorPosition(loc);
            //Console.SetCursorPosition(Loc.AbscissaValue.Value, Loc.OrdinateValue.Value);
        }

        public static void SetDefaultState()
        {
            SetDefaultCursorPosition();
            Game.Console.SetDefaultConsoleColor();
        }

        public static void Write(char symbol)
        {
            System.Console.Write(symbol);
        }

        public static void Write(ConsolePoint point)
        {
            Game.Console.SetConsoleColor(point.BackgroundColor, point.ForegroundColor);
            Game.Console.Write(point.Symbol);
        }

        public static void SetCursorPosition(Location loc)
        {
            System.Console.SetCursorPosition(loc.AbscissaValue.Value, loc.OrdinateValue.Value);
        }
    }
}
