using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Timers;
using Timer = System.Timers.Timer;

namespace lab_8
{
    class Program
    {
        static Timer aTimer;
        static int Timer = 0;
        public static List<string> subtitles = new List<string>();
        static void Main(string[] args)
        {
            foreach (string str in File.ReadAllLines("D:\\УНИВЕР\\прога\\лб8\\LB8.txt"))
            {
                subtitles.Add(str.Replace(" ", ""));
            }
            DrawScreen();
            StartTimer();
            Console.ReadKey();
        }

        private static void StartTimer()
        {
            aTimer = new Timer(1000);
            aTimer.Elapsed += CheckTime;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        //public static void DataFromStr(string str)
        //{
        //    char[] separators = {'-', '[', ']',};
        //    str = str.Replace(" ", "");
        //    //string[] subtitle = str.Split(separators);
        //    subtitles.Add(str);
        //    Console.WriteLine(int.Parse(str.Split(':')[0]) + int.Parse(str.Split(':')[1]));
        //    //ChangeColor(subtitle);
        //    //ChangeCursor(subtitle);
        //    //CheckTime(subtitle);
        //}

        public static void DrawScreen()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    if (i == 0 || i == 14) sb.Append("-");
                    else if (j == 0 || j == 99) sb.Append("|");
                    else sb.Append(" ");
                }
                sb.AppendLine("\n"); 
            }
            Console.WriteLine(sb.ToString());
        }

        static void CheckTime(Object source, ElapsedEventArgs e)
        {
            foreach (string str in subtitles)
            {
                string[] startTime = str.Substring(0, 5).Split(':');
                string[] finishTime = str.Substring(5, 5).Split(':');

                int start = 60 * int.Parse(startTime[0]) + int.Parse(startTime[1]);
                int finish = 60 * int.Parse(finishTime[0]) + int.Parse(finishTime[1]);

                if (start == Timer)
                {
                    Console.WriteLine(str);
                }
                else if (finish == Timer)
                {
                    Console.WriteLine(str);
                }
            }
            Timer++;
        }

        static void PrintSubtitle()
        {

        }

    }
}