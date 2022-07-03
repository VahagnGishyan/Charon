using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    public static class Path
    {
        public static string StrToPath(string somestring)
        {
            var length = somestring.Length;
            string path = "";
            for (int index = 0; index < length; ++index)
            {
                char symbol = somestring[index];
                if (symbol == '\\' && index != length - 1)
                {
                    char ctrl = somestring[index + 1];
                    if (ctrl != '\\' && ctrl != 'n' && ctrl != 't') // temp
                    {
                        path += symbol;
                    }
                }
                path += symbol;
            }
            return (path);
        }
    }
}
