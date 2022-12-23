using CourseProject.Calculus.Global;

namespace MSG
{
    public class MSG
    {
        private int _maxIter = 30000;
        private double _relativeDiscrepancy = 0;
        private GlobalVector _globalVector;
        private GlobalMatrix _globalMatrix;
        public MSG(GlobalMatrix globalMatrix, GlobalVector globalVector)
        {
            _globalVector = globalVector;
            _globalMatrix = globalMatrix;
        }

        public double[] Solve()
        {
            double[] Solution = new double[_globalVector.Values.Length];
            double[] Ax0 = new double[_globalVector.Values.Length];

            MultiplyMatrixByVector(Solution, Ax0);

            double[] r0 = new double[_globalVector.Values.Length];
            double[] z0 = new double[_globalVector.Values.Length];

            r0 = Ax0.Select((value, index) => _globalVector.Values[index]  - value).ToArray();
            z0 = r0.Select(x => x).ToArray();
            double alpha = 0;
            double betta = 0;

            for (int k = 1; k < _maxIter; k++)
            {
                var r0Norm = CalcScalarProduct(r0, r0);
                MultiplyMatrixByVector(z0, Ax0);
                alpha = r0Norm / CalcScalarProduct(Ax0, z0); //	alpha_k = (r_(k-1),r_(k-1)) / (A*z_(k-1),z_(k-1))
                Solution = Solution.Select((value, index) => value + alpha * z0[index]).ToArray();
                MultiplyMatrixByVector(z0, Ax0);
                r0 = r0.Select((value, index) => value - alpha * Ax0[index]).ToArray();
                betta = CalcScalarProduct(r0, r0) / r0Norm;
                z0 = z0.Select((value, index) => r0[index] + betta * value).ToArray();

                if (CalcNorm(r0) / CalcNormGlobalVector(_globalVector) < _relativeDiscrepancy) // ||r_k|| / ||f|| < e
                {

                    return Solution;
                }
            }

            throw new ArgumentException("Too long");
        }

        private void MultiplyMatrixByVector(double[] InitialApproximation, double[] Solution)
        {
            Array.Clear(Solution, 0, Solution.Length);
            for (int i = 0; i < Solution.Length; i++)
                Solution[i] = _globalMatrix.Diag[i] * InitialApproximation[i];

            for (int i = 1; i < Solution.Length; i++)
            {
                int i0 = _globalMatrix.RowPtr[i];
                int i1 = _globalMatrix.RowPtr[i + 1];
                for (int j = 0; j < (i1 - i0); j++)
                {
                    Solution[i] += _globalMatrix.Values[i0 + j] * InitialApproximation[_globalMatrix.ColumnPtr[i0 + j]]; // нижний треугольник
                    Solution[_globalMatrix.ColumnPtr[i0 + j]] += _globalMatrix.Values[i0 + j] * InitialApproximation[i]; // верхний треугольник
                }
            }
        }

        private double CalcScalarProduct(double[] vector1, double[] vector2)
        {
            double sum = 0;
            for (int i = 0; i < vector1.Length; i++)
            {
                sum += vector1[i] * vector2[i];
            }

            return sum;
        }

        private double CalcNorm(double[] vector)
        {
            double sum = 0;
            foreach (var data in vector)
            {
                sum += data * data;
            }

            return Math.Sqrt(sum);
        }

        private double CalcNormGlobalVector(GlobalVector vector)
        {
            double sum = 0;
            foreach (var data in vector.Values)
            {
                sum += data * data;
            }

            return Math.Sqrt(sum);
        }
    }
}