
using System;

namespace UserInput
{
    public static class Keyboard
    {
        static public bool IsPressed(ConsoleKeyInfo input, ConsoleKey key)
        {
            return (input.Key == key);
        }
        static public bool IsPressedEscape(ConsoleKeyInfo input)
        {
            return (input.Key == ConsoleKey.Escape);
        }
        static public bool IsPressedEnter(ConsoleKeyInfo input)
        {
            return (input.Key == ConsoleKey.Enter);
        }

        static public bool IsPressedAlt(ConsoleKeyInfo input)
        {
            //return (input.Modifiers == ConsoleModifiers.Alt);
            return ((input.Modifiers & ConsoleModifiers.Alt) == ConsoleModifiers.Alt);
        }
        static public bool IsPressedControl(ConsoleKeyInfo input)
        {
            //return (input.Modifiers == ConsoleModifiers.Control);
            return ((input.Modifiers & ConsoleModifiers.Control) == ConsoleModifiers.Control);
        }
        static public bool IsPressedShift(ConsoleKeyInfo input)
        {
            //return (input.Modifiers == ConsoleModifiers.Shift);
            return ((input.Modifiers & ConsoleModifiers.Shift) == ConsoleModifiers.Shift);
        }
        static public string CheckInput(ConsoleKeyInfo input)
        {
            string message = "You input " + input.KeyChar;

            bool alt = IsPressedAlt(input);
            bool control = IsPressedControl(input);
            bool shift = IsPressedShift(input);

            if (alt || control || shift)
            {
                message += " with";
                message += alt ? " alt" : "";

                message += control ? alt ? ", control" : " control" : "";
                message += shift ? alt || control ? ", shift" : " shift" : "";
            }
            return (message);
        }

        public static ConsoleKeyInfo Input()
        {
            return (Console.ReadKey(true));
        }
    }
}
