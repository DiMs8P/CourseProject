using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CourseProject.DataStructures.Matrixes
{
    public class Matrix
    {
        public readonly double[,] Data;

        public Matrix(double[,] data)
        {
            Data = data;
        }

        public static Matrix operator *(Matrix matrix, double value)
        {
            for (int i = 0; i < matrix.Data.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.Data.GetLength(1); j++)
                {
                    matrix.Data[i, j] *= value;
                }
            }

            return matrix;
        }

        public static Matrix operator /(Matrix matrix, double value)
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

        public static Matrix operator +(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.Data.GetLength(0) != matrix2.Data.GetLength(0) &&
                matrix1.Data.GetLength(1) != matrix2.Data.GetLength(1))
                throw new ArgumentException("Different length");

            for (int i = 0; i < matrix1.Data.GetLength(0); i++)
            {
                for (int j = 0; j < matrix1.Data.GetLength(1); j++)
                {
                    matrix1.Data[i, j] += matrix2.Data[i, j];
                }
            }
            return matrix1;
        }

        public Matrix GetCopy()
        {
            double[,] newData = new double[Data.GetLength(0), Data.GetLength(1)];
            for (int i = 0; i < Data.GetLength(0); i++)
            {
                for (int j = 0; j < Data.GetLength(1); j++)
                {
                    newData[i, j] *= Data[i,j];
                }
            }

            return new Matrix(newData);
        }

        public double this[int i, int j]
        {
            get => Data[i, j];
            set => Data[i, j] = value;
        }
    }
}
