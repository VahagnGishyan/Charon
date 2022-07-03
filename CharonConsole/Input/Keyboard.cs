
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

        private static string InputAsString(ConsoleKeyInfo input)
        {
            string message = "You input " + input.KeyChar;

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
            Loger.WriteMessage(InputAsString(LastKey));
        }

        private static ConsoleKeyInfo LastKey { get; set; }
    }
}
