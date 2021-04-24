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
        public static ConsoleColor[] rarityColors = { ConsoleColor.Gray, ConsoleColor.Green, ConsoleColor.Cyan, ConsoleColor.Magenta, ConsoleColor.Yellow };
        /*C,U,R,M,L*/

        public static void initializeColors()
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

        public static void ColoredPrint(string text, ConsoleColor color, bool withLine = true)
        {
            Console.ForegroundColor = color;
            if (withLine)
            {
                Console.WriteLine(text);
            }
            else
            {
                Console.Write(text);
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        public static string BoxText(string[][] stringsIn, string joiner = " | ",Orientation orientation = Orientation.Vertical)
        {
            string finalString = "";
            List<List<string>> stringsProcessed = new List<List<string>>();
            List<List<string>> final = new List<List<string>>();

            //Replaces each string in all of the given lists with Endstring() (the one with the spaces)
            foreach (string[] i in stringsIn)
            {
                List<string> _finalElement = new List<string>();
                foreach (string ii in EndString(i))
                {
                    _finalElement.Add(ii);
                }
                stringsProcessed.Add(_finalElement);
            }

            switch (orientation)
            {
                case Orientation.Vertical:
                    //Adds them all together accordingly
                    for (int i = 0; i < stringsProcessed[0].Count; i++)
                    {
                        List<string> _finalElement = new List<string>();
                        foreach (List<string> ii in stringsProcessed)
                        {
                            _finalElement.Add(ii[i]);
                        }
                        final.Add(_finalElement);
                    }

                    //Joining all the lists together
                    finalString = string.Join("\n", final.Select(n => string.Join(joiner, n)));
                    break;

                case Orientation.Horizontal:
                    //Adds them all together accordingly
                    for (int i = 0; i < stringsProcessed[0].Count; i++)
                    {
                        List<string> _finalElement = new List<string>();
                        foreach (List<string> ii in stringsProcessed)
                        {
                            _finalElement.Add(ii[i]);
                        }
                        final.Add(_finalElement);
                    }

                    //Joining all the lists together
                    finalString = string.Join("\n", final.Select(n => string.Join(joiner, n)));
                    break;
            }

            return finalString;

            List<string> EndString(string[] InStr)
            {
                List<string> _final = new List<string>();
                int totalLength = InStr.Max(n => n.Length);

                foreach (string _x in InStr)
                {
                    _final.Add($"{_x}{new string(' ', totalLength - _x.Length)}");
                }

                return _final;
            }
        }

        public static List<List<string>> BoxText(string[][] stringsIn, bool join = false, string joiner = "|", Orientation orientation = Orientation.Vertical)
        {
            string finalString = "";
            List<List<string>> stringsProcessed = new List<List<string>>();
            List<List<string>> final = new List<List<string>>();

            //Replaces each string in all of the given lists with Endstring() (the one with the spaces)
            foreach (string[] i in stringsIn)
            {
                List<string> _finalElement = new List<string>();
                foreach (string ii in EndString(i))
                {
                    _finalElement.Add(ii);
                }
                stringsProcessed.Add(_finalElement);
            }

            switch (orientation)
            {
                case Orientation.Vertical:
                    //Adds them all together accordingly
                    for (int i = 0; i < stringsProcessed[0].Count; i++)
                    {
                        List<string> _finalElement = new List<string>();
                        foreach (List<string> ii in stringsProcessed)
                        {
                            _finalElement.Add(ii[i]);
                        }
                        final.Add(_finalElement);
                    }

                    //Joining all the lists together
                    finalString = string.Join("\n", final.Select(n => string.Join(joiner, n)));
                    break;

                case Orientation.Horizontal:
                    //Adds them all together accordingly
                    for (int i = 0; i < stringsProcessed[0].Count; i++)
                    {
                        List<string> _finalElement = new List<string>();
                        foreach (List<string> ii in stringsProcessed)
                        {
                            _finalElement.Add(ii[i]);
                        }
                        final.Add(_finalElement);
                    }

                    //Joining all the lists together
                    finalString = string.Join("\n", final.Select(n => string.Join(joiner, n)));
                    break;
            }

            return final;

            List<string> EndString(string[] InStr)
            {
                List<string> _final = new List<string>();
                int totalLength = InStr.Max(n => n.Length);

                foreach (string _x in InStr)
                {
                    _final.Add($"{_x}{new string(' ', totalLength - _x.Length)}");
                }

                return _final;
            }
        }

        public enum Orientation
        {
            // Vertical = First element goes vertically
            /* A1 B1 C1
             * A2 B2 C2
             * A3 B3 C3 */

            // Horizontal = First element goes horizontally
            /* A1 A2 A3
             * B1 B2 B3
             * C1 C2 C3 */
            Vertical,
            Horizontal
        }
    }
}
