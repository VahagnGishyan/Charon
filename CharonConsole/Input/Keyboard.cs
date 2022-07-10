
using System;

using Loging;

namespace Input
{
    public static class Keyboard
    {
        public static bool IsPressed(ConsoleKey key)
        {
            return (LastKey.Key == key);
        }
        public static bool IsPressedEscape()
        {
            return (LastKey.Key == ConsoleKey.Escape);
        }
        public static bool IsPressedEnter()
        {
            return (LastKey.Key == ConsoleKey.Enter);
        }

        public static bool IsPressedAlt()
        {
            return ((LastKey.Modifiers & ConsoleModifiers.Alt) == ConsoleModifiers.Alt);
        }
        public static bool IsPressedControl()
        {
            return ((LastKey.Modifiers & ConsoleModifiers.Control) == ConsoleModifiers.Control);
        }
        public static bool IsPressedShift()
        {
            return ((LastKey.Modifiers & ConsoleModifiers.Shift) == ConsoleModifiers.Shift);
        }

        public static bool IsPressedModifier(ConsoleModifiers modifier)
        {
            switch (modifier)
            {
                case ConsoleModifiers.Alt: return IsPressedAlt();
                case ConsoleModifiers.Control: return IsPressedControl();
                case ConsoleModifiers.Shift: return IsPressedShift();
            }
            return false;
        }

        public static bool IsPressedModifiers()
        {
            return (IsPressedAlt() || IsPressedControl() || IsPressedShift());
        }

        private static string InputAsString()
        {
            string symbol = "";
            switch (LastKey.Key)
            {
                //case ConsoleKey.A       : symbol = "Escape";   break;
                case ConsoleKey.Escape  : symbol = "Escape";   break;
                case ConsoleKey.Spacebar: symbol = "Spacebar"; break;
                default                 : symbol += LastKey.KeyChar; break;
            }

            string message = $"User input {symbol}";

            bool alt     = IsPressedAlt();
            bool control = IsPressedControl();
            bool shift   = IsPressedShift();

            if (alt || control || shift)
            {
                message += " with";
                message += alt ? " alt" : "";

                message += control ? alt ? ", control" : " control" : "";
                message += shift ? alt || control ? ", shift" : " shift" : "";
            }
            return (message);
        }

        public static void UserPass()
        {
            LastKey = Console.ReadKey(true);
            Loger.WriteLineMessage(InputAsString());
        }

        public static ConsoleKeyInfo LastPassedKey()
        {
            return (LastKey);
        }

        private static ConsoleKeyInfo LastKey { get; set; }
    }
}
