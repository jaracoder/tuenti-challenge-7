using System;
using System.IO;

/// <summary>
/// @date. 27/04/2017
/// @author. Juan Antonio Ripoll Armengol
/// 
/// Bowling
/// Tuenti Challenge 2.
/// </summary>
/// 
namespace TuentiChallenge7
{
    public class Bowling
    {
        static string inputFile = "submitInput.txt";
        static string[] inputLines = null;

        static string outputFile = "submitOutput.txt";
        static string[] outputLines = null;
        static string line = "";

        static int totalCases;

        static int score = 0;
        static bool isEndRoll = false;
        static int rolls = 0;


        static void Main()
        {
            string[] input;
            string scoreFormat;
            int count = 1;

            try
            {
                inputLines = GetLinesFromFile();
                totalCases = Int32.Parse(inputLines[0]);
                outputLines = new string[totalCases];


                for (int i = 1; i < inputLines.Length; i += 2)
                {
                    input = inputLines[i + 1].Split(' ');
                    scoreFormat = ParserLegalScore(input);

                    line = "Case #" + count + ": ";
                    CalculateScore(scoreFormat, 0, 10);
                    outputLines[count - 1] = line;

                    score = 0;
                    rolls = 0;
                    count++;
                }

                SaveLinesOnFile(outputLines);
            }
            catch
            {

            }
        }


        /// <summary>
        /// Parser a score to legal score. 
        /// X for strike.
        /// / for spare.
        /// </summary>
        static string ParserLegalScore(string[] score)
        {
            string scoreFormat = "";

            int roll = 0;
            int scoreTemp = 0;

            try
            {
                foreach (string number in score)
                {
                    roll++;

                    if (roll > 1)
                    {
                        if (number == "10")
                        {
                            scoreFormat += "/";
                        }
                        else
                        {
                            scoreTemp += int.Parse(number);

                            if (scoreTemp == 10)
                            {
                                scoreFormat += "/";
                            }
                            else
                            {
                                scoreFormat += number;
                            }
                        }


                        roll = 0;
                        scoreTemp = 0;
                    }
                    else
                    {
                        if (number == "10")
                        {
                            scoreFormat += "X";
                            roll = 0;
                        }
                        else
                        {
                            scoreFormat += number;
                            scoreTemp += int.Parse(number);
                        }
                    }
                }
            }
            catch
            {
                scoreFormat = "-1";
            }

            return scoreFormat;
        }


        /// <summary>
        /// Calculate score and save in memory.
        /// </summary>
        static int CalculateScore(string scores, int from, int to)
        {
            if (scores.Length == from || rolls >= to)
            {
                line = line.Trim();

                return 0;
            }

            switch (scores[from])
            {
                case 'X':
                    score += 10 + GetExtraScoreStrike(scores, from);
                    line += score.ToString() + " ";

                    rolls++;
                    isEndRoll = false;
                    break;
                case '/':
                    score += 10 - int.Parse(scores[from - 1].ToString()) +
                            GetExtraScoreSpare(scores, from);
                    line += score.ToString() + " ";

                    rolls++;
                    isEndRoll = false;
                    break;
                default:
                    score += int.Parse(scores[from].ToString());

                    if (isEndRoll)
                    {
                        line += score.ToString() + " ";

                        rolls++;
                        isEndRoll = false;
                    }
                    else
                    {
                        isEndRoll = true;
                    }
                    break;
            }

            return CalculateScore(scores, from + 1, to);
        }


        /// <summary>
        /// Get sum the extra score for a strike. 
        /// </summary>
        static int GetExtraScoreStrike(string score, int from)
        {
            // limits
            if (score.Length == (from + 1))
            {
                return 0;
            }

            // calculate first score
            char first = score[from + 1];
            int total = 0;

            if (first == 'X')
            {
                total += 10;
            }
            else
            {
                total += int.Parse(first.ToString());
            }


            if (score.Length == (from + 2))
            {
                return total;
            }

            // calculate second score
            char second = score[from + 2];
            if (second == 'X')
            {
                total += 10;
            }
            else if (second == '/')
            {
                total += (10 - total);
            }
            else
            {
                total += int.Parse(second.ToString());
            }

            return total;
        }


        /// <summary>
        /// Get sum the extra score for a spare. 
        /// </summary>
        static int GetExtraScoreSpare(string score, int from)
        {
            if (score.Length == (from + 1))
            {
                return 0;
            }

            if (score[from + 1] == 'X')
            {
                return 10;
            }

            return int.Parse(score[from + 1].ToString());
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