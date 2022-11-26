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
            foreach (var subtitle in subtitles)
            {
                string[] startTime = subtitle.Substring(0, 5).Split(':');
                string[] finishTime = subtitle.Substring(6, 5).Split(':');

                int start = 60 * int.Parse(startTime[0]) + int.Parse(startTime[1]);
                int finish = 60 * int.Parse(finishTime[0]) + int.Parse(finishTime[1]);

                if (start == Timer)
                {
                    PrintSubtitle(subtitle);
                }
                else if (finish == Timer)
                {
                    DeleteSubtitle(subtitle);
                }
            }
            Timer++;
        }

        static void PrintSubtitle(string str)
        {
            СhangePosition(str);
            СhangeColor(str);
            string text = str.Substring((str.IndexOf("]") + 1));
            Console.WriteLine(text);
        }

        static void СhangeColor(string str)
        {
            string color = str.Substring(str.IndexOf(',') + 1, str.IndexOf(']') - str.IndexOf(',') - 1);

            switch (color)
            {
                case "Red":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "Green":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case "Blue":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case "White":
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
        }

        static void СhangePosition(string str)
        {
            string position = str.Substring(str.IndexOf('[') + 1, str.IndexOf(',') - str.IndexOf('[') - 1);
            string text = str.Substring((str.IndexOf("]") + 1));
            switch (position)
            {
                case "Top":
                    Console.SetCursorPosition((100 - text.Length) / 2, 2);
                    break;
                case "Bottom":
                    Console.SetCursorPosition((100 - text.Length) / 2, 26 - 1);
                    break;
                case "Right":
                    Console.SetCursorPosition(100 - 2 - text.Length, 26 / 2);
                    break;
                case "Left":
                    Console.SetCursorPosition(2, 26 / 2);
                    break;
            }
        }
        static void DeleteSubtitle(string str)
        {
            СhangePosition(str);
            string text = str.Substring((str.IndexOf("]") + 1));
            for (int i = 0; i < text.Length; i++)
            {
                Console.Write(" ");
            }
        }

    }
    
}