using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class Solving
    {
        public static double[] alpha;
        public static int N
        {
            get;
            set;
        }
        public static double[] kurantAlpha;
        public static Integral.KurantFunc Kurant = KurantFunction;       
        
        public static double X(int i, double a, double b)
        {
            double h = (b - a) / N;
            return a + i * h;
        }

        public static double Phi(double x, int i)
        {
            return Math.Pow(x, i);
        }

        public static double KurantFunction(double x, int k, double a, double b)
        {
            double result = 0;
           
            if (x < X(k - 1, a, b) && x >= 0)
            {
                result = 0;
            }
            else if (X(k - 1, a, b) <= x && x < X(k, a, b))
            {
                result = (x - X(k - 1, a, b)) / (X(k, a, b) - X(k - 1, a, b));
            }

            else if ((X(k, a, b) <= x && x < X(k + 1, a, b)))
            {
                result = (X(k + 1, a, b) - x) / (X(k + 1, a, b) - X(k, a, b));
            }

            else if (x >= X(k + 1, a, b))
            {
                result = 0;
            }
            return result;
        }
        public static double F(double x)
        {
            //return Math.Cos(x);
            //return Math.Sin(x);           
            //return Math.Pow(Math.Sin(3*x), Math.E);
            //return x * Math.Cos(x);

            return Math.Sin(Math.Pow(Math.E, 3 * x));
        }

        public static double PhiKPhiJ(int k, int j, double a, double b, int n)
        {
            double result = 0;
            result = Integral.IntegralPhiKPhiJ1(k,j,a,b,n);
            return result;
        }

        public static double KurantPhiKPhiJ(int k, int j, double a, double b, int n)
        {
            double result = 0;
            result = Integral.IntegralPhiKPhiJ(k, j, Kurant, a, b, n);
            return result;
        }

        public static double FPhiJ(int j, double a, double b, int n)
        {
            double result = 0;
            result = Integral.IntegralFPhiJ1(j, a, b, n);
            return result;
        }

        public static double KurantFPhiJ(int j, double a, double b, int n)
        {
            double result = 0;
            result = Integral.IntegralFPhiJ(j, a, b, n);
            return result;
        }


        public static double[] GetAlphaForKurant(double a, double b)
        {
            kurantAlpha = new double[N + 1];
            double[,] A = new double[N+1, N+1];
            double[] B = new double[N+1];
            for (int k = 0; k < N+1; k++)
            {
                for (int j = 0; j < N+1; j++)
                {
                    A[k, j] = KurantPhiKPhiJ(k, j, a, b, N);
                }
            }

            for (int j = 0; j < N+1; j++)
            {
                B[j] = KurantFPhiJ(j, a, b, N);
            }
            kurantAlpha = JordanGaussMethod.JordanGaus(A, B, N + 1);
            return kurantAlpha;
        }


        public static double[] GetAlpha(int m, double a, double b)
        {
            alpha = new double[m + 1];
            double[,] A = new double[m + 1, m + 1];
            double[] B = new double[m + 1];
            for (int k = 0; k < m + 1; k++)
            {
                for (int j = 0; j < m + 1; j++)
                {
                    A[k, j] = PhiKPhiJ(k, j, a, b, N);                   
                }
            }

            for (int j = 0; j < m + 1; j++)
            {
                B[j] = FPhiJ(j, a, b, N);            
            }
            alpha = JordanGaussMethod.JordanGaus(A, B, m + 1);
            return alpha;
        }
 
        public static double PhiPolinomialForKurant(double x, double a, double b)
        {
            double result = 0;
            if (kurantAlpha == null)
            {
                kurantAlpha = GetAlphaForKurant(a, b);
            }
            for (int i = 0; i < N + 1; i++)
            {
                result += kurantAlpha[i] * KurantFunction(x, i, a, b);
            }

            return result;
        }

        public static double PhiPolinomial(double x, int m, double a, double b)
        {
            double result = 0;
            if (alpha == null)
            {
                alpha = GetAlpha(m, a, b);
            }
            for (int i = 0; i < m + 1; i++)
            {
                result += alpha[i] * Phi(x, i);
            }
            return result;
        }
    }
}
