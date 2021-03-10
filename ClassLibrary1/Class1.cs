using System;

namespace ClassLibrary1
{
    public class Class1
    {
        public static double[,] GaussaMethod(double[,] bigMatrix)
        {
            int n = bigMatrix.GetLength(0);
            double a, d;
            a = bigMatrix[0, 0];
            for (int j = 0; j < n; j++)
            {
                bigMatrix[0, j] /= a;
            }
            for (int k = 1; k < n; k++)
            {
                d = bigMatrix[k, 0];
                for (int j = 0; j < n; j++)
                     bigMatrix[k, j] = bigMatrix[k, j] - bigMatrix[0, j] * d;
            }
            return bigMatrix;
        }
        public static void MatrixPrint(double[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    string result = $"{matrix[i, j]:f6}";
                    Console.Write(result+"\t");
                }
                Console.WriteLine();
            }
        }
        public static double[,] Zamena (double[,] matrix) // заміна другого і третього рядка
        {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    double temp = matrix[1, j];
                    matrix[1, j] = matrix[2, j];
                    matrix[2, j] = temp;
                }
            return matrix;
        }
        public static void VectorNeviazki(double[] arrayB, double[,] matrix, double[] arrayX)
        {
            Console.Write("\nВектор нев'язки: ");
            double[] newArray = new double[matrix.GetLength(0)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    newArray[i] += matrix[i, j] * arrayX[j]; //множення матриці на вектор
                }
            }
            double[] result = new double[arrayB.Length];
            for (int i = 0; i < arrayB.Length; i++)
            {
                result[i] = arrayB[i] - newArray[i]; // знаходження вектора нев'язки
            }
            for (int i = 0; i < result.Length; i++)
            {
                string resultStr = $"{result[i]:f6}";
                Console.Write(resultStr+"\t");
            }
        }
        public static double[,] NormForm(double[,] matrix, double[] arrayB)
        {
            double[,] bigMatrix = new double[matrix.GetLength(0), matrix.GetLength(1) + 1];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    bigMatrix[i, j] = matrix[i, j];
                }
            }                                                        //заповнення новоi матрицi bigMatrix
            for (int i = 0; i < bigMatrix.GetLength(0); i++)
            {
                bigMatrix[i, bigMatrix.GetLength(0)] = arrayB[i];
            }
            int side0 = bigMatrix.GetLength(0);
            int side1 = bigMatrix.GetLength(1);
            double[,] devidedBigMatrix = new double[side0, side1];
            for (int i = 0; i < side0; i++)
            {
                for (int j = 0; j < side1; j++)
                {
                    devidedBigMatrix[i, j] = bigMatrix[i, j] / bigMatrix[i, i];
                }
            } //поділена на діагональні елементи
            Console.WriteLine("\n``````````````````````` повна форма```````````````````````");
            MatrixPrint(devidedBigMatrix);
            Console.WriteLine("\n```````````````````` iтерацiйнi формули````````````````````");
            Console.WriteLine($"x1 = {devidedBigMatrix[0, side1 - 1]:f2} - {devidedBigMatrix[0, 1]:f2}x2 - {devidedBigMatrix[0, 2]:f2}x3 - {devidedBigMatrix[0, 3]:f2}x4");
            Console.WriteLine($"x2 = {devidedBigMatrix[1, side1 - 1]:f2} - {devidedBigMatrix[1, 0]:f2}x1 - {devidedBigMatrix[1, 2]:f2}x3 - {devidedBigMatrix[1, 3]:f2}x4");
            Console.WriteLine($"x3 = {devidedBigMatrix[2, side1 - 1]:f2} - {devidedBigMatrix[2, 0]:f2}x1 - {devidedBigMatrix[2, 1]:f2}x2 - {devidedBigMatrix[2, 3]:f2}x4");
            Console.WriteLine($"x4 = {devidedBigMatrix[3, side1 - 1]:f2} - {devidedBigMatrix[3, 0]:f2}x1 - {devidedBigMatrix[3, 1]:f2}x2 - {devidedBigMatrix[3, 2]:f2}x3");
            return devidedBigMatrix;
        }
        public static void IteraciYYYYY(double[,] normMatrix, double[,] matrixA, double[] arrayB)
        {
            double[,] devidedBigMatrix = NormForm(normMatrix, arrayB);
            double[] arrayX = new double[arrayB.Length];
            for (int i = 0; i < 7; i++)
            {
                Console.WriteLine($"\n``````````````````````{i + 1} iтерацiя````````````````````````\n");
                arrayX = Iteraciya(devidedBigMatrix, arrayX);
                double[] arrayX2 = new double[arrayX.Length];
                for (int j = 0; j < arrayX.Length; j++)
                {
                    arrayX2[j] = arrayX[j];
                }
                arrayX2 = Iteraciya(devidedBigMatrix, arrayX2);
                ArrayPrint(arrayX);
                VectorNeviazki(arrayB, matrixA, arrayX);
                ArrayEpsylon(arrayX, arrayX2);
                Console.WriteLine();
            }
        }
        public static double[] ArrayEpsylon(double[] array1, double[] array2)
        {
            double[] arrayForEps = new double[array1.Length];
            for (int i = 0; i < array1.Length; i++)
            {
                arrayForEps[i] = Math.Abs(array2[i] - array1[i]);
            }
            Console.Write("\nВектор визначення точностi: ");
            for (int i = 0; i < array1.Length; i++)
            {
                Console.Write($"{arrayForEps[i]:f6}\t");
            }
            return arrayForEps;
        }
        public static double[] Iteraciya(double[,] devidedBigMatrix, double[] arrayX)
        {
            int side1 = devidedBigMatrix.GetLength(1);
            arrayX[0] = devidedBigMatrix[0, side1 - 1] - devidedBigMatrix[0, 1] * arrayX[1] - devidedBigMatrix[0, 2] * arrayX[2] - devidedBigMatrix[0, 3] * arrayX[3];
            arrayX[1] = devidedBigMatrix[1, side1 - 1] - devidedBigMatrix[1, 0] * arrayX[0] - devidedBigMatrix[1, 2] * arrayX[2] - devidedBigMatrix[1, 3] * arrayX[3];
            arrayX[2] = devidedBigMatrix[2, side1 - 1] - devidedBigMatrix[2, 0] * arrayX[0] - devidedBigMatrix[2, 1] * arrayX[1] - devidedBigMatrix[2, 3] * arrayX[3];
            arrayX[3] = devidedBigMatrix[3, side1 - 1] - devidedBigMatrix[3, 0] * arrayX[0] - devidedBigMatrix[3, 1] * arrayX[1] - devidedBigMatrix[3, 2] * arrayX[2];
            return arrayX;
        }
        public static void ArrayPrint(double[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write($"{array[i]:f6}\t");
            }
        }
    }
}
