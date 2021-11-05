using System;
using System.IO;

namespace fileIO
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"c:\Users\yongj\OneDrive\Documents\Common\repos\c#\learnCSharp\fileIO";
            Console.Write("Press R for readonly or W for write: ");
            ConsoleKeyInfo key = Console.ReadKey();
            Console.WriteLine();
            Stream s = null;
            if (key.Key == ConsoleKey.R) {
                s = File.Open(Path.Combine(path, "file.txt"),
                            FileMode.OpenOrCreate,
                            FileAccess.Read);
            } else {
                s = File.Open(Path.Combine(path, "file.txt"),
                            FileMode.OpenOrCreate,
                            FileAccess.Write);
            }
            string msg = string.Empty;
            switch(s) {
                case FileStream writeableFile when s.CanWrite:
                    msg = "The Stream is a file thate I can write to";
                    break;
                case FileStream readOnlyFile:
                    msg = "The stream is a read-only file.";
                    break;
                case MemoryStream ms:
                    msg = "The stream is a memory address";
                    break;
                default:
                    msg = "The stream is some other type.";
                    break;
            }
            Console.WriteLine(msg);
        }
    }
}
