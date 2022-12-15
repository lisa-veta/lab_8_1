using System;
using System.IO;
using System.Text;
using System.Timers;
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
        static Timer timer;
        static int Timer = 0;
        static List<Subtitles> subtitles = new List<Subtitles>();
        static int width = 95;
        static int height = 25;

        static void Main(string[] args)
        {
            ReadFile();
            DrawScreen();
            StartTimer();
            Console.ReadKey();
        }

        static void ReadFile()
        {
            foreach (string str in File.ReadAllLines("D:\\УНИВЕР\\прога\\лб8\\LB8.txt"))
            {
                Subtitles subtitle = new Subtitles();

                string[] startTime = str.Substring(0, 5).Split(':');
                string[] finishTime = str.Substring(8, 5).Split(':');

                subtitle.StartTime = 60 * int.Parse(startTime[0]) + int.Parse(startTime[1]);
                subtitle.FinishTime = 60 * int.Parse(finishTime[0]) + int.Parse(finishTime[1]);

                if (str.Contains('['))
                {
                    subtitle.Color = str.Substring(str.IndexOf(',') + 2, str.IndexOf(']') - str.IndexOf(',') - 2);
                    subtitle.Position = str.Substring(str.IndexOf('[') + 1, str.IndexOf(',') - str.IndexOf('[') - 1);
                    subtitle.Text = str.Substring((str.IndexOf("]") + 2));
                }
                else
                {
                    subtitle.Color = "White";
                    subtitle.Position = "Bottom";
                    subtitle.Text = str.Substring(14);
                }
                subtitles.Add(subtitle);
            }
        }

        static void DrawScreen()
        {
            StringBuilder sb = new StringBuilder();
           
            sb.Append(new string('-', width));
            sb.Append("\n");

            for(int i = 0; i < height; i++)
            {
                for (int j = 0; j < width + 1; j++)
                {
                    if (j == 0 || j == width)
                        sb.Append("|");
                    else
                        sb.Append(" ");
                }
                sb.Append("\n");
            }

            sb.Append(new string('-', width));

            Console.WriteLine(sb.ToString());
        }

        static void StartTimer()
        {
            timer = new Timer(1000);
            timer.Elapsed += CheckTime;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        static void CheckTime(Object source, ElapsedEventArgs e)
        {
            foreach (Subtitles subtitle in subtitles)
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

        static void PrintSubtitle(Subtitles subtitle)
        {
            СhangePosition(subtitle);
            СhangeColor(subtitle);
            Console.WriteLine(subtitle.Text);
        }

        static void СhangeColor(Subtitles subtitle)
        {
            switch (subtitle.Color)
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
                default:
                    break;
            }
        }

        static void СhangePosition(Subtitles subtitle)
        {
            switch (subtitle.Position)
            {
                case "Top":
                    Console.SetCursorPosition((width - subtitle.Text.Length) / 2, 1);
                    break;
                case "Bottom":
                    Console.SetCursorPosition((width - subtitle.Text.Length) / 2, height);
                    break;
                case "Right":
                    Console.SetCursorPosition(width - subtitle.Text.Length - 1, height / 2);
                    break;
                case "Left":
                    Console.SetCursorPosition(2, height / 2);
                    break;
                default:
                    break;
            }
        }

        static void DeleteSubtitle(Subtitles subtitle)
        {
            СhangePosition(subtitle);
            for (int i = 0; i < subtitle.Text.Length; i++)
            {
                Console.Write(" ");
            }
        }

    }
    
}