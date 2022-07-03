
using System;

namespace Input
{
    public static class Keyboard
    {
        static public bool IsPressed(ConsoleKey key)
        {
            return (LastKey.Key == key);
        }
        static public bool IsPressedEscape()
        {
            return (LastKey.Key == ConsoleKey.Escape);
        }
        static public bool IsPressedEnter()
        {
            return (LastKey.Key == ConsoleKey.Enter);
        }

        static public bool IsPressedAlt()
        {
            return ((LastKey.Modifiers & ConsoleModifiers.Alt) == ConsoleModifiers.Alt);
        }
        static public bool IsPressedControl()
        {
            return ((LastKey.Modifiers & ConsoleModifiers.Control) == ConsoleModifiers.Control);
        }
        static public bool IsPressedShift()
        {
            return ((LastKey.Modifiers & ConsoleModifiers.Shift) == ConsoleModifiers.Shift);
        }

        public static ConsoleKeyInfo UserPass()
        {
            LastKey = Console.ReadKey(true);
            return (LastKey);
        }

        private static ConsoleKeyInfo LastKey { get; set; }
    }
}
