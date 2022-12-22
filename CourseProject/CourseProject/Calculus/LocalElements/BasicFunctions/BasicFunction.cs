using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Calculus.LocalElements.BasicFunctions
{
    public class BasicFunction
    {
        readonly double[] _coefficients;
        private readonly double _delta = 0.00001;

        public BasicFunction(double[] x)
        {
            if (x.Length < 3)
            {
                throw new ArgumentException();
            }

            _coefficients = x;
        }

        public double ValueIn(double radius, double angle)
        {
            return _coefficients[0] + _coefficients[1] * radius + _coefficients[2] * angle;
        }

        public double ValueRadiusDerivativeIn(double radius, double angle)
        {
            return (ValueIn(radius + _delta, angle) - ValueIn(radius - _delta, angle)) / (2 * _delta);
        }

        public double ValueAngleDerivativeIn(double radius, double angle)
        {
            return (ValueIn(radius, angle + _delta) - ValueIn(radius, angle - _delta)) / (2 * _delta);
        }
    }
}
