using System;
using ClassLibrary1;
namespace ChM_Laba3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Возняк Софiя IС-01, 7 варiант");
            double[,] matrixA = { { 3.81, 0.25, 1.28, 1.75 }, { 2.25, 1.32, 5.58, 0.49 },
                { 5.31, 7.28, 0.98, 1.04 }, { 10.39, 2.45, 3.35, 2.28 } };
            double[] arrayB = { 4.21, 7.47, 2.38, 11.48 };
            Console.WriteLine("`````````````````````початкова матриця`````````````````````");
            Class1.MatrixPrint(matrixA);
            Console.WriteLine("```````````````````пiсля 1 iтерацiї Гауса``````````````````");
            Class1.MatrixPrint(Class1.GaussaMethod(matrixA));
            Console.WriteLine("``````````````````замiна 2-го i 3-го рядка`````````````````");
            double[,] normMatrix = Class1.Zamena(Class1.GaussaMethod(matrixA));
            Class1.MatrixPrint(normMatrix); //матриця з діагональною перевагою
            Class1.NormForm(normMatrix, arrayB); //ітераційні формули
            Class1.IteraciYYYYY(normMatrix, matrixA, arrayB);
        }
    }
}
