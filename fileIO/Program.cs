using System;
using System.IO;
using static System.Console;
using static System.IO.Directory;
using static System.IO.Path;
using static System.Environment;

namespace fileIO
{
    class Program
    {
        static void OutputFileSystemInfo()
        {
            WriteLine("{0, -33} {1}", "Path.PathSeparator", PathSeparator);
            WriteLine("{0, -33} {1}", "Path.DirectorySeparatorChar", DirectorySeparatorChar);
            WriteLine("{0, -33} {1}", "Directory.GetCurrentDirectory()", GetCurrentDirectory());
            WriteLine("{0, -33} {1}", "Environment.CurrectDirectory", CurrentDirectory);
            WriteLine("{0, -33} {1}", "Environment.SystemDirectory", SystemDirectory);
            WriteLine("{0, -33} {1}", "Path.GetTempPath()", GetTempPath());
            WriteLine("GetFolderPAth(SpecialFolder");
            WriteLine("{0, -33} {1}", " .System", GetFolderPath(SpecialFolder.System));
            WriteLine("{0, -33} {1}", " .ApplicationData", GetFolderPath(SpecialFolder.ApplicationData));
            WriteLine("{0, -33} {1}", " .MyDocuments", GetFolderPath(SpecialFolder.MyDocuments));
            WriteLine("{0, -33} {1}", " .Personal", GetFolderPath(SpecialFolder.Personal));

        }
        static void WorkWithDrives() {
            WriteLine("{0,-30} | {1,-10} | {2,-7} | {3,18} | {4,18}",
                        "NAME", "TYPE", "FORMAT", "SIZE(BYTES)", "FREE SPACE");
             foreach(DriveInfo drive in DriveInfo.GetDrives()) {
                if(drive.IsReady) {
                    WriteLine("{0, -30} | {1, -10} | {2, -7} | {3, 18:N0} | {4, 18:N0}",
                                drive.Name, drive.DriveType, drive.DriveFormat, drive.TotalSize, drive.AvailableFreeSpace);
                } else {
                    WriteLine("{0, -30} | {1,-10}", drive.Name, drive.DriveType);
                }
            }
        }
        static void WorkWithDirectories() {
            var newFolder = Combine(Environment.CurrentDirectory, "NewFolder");
            WriteLine($"Working with: {newFolder}");
            WriteLine($"Does it exist? {Exists(newFolder)}");
            // create new directory
            WriteLine($"Creating it...");
            Directory.CreateDirectory(newFolder);
            WriteLine($"Does it exist? {Exists(newFolder)}");
            Write("Confirm the directory exists, and then press ENTER: ");
            ReadLine();
            // delete directory
            WriteLine("Deleting it...");
            Delete(newFolder, true);
            WriteLine($"Does it exist? {Exists(newFolder)}");
        }
        static void WorkwithFiles() {
            var dir = Path.Combine(Environment.CurrentDirectory,"Output");
            if(!Directory.Exists(dir)) {
                Directory.CreateDirectory(dir);
            }
            string textFile = Path.Combine(dir,"Dummy.txt");
            string backupFile = Path.Combine(dir,"Dummy.bak");
            WriteLine($"Working with: {textFile}");
            StreamWriter textWriter;
            textWriter = File.CreateText(textFile);
            textWriter.WriteLine("Hello, C#!");
            textWriter.Close();

            File.Copy(textFile, backupFile, true);
        }
        static void Main(string[] args)
        {
            OutputFileSystemInfo();

            WorkWithDrives();

            WorkWithDirectories();

            WorkwithFiles();
        }
    }
}
