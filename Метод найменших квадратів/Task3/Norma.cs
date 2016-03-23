using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public static class Norma
    {
        public static Integral.Un Un = (x, a, b) => { return Math.Pow(Solving.PhiPolinomialForKurant(x, a, b), 2); };
        public static Integral.Function F = (x) => { return Math.Pow(Solving.F(x), 2); };
        public static Integral.Un UnF = (x, a, b) => { return Math.Pow(Solving.PhiPolinomialForKurant(x, a, b) - Solving.F(x), 2); };

        public static double NormaUn(double a, double b)
        {
            //   return Math.Sqrt(Integral.IntegralUn(Un, a, b, n));
            return Math.Sqrt(Integral.CalculateIntegralImprovement(Un, a, b));
        }

        public static double NormaF(double a, double b)
        {
            return Math.Sqrt(Integral.CalculateIntegralImprovementF(F, a, b));
        }

        public static double NormaUnF(double a, double b)
        {
           // return Math.Sqrt(Integral.IntegralUnF(Un, F, a, b, n));
            return Math.Sqrt(Integral.CalculateIntegralImprovement(UnF, a, b));
        }

        public static double NormaUnFDivUn(double a, double b)
        {
            return ((NormaUnF(a, b) / NormaF(a, b))*100);
        }

    }
}
