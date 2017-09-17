using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SortYourselfOut
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Invalud arguments");
                return;
            }

            if (File.Exists(args[0]) != true)
            {
                Console.WriteLine("Inputted file does not exist");
                return;
            }

            List<int> numberList = new List<int>();
            
            using (FileStream fs = File.Open(args[0], FileMode.Open, FileAccess.Read))
            using (BufferedStream bs = new BufferedStream(fs))
            using (StreamReader sr = new StreamReader(bs))
            {
                char[] bytes = new char[4];
                
                while (sr.Read(bytes, 0, 4) != 0)
                {
                    int newnum = BitConverter.ToInt32(Encoding.Default.GetBytes(bytes), 0);
                    numberList.Add(newnum);
                }
                
                numberList.Sort();
                using (BinaryWriter writer = new BinaryWriter(File.Open("out.bin", FileMode.Create)))
                {
                    foreach (var number in numberList)
                    {
                        writer.Write(number);
                    }
                }
            }
        }
    }
}