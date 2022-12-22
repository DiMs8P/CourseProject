using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.DataStructures.Matrixes
{
    public class SymmetricMatrix
    {
        public readonly double[,] Data;
        public SymmetricMatrix(double[,] data)
        {
            Data = data;
        }

        public double this[int i, int j]
        {

            get =>  j >= i ? Data[j, i] : Data[i, j];
            set
            {
                if (j > i)
                {
                    Data[i, j] = value;
                }
                else
                {
                    Data[i, j] = value;
                }
                
            }
        }

        public static SymmetricMatrix operator /(SymmetricMatrix matrix, double value)
        {
            if (Math.Abs(value) < 1E-4)
            {
                throw new ArgumentException("Dividing by zero");
            }
            for (int i = 0; i < matrix.Data.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.Data.GetLength(1); j++)
                {
                    matrix.Data[i, j] /= value;
                }
            }

            return matrix;
        }

        public static SymmetricMatrix operator *(SymmetricMatrix matrix, double value)
        {
            for (int i = 0; i < matrix.Data.GetLength(0); i++)
            {
                for (int j = 0; j < i; j++)
                {
                    matrix.Data[i, j] *= value;
                }
            }

            return matrix;
        }

        public static SymmetricMatrix operator +(SymmetricMatrix matrix1, SymmetricMatrix matrix2)
        {
            if (matrix1.Data.GetLength(0) != matrix2.Data.GetLength(0) &&
                matrix1.Data.GetLength(1) != matrix2.Data.GetLength(1))
                throw new ArgumentException("Different length");

            for (int i = 0; i < matrix1.Data.GetLength(0); i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    matrix1[i, j] += matrix2[i, j];
                }
            }
            return matrix1;
        }
    }
}
