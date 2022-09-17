using System;
using System.IO;

/// <summary>
/// @date. 27/04/2017
/// @author. Juan Antonio Ripoll Armengol
/// 
/// Pizza love
/// Challenge 1 - Qualification Round 2017 - Google Code Jam
/// </summary>
/// 

namespace TuentiChallenge7
{
    public class PizzaLove
    {
        const byte NUM_PIZZA_SLICES = 8;

        static string inputFile = "submitInput.txt";
        static string[] inputLines = null;

        static string outputFile = "solved.in";
        static string[] outputLines = null;

        static int totalCases;
        static int totalPizzaSlices = 0;
        static int totalPizzas = 1;


        static void Main()
        {
            try
            {
                inputLines = GetLinesFromFile();
                totalCases = Int32.Parse(inputLines[0]);
                outputLines = new string[totalCases];

                int count = 0;
                for (int i = 1; i < inputLines.Length; i += 2)
                {
                    // sum the total of pizza slices
                    string[] numSlices = inputLines[i + 1].ToString().Split(' ');
                    foreach (string numSlice in numSlices)
                    {
                        totalPizzaSlices += Int32.Parse(numSlice);
                    }

                    // calculate number minimiun of pizzas.
                    if (totalPizzaSlices > NUM_PIZZA_SLICES)
                    {
                        if (totalPizzaSlices % NUM_PIZZA_SLICES == 0)
                        {
                            totalPizzas = totalPizzaSlices / NUM_PIZZA_SLICES;
                        }
                        else
                        {
                            totalPizzas = totalPizzaSlices / NUM_PIZZA_SLICES;
                            totalPizzas++;
                        }
                    }

                    // save lines of result
                    outputLines[count] = "Case #" + (count + 1) + ": " + totalPizzas;
                    count++;

                    // reset values
                    totalPizzaSlices = 0;
                    totalPizzas = 1;
                }

                SaveLinesOnFile(outputLines);
            }
            catch
            {

            }
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