using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SimplexMethod
{
    static class SimplexSolver
    {
        public static double[] Solve(double[,] matrix)
        {            
            int insertInBasis;
            int outsideFromBasis;
            int numberOfIteration = 0;
            while (true)
            {
                insertInBasis = GetIndexOfBasisIn(matrix);
                if (insertInBasis == -1)
                {
                    break;
                }
                numberOfIteration++;
                outsideFromBasis = GetIndexOfBasisOut(matrix, insertInBasis);                
                ReBuild(ref matrix, insertInBasis, outsideFromBasis);
                Console.WriteLine("----------Итерация-номер-" + numberOfIteration.ToString() + "----------");
                PrintMaxtrix(matrix);
                Console.WriteLine();
            }



            double[] result = GetAnswer(matrix);
            PrintAnswer(result);
            return result;
        }

        private static void PrintAnswer(double[] result)
        {
            Console.WriteLine("Ответ:");
            for (int i = 0; i<result.Length; i++)
            {
                Console.WriteLine(String.Format("x[{0}] = {1}", i+1, result[i]));
            }
        }

        public static double[,] BuildMaxtrix(string fileName)
        {
            double[,] resultMatrix;
            string[] fileStrings = File.ReadAllLines(fileName);

            string[] tempArray = fileStrings[0].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            resultMatrix = new double[fileStrings.Length, tempArray.Length];

            for(int i = 0; i<fileStrings.Length; i++)
            {
                tempArray = fileStrings[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < tempArray.Length; j++)
                {
                    resultMatrix[i, j] = Convert.ToDouble(tempArray[j]);
                }
            }
            return resultMatrix;
        }

        public static void PrintMaxtrix(double[,] matrix)
        {
            for(int i = 0; i<matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j].ToString() + " ");
                }
                Console.Write("\n");
            }
        }

        private static int GetIndexOfBasisIn(double[,] matrix)
        {
            int result = -1;
            double lastMax = 0; 
            for(int i = 0; i<matrix.GetLength(1)-1; i++)
            {
                if (matrix[matrix.GetLength(0)-1, i]<lastMax)
                {
                    lastMax = matrix[matrix.GetLength(0) - 1, i];
                    result = i;
                }
            }
            return result;
        }

        private static int GetIndexOfBasisOut(double[,] matrix, int insertInBasis)
        {
            int result = -1;
            double lastMin = 100000000;
            for (int i = 0; i < matrix.GetLength(0) - 1; i++)
            {
                if ((matrix[i, matrix.GetLength(1) - 1] / matrix[i, insertInBasis] < lastMin)&&((matrix[i, matrix.GetLength(1) - 1] / matrix[i, insertInBasis])>0))
                {
                    lastMin = matrix[i, matrix.GetLength(1) - 1] / matrix[i, insertInBasis];
                    result = i;
                }
            }
            return result;
        }

        private static void Norm(ref double[,] matrix, int insertInBasis, int outsideFromBasis)
        {
            int i = outsideFromBasis;
            double a = matrix[outsideFromBasis, insertInBasis];
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                matrix[i, j] /= a;
            }
        }

        private static void ReBuild(ref double[,] matrix, int insertInBasis, int outsideFromBasis)              //outside - i //inside - j
        {
            Norm(ref matrix, insertInBasis, outsideFromBasis);
            double a;
            for(int i = 0; i<matrix.GetLength(0); i++)
            {
                if (i == outsideFromBasis)
                {
                    continue;
                }


                a = -matrix[i, insertInBasis];
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] += a * matrix[outsideFromBasis, j];
                }
            }
        }

        private static double[] GetAnswer(double[,] matrix)
        {
            double[] result = new double[matrix.GetLength(1) - matrix.GetLength(0)];
            int countOfZero;
            int countOfOne;
            int indexOfOne = -1;
            for (int j = 0; j<result.Length; j++)
            {
                countOfOne = 0;
                countOfZero = 0;
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    if (matrix[i, j] == 0) countOfZero++;
                    if (matrix[i, j] == 1)
                    {
                        countOfOne++;
                        indexOfOne = i;
                    }
                }
                if ((countOfOne == 1)&&(countOfZero == (matrix.GetLength(0)-1)))
                {
                    result[j] = matrix[indexOfOne, matrix.GetLength(1) - 1];
                }
                else
                {
                    result[j] = 0;
                }
                    
            }
            return result;
        }
    }
}
