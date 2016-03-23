using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Метод_найменших_квадратів
{
    public class Program
    {
        public static int n = 20;
        public static double a = -1;
        public static double b = 2;
        //public double[]x = new double[n];
        public static double X(int i)
        {
            double h = (b-a)/n;
            return a + i * h;
        }
        public static double Phi(double x, int i)
        {
            return Math.Pow(x, i);
        }
        public static double F(double x)
        {
            return Math.Pow(2, x);
        }
        public static double PhiKPhiJ(/*double[]x, */int k, int j)
        {
            double result = 0;
            for(int i=0;i<=n;i++)
            {
                //result += (Phi(x[i], k) * Phi(x[i], j));
                result += (Phi(X(i), k) * Phi(X(i), j));
            }
            return result;
        }
        public static double FPhiJ(/*double[]x, int n, */int j)
        {
            double result = 0;
            for(int i = 0; i <= n;i++)
            {
                //result += (F(x[i]) * Phi(x[i], j));
                result += (F(X(i)) * Phi(X(i), j));
            }
            return result;
        }
      
        public static double[] GetAlpha(int m)
        {
            m = n;
            double[]alpha = new double[m];
            double[,] A = new double[n, n + 1];
            double[] B = new double[n + 1];
            for (int k = 0; k < n; k++)
            {
                for (int j = 0; j < n; j++)
                {
                    A[k, j] = PhiKPhiJ(k, j);
                }
            }

            for (int j = 0; j < n;j++)
            {
                B[j] = FPhiJ(j);
            }
           for(int k=0;k<m;k++)
           {
               alpha[k] = JordanGaussMethod.JordanGaus(A, B, m)[k];
           }
            return alpha;
        }
        public static double PhiPolinomial(double x, int m)
        {
            double result = 0;
            for(int i=0;i<m;i++)
            {
                result += GetAlpha(m)[i] * Phi(x, i);
            }
            return result;
        }
        static void Main(string[] args)
        {
            int m=n;
            for(int i=0;i<m;i++)
            {
               // Console.WriteLine("{0} ", GetAlpha(m)[i]);
                //Console.WriteLine("{0} ", PhiPolinomial(X(i), m));
            }
            
        }
    }
}
