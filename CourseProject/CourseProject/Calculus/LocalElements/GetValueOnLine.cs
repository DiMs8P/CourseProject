using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileGenerators;

namespace CourseProject.Calculus.LocalElements
{
    public class GetValueOnLine
    {
        public static double Get(double k, double b, double point)
        {
            return k * point + b;
        }
    }
}
