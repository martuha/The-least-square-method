using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public static class Integral
    {
        public delegate double KurantFunc(double x, int k, double a, double b);
        public delegate double Un(double x, double a, double b);
        public delegate double Function(double x);
    
        public static double CalculateIntegral(Un F, double a, double b, int n)
        {
          //  n = 20;
            double h = (b - a) / n;
            double integral = 0;
            double xi = 0;
            for (int i = 1; i < n; i++)
            {
                xi = a + i * h;
                integral += F(xi, a, b);
            }
            integral *= h;
            integral += (0.5 * h) * (F(a, a, b) + F(b, a, b));
            return integral;          
        }

        //public static double IntegralUn(Un F, double a, double b, int n)
        //{
        //    double h = (b - a) / n;
        //    double integral = 0;
        //    double xi = 0;
        //    for (int i = 1; i < n; i++)
        //    {
        //        xi = a + i * h;
        //        integral += Math.Pow(F(xi, a, b, n), 2);//F(xi,a,b,n) * F(xi, a,b,n);
        //    }
        //    integral *= h;
        //    integral += (0.5*h)*(Math.Pow(F(a,a,b,n), 2) + Math.Pow(F(b,a,b,n),2));//(0.5 * h) * (F(a, a, b, n) * F(a, a, b, n) + F(b, a, b, n) * F(b, a, b, n));
        //    return integral;
        //}

        public static double IntegralF(Function F, double a, double b, int n)
        {
           // n = 20;
            double h = (b - a) / n;
            double integral = 0;
            double xi = 0;
            for (int i = 1; i < n; i++)
            {
                xi = a + i * h;
                integral += F(xi);
            }
            integral *= h;
            integral += (0.5 * h) * (F(a) + F(b));
            return integral;
        } 

        public static double CalculateIntegralImprovement(Un F, double a, double b)
        {
            int n = 20;
            double integral1, integral2;
            double error;
            integral2 = CalculateIntegral(F, a, b, n);

            do
            {
                integral1 = integral2;
                n*=2;
                integral2 = CalculateIntegral(F,a,b,n);
                error = Math.Abs((integral2-integral1)*100/integral1);
            }
            while(error>=3);
            return integral2;
        }

        public static double CalculateIntegralImprovementF(Function F, double a, double b)
        {
            int n = 20;
            double integral1, integral2;
            double error;
            integral2 = IntegralF(F, a, b, n);

            do
            {
                integral1 = integral2;
                n *= 2;
                integral2 = IntegralF(F, a, b, n);
                error = Math.Abs((integral2 - integral1) * 100 / integral1);
            }
            while (error >= 3);
            return integral2;
        }
        //public static double IntegralUnF(Un Un, Function F, double a, double b, int n)
        //{
        //    double h = (b - a) / n;
        //    double integral = 0;
        //    double xi = 0;
        //    for (int i = 1; i < n; i++)
        //    {
        //        xi = a + i * h;
        //        integral += (Un(xi, a, b, n)-F(xi)) * (Un(xi, a, b, n)-F(xi));
        //    }
        //    integral *= h;
        //    integral += (0.5 * h) * ((Un(a, a, b, n)-F(a)) * (Un(a, a, b, n)-F(a)) + (Un(b, a, b, n)-F(b)) * (Un(b, a, b, n)-F(b)));
        //    return integral;
        //}

        public static double IntegralPhiKPhiJ(int k, int j, KurantFunc F, double a, double b, int n)
        {
            double h = (b - a) / n;
            //double h = Math.Abs((Solving.X(j+1, a,b,n) -Solving.X(j,a,b,n))/n);
            double integral = 0;
            double xi = 0;
            for (int i = 1; i < n; i++)
            {
                xi = a + i * h;
                integral += F(xi, k, a, b) * F(xi, j, a, b);
            }
            integral *= h;
            integral += (0.5 * h) * (F(a, k, a, b)*F(a, j, a, b) + F(b, k, a, b)*F(b, j, a, b));
            return integral;
        }

        public static double IntegralFPhiJ(int j, double a, double b, int n)
        {
            double h = (b - a) / n;
            double integral = 0;
            double xi = 0;
            for (int i = 1; i < n; i++)
            {
                xi = a + i * h;
                integral += Solving.F(xi) * Solving.Kurant(xi, j, a, b);
            }
            integral *= h;
            integral += (0.5 * h) * (Solving.F(a) * Solving.Kurant(a, j, a, b) + Solving.F(b) * Solving.Kurant(b, j, a, b));

            return integral;
        }

        public static double IntegralFPhiJ1(int j, double a, double b, int n)
        {
            n = 20;
            double h = (b - a) / n;
            double integral = 0;
            double xi = 0;
            for (int i = 1; i < n; i++)
            {
                xi = a + i * h;
                integral += Solving.F(xi) * Solving.Phi(xi, j);
            }
            integral *= h;
            integral += (0.5 * h) * (Solving.F(a) * Solving.Phi(a, j) + Solving.F(b) * Solving.Phi(b, j));

            return integral;
        }

        public static double IntegralPhiKPhiJ1(int k, int j, double a, double b, int n)
        {
            n = 20;
            double h = (b - a) / n;
            double integral = 0;
            double xi = 0;
            for (int i = 1; i < n; i++)
            {
                xi = a + i * h;
                integral += Solving.Phi(xi, k) * Solving.Phi(xi, j);
            }
            integral *= h;
            integral += (0.5 * h) * (Solving.Phi(a, k) * Solving.Phi(a, j) + Solving.Phi(b, k) * Solving.Phi(b, j));
            return integral;
        }      
    }
}
