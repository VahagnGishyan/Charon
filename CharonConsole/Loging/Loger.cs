
using System;
using System.IO;

namespace Loging
{
    
    public class Loger
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        //
        /////////////////////////////////////////////////////////////////////////////////////////////

        public Loger()
        {

        }

        public Loger(string filepathMessage, string filepathWarning, string filepathError)
        {
            FileMessage = new StreamWriter(filepathMessage);
            FileWarning = new StreamWriter(filepathWarning);
            FileError   = new StreamWriter(filepathError);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        //
        /////////////////////////////////////////////////////////////////////////////////////////////

        public void OpenFileMessage(string filepathMessage)
        {
            FileMessage = new StreamWriter(filepathMessage);
        }

        public void OpenFileWarning(string filepathWarning)
        {
            FileWarning = new StreamWriter(filepathWarning);
        }

        public void OpenFileError(string filepathError)
        {
            FileError   = new StreamWriter(filepathError);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        //
        /////////////////////////////////////////////////////////////////////////////////////////////

        public void WriteMessage(string message)
        {
            FileMessage.Write(message);
        }
        public void WriteLineMessage(string message)
        {
            FileMessage.WriteLine(message);
        }

        public void WriteWarning(string message)
        {
            FileWarning.Write(message);
        }
        public void WriteLineWarning(string message)
        {
            FileWarning.WriteLine(message);
        }

        public void WriteError(string message)
        {
            FileError.Write(message);
        }
        public void WriteLineError(string message)
        {
            FileError.WriteLine(message);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        //
        /////////////////////////////////////////////////////////////////////////////////////////////

        public static string DefaultLogFilesDir()
        {
            return ("D:\\Projects\\Charon\\CharonConsole\\Loging\\log\\");
        }

        public static string DefaultLogFileMessage()
        {
            return (DefaultLogFilesDir() + "message.txt");
        }

        public static string DefaultLogFileWarning()
        {
            return (DefaultLogFilesDir() + "warning.txt");
        }

        public static string DefaultLogFileError()
        {
            return (DefaultLogFilesDir() + "error.txt");
        }

        public static Loger MakeDefault()
        {
            Loging.Loger file = new Loger(); // (fpMessage, fpWarning, fpError);
            file.OpenFileMessage(DefaultLogFileMessage());
            file.OpenFileWarning(DefaultLogFileWarning());
            file.OpenFileError  (DefaultLogFileError  ());

            return (file);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        //
        /////////////////////////////////////////////////////////////////////////////////////////////

        public void Close()
        {
            FileMessage.Close();
            FileWarning.Close();
            FileError  .Close();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        //
        /////////////////////////////////////////////////////////////////////////////////////////////

        private StreamWriter FileMessage { get; set; }
        private StreamWriter FileWarning { get; set; }
        private StreamWriter FileError   { get; set; }
    }
}
