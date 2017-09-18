using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            //List<int> numberList = new List<int>();
            int[] numberList = new int[1000000];
            
            using (FileStream fs = File.Open(args[0], FileMode.Open, FileAccess.Read))
            using (BufferedStream bs = new BufferedStream(fs))
            {
                byte[] bytes = new byte[4];
                int i = 0;
                
                Stopwatch loadsw = Stopwatch.StartNew();
                while (bs.Read(bytes, 0, 4) != 0)
                {
                    int newnum = BitConverter.ToInt32(bytes, 0);
                    numberList[i] = newnum;
                    i++;
                }
                loadsw.Stop();
                
                Stopwatch sortsw = Stopwatch.StartNew();
                //numberList.Sort();
                Array.Sort(numberList);
                sortsw.Stop();
                
                Stopwatch writesw = Stopwatch.StartNew();
                using (FileStream fs2 = File.Open("out.bin", FileMode.Create, FileAccess.Write))
                using (BufferedStream bs2 = new BufferedStream(fs2))
                {
                    foreach (var number in numberList)
                    {
                        byte[] num = BitConverter.GetBytes(number);
                        bs2.Write(num, 0, num.Length);
                    }
                }
                writesw.Stop();
                
                Console.WriteLine(loadsw.ElapsedMilliseconds);
                Console.WriteLine(sortsw.ElapsedMilliseconds);
                Console.WriteLine(writesw.ElapsedMilliseconds);
                Console.WriteLine(numberList.Length);
            }
        }
    }
}