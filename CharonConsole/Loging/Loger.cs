
using System;
using System.Collections.Generic;
using System.IO;

namespace Loging
{
    
    public static class Loger
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        //
        /////////////////////////////////////////////////////////////////////////////////////////////

        //public class Path
        //{
        //    Path() { }
        //    Path(string path) 
        //    {
        //        string ext = System.IO.Path.GetExtension(path);
        //        StrPath = path;
        //    }

        //    public string StrPath { get; set; }
        //}

        //public class DirPath : Path
        //{

        //}

        //public class FilePath : Path
        //{

        //}

        //public class MessagePath : FilePath
        //{

        //}

        //public class WarningPath : FilePath
        //{

        //}

        //public class ErrorPath : FilePath
        //{

        //}

        /////////////////////////////////////////////////////////////////////////////////////////////
        
        public static string DefaultLogDirPath()
        {
            return ("D:\\Projects\\Charon\\CharonConsole\\Loging\\log\\");
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        //
        /////////////////////////////////////////////////////////////////////////////////////////////

        static Loger()
        {
            FMessageList = new List<StreamWriter>();
            FWarningList = new List<StreamWriter>();
            FErrorList   = new List<StreamWriter>();

            CreateLogFilesInDir(DefaultLogDirPath());
        }

        public static void AddLogDir(string dirPath)
        {
            FileAttributes attr = File.GetAttributes(dirPath);
            if ((attr & FileAttributes.Directory) != FileAttributes.Directory)
            {
                WriteError($"LogDirPath[{dirPath}] isn't a directory", WriteErrorMod.MakeException);
            }

            CreateLogFilesInDir(dirPath);
        }

        private static void CreateLogFilesInDir(string dirpath)
        {
            string fpmessage = dirpath + "message.txt";
            string fpwarning = dirpath + "warning.txt";
            string fperror = dirpath + "error.txt";

            FMessageList.Add(new StreamWriter(fpmessage));
            FWarningList.Add(new StreamWriter(fpwarning));
            FErrorList.Add(new StreamWriter(fperror));
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        //
        /////////////////////////////////////////////////////////////////////////////////////////////

        public static void AddFMessage(string filepathMessage)
        {
            FMessageList.Add(new StreamWriter(filepathMessage));
        }

        public static void AddFWarning(string filepathWarning)
        {
            FWarningList.Add(new StreamWriter(filepathWarning));
        }

        public static void AddFError(string filepathError)
        {
            FErrorList.Add(new StreamWriter(filepathError));
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        //
        /////////////////////////////////////////////////////////////////////////////////////////////

        public static void WriteMessage(string message)
        {
            foreach(var file in FMessageList)
            {
                file.Write(message);
            }
        }
        public static void WriteLineMessage(string message)
        {
            foreach (var file in FMessageList)
            {
                file.WriteLine(message);
            }
        }

        public static void WriteWarning(string warning)
        {
            foreach (var file in FWarningList)
            {
                file.Write(warning);
            }
        }
        public static void WriteLineWarning(string warning)
        {
            foreach (var file in FWarningList)
            {
                file.WriteLine(warning);
            }
        }

        public enum WriteErrorMod
        {
            Unset,
            MakeException
        }

        public static void WriteError(string error, WriteErrorMod mod = WriteErrorMod.Unset)
        {
            foreach (var file in FErrorList)
            {
                file.Write(error);
            }

            if(mod == WriteErrorMod.MakeException)
            {
                throw new Exception(error);
            }
        }

        public static void WriteLineError(string error, WriteErrorMod mod = WriteErrorMod.Unset)
        {
            foreach (var file in FErrorList)
            {
                file.WriteLine(error);
            }

            if (mod == WriteErrorMod.MakeException)
            {
                throw new Exception(error);
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        //
        /////////////////////////////////////////////////////////////////////////////////////////////

        public static string DefaultLogFMessage()
        {
            return (DefaultLogDirPath() + "message.txt");
        }

        public static string DefaultLogFWarning()
        {
            return (DefaultLogDirPath() + "warning.txt");
        }

        public static string DefaultLogFError()
        {
            return (DefaultLogDirPath() + "error.txt");
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        //
        /////////////////////////////////////////////////////////////////////////////////////////////

        private static void CloseFileList(List<StreamWriter> filelist)
        {
            foreach(var file in filelist)
            {
                file.Close();
            }
        }

        public static void Close()
        {
            CloseFileList(FMessageList);
            CloseFileList(FWarningList);
            CloseFileList(FErrorList);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        //
        /////////////////////////////////////////////////////////////////////////////////////////////

        private  static List<StreamWriter> FMessageList { get; set; }
        private  static List<StreamWriter> FWarningList { get; set; }
        private  static List<StreamWriter> FErrorList   { get; set; }
    }
}
