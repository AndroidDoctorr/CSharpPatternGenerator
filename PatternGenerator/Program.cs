using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PatternGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            // List<string> pattern = RandomFade(10, 10);
            // DisplayRows(pattern);
            // Console.WriteLine(RandomNumber(0, 100));
            // Console.WriteLine(RandomNumber(0, 100));
            // Console.WriteLine(RandomNumber(0, 100));
            BuildingPattern(15, 24);
            BuildingPattern(15, 24);
            BuildingPattern(15, 24);
            BuildingPattern(15, 24);
            // DisplayRows(RandomFade(96, 24));
            Console.Write("\n");
            // DisplayRows(RandomFade(96, 24));
            Console.ReadLine();


        }

        private static void BuildingPattern(int x, int y)
        {
            // Generate patterns
            List<string> NorthPattern = RandomFade(x, y);
            List<string> SouthPattern = RandomFade(x, y);
            List<string> EastPattern = RandomFade(x, y);
            List<string> WestPattern = RandomFade(x, y);

            // Display patterns, one row at a time

            // For each row
            for (int j = 0; j < y; j++)
            {
                // Display that row for each pattern
                DisplayRow(NorthPattern[j]);
                // Separate them by 3 spaces
                Console.Write("   ");
                DisplayRow(EastPattern[j]);
                Console.Write("   ");
                DisplayRow(SouthPattern[j]);
                Console.Write("   ");
                DisplayRow(WestPattern[j]);
                // Go to the beginning of the next line
                Console.Write('\n');
            }
            Console.Write("\n\n");
        }

        private static List<string> RandomFade(int x, int y)
        {
            // Random r = new Random();
            // Generate
            // Start with an empty list of strings
            List<string> rows = new List<string>();
            for (int j = 0; j < y; j++)
            {
                // Each row starts empty
                string row = "";
                // For each window in that row, pick a random number
                for (int i = 0; i < x; i++)
                {
                    // Pick a random number between 0 and the total height
                    // If the random number is less than the height of this row, mark it as X, otherwise make it a Y
                    // This way, X's should be more likely the higher the row is
                    // row += (r.Next(0, y) < j ? 'X' : 'Y');
                    row += (RandomNumber(0, y) < j ? 'X' : 'Y');
                }
                // Add this row to the list
                rows.Add(row);
                // Sleep so we can get a different random number
                // (Not needed after RandomNumber() method created)
                // Thread.Sleep(r.Next(0, 3));
            }

            return rows;
        }


        private static int RandomNumber(int min, int max)
        {
            using (var generator = RandomNumberGenerator.Create())
            {
                if (max < min)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error - RandomNumber - max must be bigger than min");
                    Console.ForegroundColor = ConsoleColor.White;
                    return -1;
                }
                var bytes = new byte[16];
                generator.GetBytes(bytes);

                int tempMax = max;
                int tempMin = min;
                foreach (byte b in bytes)
                {
                    if (64 < (tempMax - tempMin))
                    {
                        // Higher resolution is needed, select a new range
                        double range = (double)(tempMax - tempMin);
                        // Cut into 64ths
                        int slice = (int) Math.Ceiling(range / 64);
                        // Find a number 1 64th higher than the random selection
                        int newMax = (int)Math.Ceiling((double)b / (double)255 * range) + slice;
                        // Find a number 1 64th lower than the random selection
                        int newMin = (int)Math.Floor((double)b / (double)255 * range) - slice;
                        // Make sure we're still within range
                        tempMax = newMax > max ? max : newMax;
                        tempMin = newMin < min ? min : newMin;
                    } else
                    {
                        // Scale the byte to the range and select the number
                        return (int) Math.Round((double) b / (double) 255 * (double) (tempMax - tempMin)) + tempMin;
                    }
                }

                Console.WriteLine("Error - RandomNumber - range is too large?");
                return -1;
            }
        }

        private static void DisplayRows(List<string> rows)
        {
            // Display
            for (int j = 0; j < rows.Count; j++)
            {
                string row = rows[j];
                DisplayRow(row);
                Console.Write('\n');
            }
        }

        private static void DisplayRow(string row)
        {
            // Display
            foreach (char e in row)
            {
                // Set the color to Cyan if it's an X, otherwise make it Blue.
                if (e == 'X')
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
                // Draw a rectangle
                Console.Write('█');
            }
        }
    }
}
