using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TextAdventure
{
    class Dialogue
    {
        public static List<ConsoleColor> colors = new List<ConsoleColor>();
        public static string[] hexChars = "0 1 2 3 4 5 6 7 8 9 a b c d e f".Split(' ');

        public static void InitColors()
        {
            colors = new List<ConsoleColor>();
            foreach (ConsoleColor x in Enum.GetValues(typeof(ConsoleColor)))
            {
                colors.Add(x);
            }
        }

        public static void TimedDialogue(string[] words, int delay = 1000)
        {
            foreach (string x in words)
            {
                string finalString = x;
                if (x.Contains("$col$") || x.Contains("$ext$"))
                {
                    //Removes the commands from the string
                    if (x.Contains("$col$"))
                    {
                        finalString = finalString.Replace("$col$", "");
                    }

                    if (x.Contains("$ext$"))
                    {
                        finalString = finalString.Replace("$ext$", "");
                    }
                    //

                    //Actually applies the commands
                    if (x.Contains("$col$"))
                    {
                        char wantedColor = 'f';//Should be returned as hexadecimal value (0-f)
                        // At this point the color integer should be at the start of the string
                        wantedColor = finalString[0];
                        finalString = finalString.Remove(0, 1);
                        Console.ForegroundColor = colors[hexChars.ToList().IndexOf(wantedColor.ToString())];
                    }
                    Console.Write($"{finalString}{(x.Contains("$ext$") ? "" : "\n")}");
                    Console.ForegroundColor = ConsoleColor.White;
                    //
                }
                else
                {
                    Console.Write($"{x}\n");
                }
                Thread.Sleep(delay);
            }
        }

        public static void ColoredPrint(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
