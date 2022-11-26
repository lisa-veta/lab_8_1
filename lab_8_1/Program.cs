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
    class Program
    {
        static Timer aTimer;
        static int Timer = 0;
        static List<string> subtitles = new List<string>();
        static int width = 100;
        static int height = 30;
        static void Main(string[] args)
        {
            foreach (string str in File.ReadAllLines("D:\\УНИВЕР\\прога\\лб8\\LB8.txt"))
            {
                subtitles.Add(str);
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
                string[] startTime = subtitle.Substring(0, 5).Split(':');
                string[] finishTime = subtitle.Substring(8, 5).Split(':');

                int start = 60 * int.Parse(startTime[0]) + int.Parse(startTime[1]);
                int finish = 60 * int.Parse(finishTime[0]) + int.Parse(finishTime[1]);

                if (start == Timer)
                {
                    PrintSubtitle(subtitle);
                }
                if (finish == Timer)
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
            if(str.Contains('['))
                Console.WriteLine(str.Substring((str.IndexOf("]") + 1)));
            else
                Console.WriteLine(str.Substring(14));
        }

        static void СhangeColor(string str)
        {
            //if(str.Contains('['))
            //{
                string color = str.Substring(str.IndexOf(',') + 2, str.IndexOf(']') - str.IndexOf(',')-2);
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
                    default:
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                }
            //}
            //else
            //{
            //    Console.ForegroundColor = ConsoleColor.White;
            //}
        }

        static void СhangePosition(string str)
        {
            //if (str.Contains('['))
            //{
                string position = str.Substring(str.IndexOf('[') + 1, str.IndexOf(',') - str.IndexOf('[') - 1);
                string text = str.Substring((str.IndexOf("]") + 2));
                switch (position)
                {
                    case "Top":
                        Console.SetCursorPosition((width - text.Length) / 2, 1);
                        break;
                    case "Bottom":
                        Console.SetCursorPosition((width - text.Length) / 2, height - 1);
                        break;
                    case "Right":
                        Console.SetCursorPosition(width - text.Length - 1, height / 2);
                        break;
                    case "Left":
                        Console.SetCursorPosition(2, height / 2);
                        break;
                    default:
                        text = str.Substring(14);
                        Console.SetCursorPosition((width - text.Length) / 2, height - 1);
                        break;
                }
            //}
            //else
            //{
            //    string text = str.Substring(14);
            //    Console.SetCursorPosition((width - text.Length) / 2, height - 1);
            //}
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