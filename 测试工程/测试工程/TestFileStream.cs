using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace 测试工程
{
    public class TestFileStream
    {



        public static void TestBinaryWriter(string binFilePath)
        {
            var stream = File.Create(binFilePath);
            using (var writer = new BinaryWriter(stream))
            {
                double d = 47.47;
                int i = 42;
                long l = 987654321;
                string s = "sample";
                writer.Write(d);
                writer.Write(i);
                writer.Write(s);
                writer.Flush();

            }
        }

        public static void TestStreamWrite()
        {
            string path = @"E:\c#Project\测试文本.txt";

            // Delete the file if it exists.
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            //Create the file.
            using (FileStream fs = File.Create(path))
            {
                byte[] preamble = Encoding.UTF8.GetPreamble();

                
                fs.Write(preamble,0,preamble.Length);
                //AddText(fs, "This is some text");
                //AddText(fs, "This is some more text,");
                //AddText(fs, "\r\nand this is on a new line");
                //AddText(fs, "\r\n\r\nThe following is a subset of characters:\r\n");

                //for (int i = 1; i < 120; i++)
                //{
                //    AddText(fs, Convert.ToChar(i).ToString());
                //}
            }
            //Open the stream and read it back.
            using (FileStream fs = File.OpenRead(path))
            {
                byte[] b = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding(true);
                while (fs.Read(b, 0, b.Length) > 0)
                {
                    Console.WriteLine(temp.GetString(b));
                }
            }
        }
        static void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }

        public static void ReadFileUsingFileStream(string fileName)
        {
            const int bufferSize = 4096;
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {

                //var reader = new StreamReader(stream);
                var writer = new StreamWriter(stream);
                //while (!reader.EndOfStream)
                //{
                //    string len = reader.ReadLine();
                //    Console.WriteLine(len);
                //}

                writer.WriteLine("xxxxxxxxxxxxxxxkkkkkkkkkkkkkkssssssssssss");
                writer.Flush();
                writer.Dispose();
                //ShowStreamInfomation(stream);
                //Encoding encoding = GetEncoding(stream);
                //Console.WriteLine(encoding);
            }

        }

        static void ShowStreamInfomation(Stream stream)
        {
            Console.WriteLine($"stream can read :{stream.CanRead}" +
                $"can write:{stream.CanWrite}" +
                $"can timeout {stream.CanTimeout}" +
                $"length :{stream.Length}" +
                $"position:{stream.Position}");
            if (stream.CanTimeout)
            {
                Console.WriteLine($"read timeout :{stream.ReadTimeout}" +
                    $"write timeout {stream.WriteTimeout}");
            }
        }

        static Encoding GetEncoding(Stream stream)
        {
            if (!stream.CanSeek)
            {
                throw new ArgumentException("require a stream that can seek");
            }

            Encoding encoding = Encoding.ASCII;

            byte[] bom = new byte[5];
            long lenght = stream.Length;
            int nRead = stream.Read(bom,offset:0,count:5);
            if (bom[0] == 0xff && bom[1] == 0xfe && bom[2] == 0 && bom[3] == 0)
            {
                Console.WriteLine("UTF-32");
                stream.Seek(4, SeekOrigin.Begin);
                return Encoding.UTF32;
            }
            else if (bom[0] == 0xff && bom[1] == 0xfe)
            {
                Console.WriteLine("UTF-16 little endian");
                stream.Seek(2, SeekOrigin.Begin);
                return Encoding.Unicode;
            }
            else if (bom[0] == 0xfe && bom[1] == 0xff)
            {
                Console.WriteLine("UTF-16 big endian");
            }
            else if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf)
            {
                Console.WriteLine("UTF-8");
                stream.Seek(3,SeekOrigin.Begin);
                return Encoding.UTF8;
            }
            stream.Seek(0,SeekOrigin.Begin);
            return encoding;

        }

    }
}
