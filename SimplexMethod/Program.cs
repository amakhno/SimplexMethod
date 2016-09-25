using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SimplexMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            double[,] matrix;
            matrix = SimplexSolver.BuildMaxtrix("input.txt");
            Console.WriteLine("----------Исходная матрица----------");
            SimplexSolver.PrintMaxtrix(matrix);
            Console.WriteLine();
            SimplexSolver.Solve(matrix);
            Console.ReadKey();
        }
    }
}
