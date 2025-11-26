using System;
using System.Collections.Generic;

namespace MainProgram
{
    public class Program
    {
        static void Main()
        {
            double[] inputArray = { 1.5, -1.0, 3.0, -0.5, -0.2 };
            double[] outputArray;
            string[] errors;

            (outputArray, errors) = ProcessArray(inputArray);
            if (errors.Length != 0)
            {
                Console.WriteLine("\nОшибки:");
                foreach (var error in errors)
                {
                    Console.WriteLine(error);
                }
                return;
            }
        }

        static public (double[] outputArray, string[] errors) ProcessArray(double[] inputArray)
        {
            var outputArray = new double[inputArray.Length];

            if (inputArray.Any(e => e < -1))
            {
                for (int i = 0; i < inputArray.Length; i++)
                {
                    if (inputArray[i] < 0)
                    {
                        outputArray[i] = Math.Pow(inputArray[i], 3);
                        if (double.IsInfinity(outputArray[i]))
                        {
                            return (new double[0], new string[] { "Error: -Infinity" });
                        }
                    }
                    else
                    {
                        outputArray[i] = inputArray[i];
                    }
                }
            }
            else
            {
                Array.Copy(inputArray, outputArray, inputArray.Length);
            }

            return (outputArray, new string[0]);
        }

    }
}