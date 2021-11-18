using System;
using System.IO;
using System.Xml;
using System.IO.Compression;

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
            textWriter = File.AppendText(textFile);
            try {
                textWriter.WriteLine("Hello, C#!");
            } finally {
                textWriter.Close();
            }
            File.Copy(textFile, backupFile, true);

            StreamReader textReader;
            textReader = File.OpenText(backupFile);
            WriteLine($"Reading contents of {backupFile}: ");
            WriteLine(textReader.ReadToEnd());
            textReader.Close();

            WriteLine($"Folder Name: {GetDirectoryName(textFile)}");
            WriteLine($"File Name: {GetFileName(textFile)}");
            WriteLine($"File Name Without Extension: {GetFileNameWithoutExtension(textFile)}");
            WriteLine($"File Extension: {GetExtension(textFile)}");
            WriteLine($"Random File Name: {GetRandomFileName()}");
            //WriteLine($"Temporary File Name: {GetTempFileName()}");

            var fileInfo = new FileInfo(backupFile);
            WriteLine($"{backupFile}: ");
            WriteLine($"Contains {fileInfo.Length} bytes");
            WriteLine($"Last accessed {fileInfo.LastAccessTime}");
            WriteLine($"Has readonly set to {fileInfo.IsReadOnly}");
        }

        static string[] callsigns = new string[] {
            "Husker", "Starbuck", "Apollo", "Boomer",
            "Bulldog", "Athena", "Helo", "Racetrack"
        };

        static void WorkWithText() {
            string textFile = Path.Combine(CurrentDirectory, "Output", "streams.txt");
            StreamWriter text = File.CreateText(textFile);
            try {
                foreach(string item in callsigns) {
                    text.WriteLine(item);
                }
            } finally {
                text.Close();
            }
            WriteLine("{0} contains {1:N0} bytes", textFile, new FileInfo(textFile).Length);
            WriteLine(File.ReadAllText(textFile));
        }
        static void WorkWithXML() {
            string xmlFile = Combine(CurrentDirectory, "output", "streams.xml");
            FileStream xmlFileStream = null;
            XmlWriter xml = null;
            try {
                xmlFileStream = File.Create(xmlFile);
                xml = XmlWriter.Create(xmlFileStream, new XmlWriterSettings { Indent = true });
                xml.WriteStartDocument();
                xml.WriteStartElement("callsigns");
                foreach(string item in callsigns) {
                    xml.WriteElementString("callsign", item);
                }
                xml.WriteEndElement();
            } catch(Exception ex) {
                WriteLine($"{ex.GetType()} says {ex.Message}");
            } finally {
                xml.Close();
                xmlFileStream.Close();
                if(xml!=null) {
                    xml.Dispose();
                    WriteLine("The XML write's unmanaged resources have been disposed.");
                }
                if(xmlFileStream!=null) {
                    xmlFileStream.Dispose();
                    WriteLine("The file stream's unmanaged resources have been disposed.");
                }
            }
            WriteLine("{0} contains {1:N0} bytes", xmlFile, new FileInfo(xmlFile).Length);
            WriteLine(File.ReadAllText(xmlFile));
        }
        static void WorkWithCompression(bool useBrotli = true) {
            // file extension.
            string fileExt = useBrotli? "brotli" : "gzip";
            string filePath = Combine(Environment.CurrentDirectory, $"streams.{fileExt}");

            FileStream file = File.Create(filePath);
            Stream compressor;
            if (useBrotli) {
                compressor = new BrotliStream(file, CompressionMode.Compress);
            } else {
                compressor = new GZipStream(file, CompressionMode.Compress);
            }

            using(compressor) {
                using(XmlWriter xml = XmlWriter.Create(compressor)) {
                    xml.WriteStartDocument();
                    xml.WriteStartElement("callsigns");
                    foreach(string item in callsigns) {
                        xml.WriteElementString("callsign", item);
                    }
                }
                // the normal call to WirteEndElement is not necessary
                // because when the XmlWriter disposes, it will
                // automatically end any elements of any depth.
            } // also closes the underlying stream

            // output all the contents of the compressed file
            WriteLine("{0} contains {1:N0} bytes.", filePath, new FileInfo(filePath).Length);
            WriteLine($"The compressed contents:");
            WriteLine(File.ReadAllText(filePath));

            WriteLine("Reading the compressed XML file: ");
            file = File.Open(filePath, FileMode.Open);
            Stream decompressor;
            if(useBrotli) {
                decompressor = new BrotliStream(file, CompressionMode.Decompress);
            } else {
                decompressor = new GZipStream(file, CompressionMode.Decompress);
            }
            using(decompressor) {
                using(XmlReader reader = XmlReader.Create(decompressor)) {
                    while(reader.Read()) {
                        //check if we are on an element node named callsign
                        if((reader.NodeType == XmlNodeType.Element) && (reader.Name == "callsign")) {
                            reader.Read(); // Move to tehe text inside element
                            WriteLine($"{reader.Value}"); // read its value
                        }
                    }
                }
            }
        }
        static void Main(string[] args)
        {
            OutputFileSystemInfo();

            WorkWithDrives();

            WorkWithDirectories();

            WorkwithFiles();

            WorkWithText();

            WorkWithXML();

            WorkWithCompression();
        }
    }
}
