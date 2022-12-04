using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Timers;
using static System.Net.Mime.MediaTypeNames;
using Timer = System.Timers.Timer;

namespace lab_8
{
    class Subtitles
    {
        public int StartTime { get; set; }
        public int FinishTime { get; set; }
        public string Position { get; set; }
        public string Color { get; set; }
        public string Text { get; set; }
    }
    class Program
    {
        static Timer aTimer;
        static int Timer = 0;
        static List<Subtitles> subtitles = new List<Subtitles>();
        static int width = 100;
        static int height = 30;
        static void Main(string[] args)
        {
            foreach (string str in File.ReadAllLines("D:\\УНИВЕР\\прога\\лб8\\LB8.txt"))
            {
                Subtitles subtitle = new Subtitles();
                
                if (str.Contains('['))
                {
                    string[] startTime = str.Substring(0, 5).Split(':');
                    string[] finishTime = str.Substring(8, 5).Split(':');

                    subtitle.StartTime = 60 * int.Parse(startTime[0]) + int.Parse(startTime[1]);
                    subtitle.FinishTime = 60 * int.Parse(finishTime[0]) + int.Parse(finishTime[1]);

                    subtitle.Color = str.Substring(str.IndexOf(',') + 2, str.IndexOf(']') - str.IndexOf(',') - 2);
                    subtitle.Position = str.Substring(str.IndexOf('[') + 1, str.IndexOf(',') - str.IndexOf('[') - 1);
                    subtitle.Text = str.Substring((str.IndexOf("]") + 2));
                }
                else
                {
                    string[] startTime = str.Substring(0, 5).Split(':');
                    string[] finishTime = str.Substring(8, 5).Split(':');

                    subtitle.StartTime = 60 * int.Parse(startTime[0]) + int.Parse(startTime[1]);
                    subtitle.FinishTime = 60 * int.Parse(finishTime[0]) + int.Parse(finishTime[1]);
                    subtitle.Color = "White";
                    subtitle.Position = "Bottom";
                    subtitle.Text = str.Substring(14);
                }
                subtitles.Add(subtitle);
            }

            DrawScreen();
            StartTimer();

            Console.ReadKey();
        }
        static void StartTimer()
        {
            aTimer = new Timer(1000);
            aTimer.Elapsed += CheckTime;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        static void DrawScreen()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < height+1; i++)
            {
                for (int j = 0; j < width+1; j++)
                {
                    if (i == 0 || i == height)
                    {
                        sb.Append("-");
                    }
                    else if (j == 0 || j == width)
                    {
                        sb.Append("|");
                    }
                    else
                    {
                        sb.Append(" ");
                    } 
                }
                sb.Append("\n");
            }
            Console.WriteLine(sb.ToString());
        }

        static void CheckTime(Object source, ElapsedEventArgs e)
        {
            foreach (var subtitle in subtitles)
            {
                if (subtitle.StartTime == Timer)
                {
                    PrintSubtitle(subtitle);
                }
                if (subtitle.FinishTime == Timer)
                {
                    DeleteSubtitle(subtitle);
                }
            }
            Timer++;
        }

        static void PrintSubtitle(Subtitles str)
        {
            СhangePosition(str);
            СhangeColor(str);
            Console.WriteLine(str.Text);
        }

        static void СhangeColor(Subtitles str)
        {
            switch (str.Color)
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
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
        }

        static void СhangePosition(Subtitles str)
        {
            switch (str.Position)
            {
                case "Top":
                    Console.SetCursorPosition((width - str.Text.Length) / 2, 1);
                    break;
                case "Bottom":
                    Console.SetCursorPosition((width - str.Text.Length) / 2, height - 1);
                    break;
                case "Right":
                    Console.SetCursorPosition(width - str.Text.Length - 1, height / 2);
                    break;
                case "Left":
                    Console.SetCursorPosition(2, height / 2);
                    break;
            }
        }
        static void DeleteSubtitle(Subtitles str)
        {
            СhangePosition(str);
            for (int i = 0; i < str.Text.Length; i++)
            {
                Console.Write(" ");
            }
        }

    }
    
}