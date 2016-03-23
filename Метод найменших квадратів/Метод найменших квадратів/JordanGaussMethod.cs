using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Метод_найменших_квадратів
{
    class JordanGaussMethod
    {
        public static double[] MatrixAndVectorMultiplication(int m, double[,] A, double[] B)
        {
            double[] result = new double[m];
            for (int i = 0; i < m; i++)
            {
                result[i] = 0;
                for (int j = 0; j < m; j++)
                {
                    result[i] += A[i, j] * B[j];
                }
            }
            return result;
        }
        static double[,] T(double[,] A, int k, int m)
        {
            double[,] matrixT = new double[m, m];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (i == j)
                    {
                        matrixT[i, j] = 1;
                    }
                    else
                    {
                        matrixT[i, j] = 0;
                    }
                }
            }
            for (int i = 0; i < m; i++)
            {
                if (i == k)
                {
                    matrixT[i, k] = 1 / (A[i, k]);
                }
                else
                {
                    matrixT[i, k] = (-(A[i, k] / A[k, k]));
                }
            }
            return matrixT;
        }
        public static double[] JordanGaus(double[,] A, double[] B, int m)
        {
            double[] x = new double[m];
            double[] temp = new double[m];
            double[,] matrixT = new double[m, m];
            double[,] obernenaA = new double[m, m];
            double[,] obernenaTemp = new double[m, m];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (i == j)
                    {
                        obernenaA[i, j] = 1;
                    }
                    else
                    {
                        obernenaA[i, j] = 0;
                    }
                }
            }
            for (int k = 0; k < m; k++)
            {
                matrixT = T(A, k, m);
                A = MultiplyingTwoMatrix(matrixT, A, m);
                B = MatrixAndVectorMultiplication(m, matrixT, B);
            }
            for (int i = m - 1; i >= 0; i--)
            {
                obernenaA = MultiplyingTwoMatrix(obernenaA, T(A, i, m), m);
            }
            x = MatrixAndVectorMultiplication(m, obernenaA, B);
            return x;
        }
        static double[,] MultiplyingTwoMatrix(double[,] A, double[,] B, int m)
        {
            double[,] result = new double[m, m];
            double sum;
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    sum = 0;
                    for (int k = 0; k < m; k++)
                    {
                        sum += A[i, k] * B[k, j];
                    }
                    result[i, j] = sum;
                }
            }
            return result;
        }
    }
}
