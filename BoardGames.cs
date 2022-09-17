using System;
using System.IO;
using System.Collections.Generic;

/// <summary>
/// @date. 28/04/2017
/// @author. Juan Antonio Ripoll Armengol
/// 
/// Board Games
/// Tuenti Challenge 3.
/// </summary>
/// 
namespace TuentiChallenge7
{
    public class BoardGames
    {
        static string inputFile = "testInput.txt";
        static string[] inputLines = null;

        static string outputFile = "testOutput.txt";
        static string[] outputLines = null;

        static int totalCases;

        static List<int> cards = null;

        static void Main()
        {
            int maxScore;


            try
            {
                inputLines = GetLinesFromFile();
                totalCases = Int32.Parse(inputLines[0]);
                outputLines = new string[totalCases];

                for (int i = 1; i < inputLines.Length; i += 1)
                {
                    maxScore = int.Parse(inputLines[i]);

                    int diferencia = 1;
                    int from = 1;
                    int to = from + 1;
                    int total = 0;
                    int contador = 1;

                    bool found = false;

                    do
                    {

                        while (total < maxScore)
                        {
                            total = ProgesionAritmetica(from, to);
                            Console.WriteLine("Search sum for " + maxScore + " from " + from + " to " + to + " with " + diferencia + " diference.");
                            Console.WriteLine("Total is " + total + ".");
                            //Console.ReadLine();

                            to++;
                        }

                        if (total > maxScore)
                        {
                            total = 0;
                            contador++;

                            //from++;
                            to = from + contador;
                        }

                        if (total == maxScore)
                        {
                            Console.WriteLine("Found from " + from + " to " + to);
                            Console.ReadLine();
                            found = true;
                        }
                    }
                    while (!found);


                    // Calculate(maxScore, 1);
                    // outputLines[i - 1] = "Case #" + i + ": " + (hasta - desde);

                }

                Console.ReadLine();

                SaveLinesOnFile(outputLines);
            }
            catch
            {

            }
        }

        static List<int> GetBetterSerie(int maxScore)
        {
            cards = new List<int>();
            int numberSerie = 1;
            int sumScore = 0;

            try
            {

                do
                {
                    if (sumScore > maxScore)
                    {
                        do
                        {
                            if (sumScore > maxScore)
                            {
                                numberSerie = cards[0];
                                cards.RemoveAt(0);

                                sumScore -= numberSerie;
                            }
                        }
                        while (sumScore > maxScore);

                        numberSerie = cards[cards.Count - 1] + 1;
                    }

                    if (sumScore < maxScore)
                    {
                        sumScore += numberSerie;
                        cards.Add(numberSerie);
                        numberSerie++;
                    }

                } while (sumScore != maxScore);
            }
            catch
            {
                cards = null;
            }

            return cards;
        }

        static int sumatotal = 0;
        static int better = 9999999;


        /// <summary>
        /// Sn = (A1 + An) x n 
        ///      _____________
        ///           2
        /// 
        /// </summary>
        /// <param name="a1"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        static int ProgesionAritmetica(int from, int to)
        {
            //   int d = (a1 + 1) - a1;

            return (((from + to) * (to - from)) / 2);
        }

        static string ultimos = "";
        static int Calculate(int maxSum, int indice)
        {
            if (indice == (maxSum / 2))
            {
                return better;
            }
            else
            {
                int total = 0;
                int i = indice;
                sumatotal = 0;
                ultimos = "";
                do
                {

                    if (sumatotal < maxSum)
                    {
                        total++;
                        ultimos += i.ToString() + " ";
                    }

                    sumatotal += i;
                    i++;

                }
                while (sumatotal < maxSum);

                if (sumatotal == maxSum)
                {
                    if (total < better)
                    {
                        better = total;
                    }
                }

                return Calculate(maxSum, indice + 1);
            }
        }

        static bool ExistNumber(int number)
        {
            foreach (int num in cards)
            {
                if (number == num)
                {
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// Gets all lines of a text file.
        /// </summary>
        static string[] GetLinesFromFile()
        {
            string[] lines;

            try
            {
                lines = File.ReadAllLines(inputFile);
            }
            catch
            {
                lines = null;
            }

            return lines;
        }


        /// <summary>
        /// Gets all lines of a text file.
        /// </summary>
        static void SaveLinesOnFile(string[] lines)
        {
            try
            {
                File.WriteAllLines(outputFile, lines);
            }
            catch { }
        }
    }
}