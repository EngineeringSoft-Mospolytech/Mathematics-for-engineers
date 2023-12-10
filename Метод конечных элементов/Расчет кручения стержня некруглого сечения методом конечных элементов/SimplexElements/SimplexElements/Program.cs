using Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MatrixMath
{
    abstract public class MatrixOperations
    {
        public static double[,] AddMatrix(double[/*строка*/,/*столбец*/] X,
                                       double[/*строка*/,/*столбец*/] Y)
        {
            double[,] res = new double[X.GetLength(0), X.GetLength(1)];
            if ((X.GetLength(0) != Y.GetLength(0)) || ((X.GetLength(1) != Y.GetLength(1))))
                return res;
            int m = X.GetLength(1);
            for (int i = 0; i < X.GetLength(0); i++)
            {
                for (int j = 0; j < X.GetLength(1); j++)
                {
                    res[i, j] = X[i, j] + Y[i, j];
                }
            }
            return res;
        }

        public static double[,] MatrixMultiply(double[/*строка*/,/*столбец*/] X,
                                       double[/*строка*/,/*столбец*/] Y)
        {
            double[,] res = new double[X.GetLength(0), Y.GetLength(1)];
            if (X.GetLength(1) != Y.GetLength(0))
                return res;
            int m = X.GetLength(1);
            for (int i = 0; i < X.GetLength(0); i++)
            {
                for (int j = 0; j < Y.GetLength(1); j++)
                {
                    for (int l = 0; l < m; l++)
                    {
                        res[i, j] += X[i, l] * Y[l, j];
                    }
                }
            }
            return res;
        }

        public static double[,] MatrixMultiply(double[/*строка*/,/*столбец*/] X,
                                       double scalar)
        {
            double[,] res = new double[X.GetLength(0), X.GetLength(1)];
            for (int i = 0; i < X.GetLength(0); i++)
            {
                for (int j = 0; j < X.GetLength(1); j++)
                {
                    res[i, j] = scalar * X[i, j];
                }
            }
            return res;
        }

        //  Определитель методом Сарюса
        public static double DeterminantS(double[/*строка*/,/*столбец*/] X)
        {
            double res = 0;
            if (X.GetLength(1) != X.GetLength(0) || X.GetLength(1) != 3)
                return 0;
            double[/*строка*/,/*столбец*/] Y = new double[3, 5];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    Y[i, j] = X[i, j];
            for (int i = 0; i < 3; i++)
                for (int j = 3; j < 5; j++)
                    Y[i, j] = X[i, j - 3];

            for (int j = 0; j < 3; j++)
            {
                double t = 1;
                for (int i = 0; i < 3; i++)
                    t *= Y[i, i + j];
                res += t;
            }

            for (int j = 2; j < 5; j++)
            {
                double t = 1;
                for (int i = 0; i < 3; i++)
                    t *= Y[i, j - i];
                res -= t;
            }
            return res;
        }

 /*       public static double Determinant(double[,] X)
        {
            double res = 0;
            if (X.GetLength(0) != X.GetLength(1))
                return 0;
            //double t = 0;
            if (X.GetLength(0) == 2)
            {
                return X[0, 0] * X[1, 1] - X[0, 1] * X[1, 0];
            }
            int temp = X.GetLength(0) - 1;
//            double[,] Y = new double[temp, temp];
            for (int i = 0; i < X.GetLength(0); i++)
            {
                int counter = 0;
                double[,] Y = new double[temp, temp];
                for (int j = 0; j < temp + 1; j++)
                {
                    if (j == 0)
                        continue;
                    for (int c = 0; c < temp + 1; c++)
                    {
                        if (c == i)
                            continue;
                        Y[counter / temp, counter % temp] = X[j, c];
                        counter++;
                    }
                }

                res += Math.Pow(-1, i + 1 + 1) * X[0, i] * Determinant(Y);
            }
            return res;
        }*/

        public static double Determinant(double[,] X)
        {
            double res = 0;
            if (X.GetLength(0) != X.GetLength(1))
                return 0;
            if (X.GetLength(0) == 2)
            {
                return X[0, 0] * X[1, 1] - X[0, 1] * X[1, 0];
            }
            for (int i = 0; i < X.GetLength(1); i++)
            {
                res +=  X[0, i] * AlgebraAdd(X, 0, i);
            }
            return res;
        }

        public static double AlgebraAdd(double[/*строка*/,/*столбец*/] X, int i, int j)
        {
            int temp = X.GetLength(0) - 1;
            double[,] Y = new double[temp, temp]; //    Минор
            //            int counter = 0;
            int ypos = 0;
            for (int k = 0; k < temp + 1; k++)
            {
                if (i == k)
                    continue;
                int xpos = 0;
                for (int c = 0; c < temp + 1; c++)
                {
                    if (c == j)
                        continue;
                    Y[ypos, xpos] = X[k, c];
                    xpos++;
  //                  counter++;
                }
                ypos++;
            }
            return Math.Pow(-1, i + j) * Determinant(Y);
        }

        public static double[,] T(double[/*строка*/,/*столбец*/] X)
        {
/*            if (X.GetLength(0) != X.GetLength(1))
                return null;*/
            double[,] B = new double[X.GetLength(1), X.GetLength(0)];
            for (int i = 0; i < X.GetLength(1); i++)
                for (int j = 0; j < X.GetLength(0); j++)
                    B[i, j] = X[j, i];
            return B;
        }

        public static double[,] ReverseMatrix(double[/*строка*/,/*столбец*/] X)
        {
            double[,] B = new double[X.GetLength(0), X.GetLength(0)];
            double det = Determinant(X);
            if (det == 0)
                return B;
            for (int i = 0; i < X.GetLength(1); i++)
            {
                for (int j = 0; j < X.GetLength(0); j++)
                {
                    B[i, j] = AlgebraAdd(X, i, j);
                }
            }
            //            B = T(B);
            for (int i = 0; i < X.GetLength(1); i++)
            {
                for (int j = 0; j < X.GetLength(0); j++)
                {
                    B[i, j] = B[i, j] / det;
                }
            }
            return T(B);
        }

        //  Вычисление системы линейных уравнений методом Гаусса
        public static double[] LinSystemGauss(double[/*строка*/,/*столбец*/] system, double[] add)
        {
            if (system.GetLength(1) != add.Length)
                return null;
            int N = system.GetLength(0);
            for (int i = 0; i < N; i++) //строки
            {
                for (int j = 0; j < i; j++) //столбцы
                {
                    double a = -1;
                    int k;
                    for (k = i - 1; k >= 0; k--)
                    {
                        if (system[k, j] != 0)
                        {
                            a = system[i, j] / system[k, j];
                            break;
                        }
                    }
                    if (k < 0)
                        continue; //!
                    for (int c = 0; c < N; c++)
                    {
                        system[i, c] -= system[k, c] * a;
                    }
                    add[i] -= add[k] * a;
                }
            }
            double[] res = new double[N];
            for (int i = N - 1; i >= 0; i--)
            {
                double a = add[i];
                for (int j = N - 1; j > i; j--)
                {
                    a -= system[i, j] * res[j];
                }
                if (system[i, i] != 0)
                {
                    res[i] = a / system[i, i];
                }
            }
            return res;
        }

        //  Вычисление факториала
        public double factorial(double n)
        {
            int a = 1;
            for (int i = 1; i <= n; i++)
                a *= i;
            return a;
        }
    }
}

namespace Geometry
{
    public struct VectorU
    {
        public double[] U;
        public VectorU(int d /* измерения*/)
        {
            U = new double[d];
        }
        public VectorU(double X)
        {
            U = new double[1];
            U[0] = X;
        }
        public VectorU(double X, double Y)
        {
            U = new double[2];
            U[0] = X;
            U[1] = Y;
        }
        public VectorU(double X, double Y, double Z)
        {
            U = new double[3];
            U[0] = X;
            U[1] = Y;
            U[2] = Z;
        }
        public VectorU(double[] V)
        {
            int l = V.Length;
            U = new double[l];
            for (int i = 0; i < l; i++)
            {
                U[i] = V[i];
            }
        }

        public int GetDimentions()
        {
            return U.Length;
        }

        public double GetLength()
        {
            double Sigm = 0;
            for (int i = 0; i < U.Length; i++)
            {
                Sigm += Math.Pow(U[i], 2);
            }
            return Math.Sqrt(Sigm);
        }
    }

    public class Point
    {
        public int id;
        public double[] point;
        public bool Fixed = false;

        public Point(int d, int ID /* измерения*/)
        {
            point = new double[d];
            id = ID;
            Fixed = false;
        }
        public Point(double X, int ID)
        {
            point = new double[1];
            point[0] = X;
            id = ID;
            Fixed = false;
        }
        public Point(double X, double Y, int ID)
        {
            point = new double[2];
            point[0] = X;
            point[1] = Y;
            id = ID;
            Fixed = false;
        }
        public Point(double X, double Y, double Z, int ID)
        {
            point = new double[3];
            point[0] = X;
            point[1] = Y;
            point[2] = Z;
            id = ID;
            Fixed = false;
        }
        public Point(double[] P, int ID)
        {
            int l = P.Length;
            point = new double[l];
            for (int i = 0; i < l; i++)
            {
                point[i] = P[i];
            }
            id = ID;
            Fixed = false;
        }

        public int GetDimentions()
        {
            return point.Length;
        }
    }

    public struct Line
    {
        public Point Point1;
        public Point Point2;

        public Line(Point a, Point b)
        {
            Point1 = a;
            Point2 = b;
        }

    }

    public class Element
    {
        public Point[] points;
        private int Dimentions;
        private int id;
        public double A;

        private static double Determinant(double[/*строка*/,/*столбец*/] X)
        {
            double res = 0;
            if (X.GetLength(0) != X.GetLength(1))
                return 0;
            //double t = 0;
            if (X.GetLength(0) == 2)
            {
                return X[0, 0] * X[1, 1] - X[0, 1] * X[1, 0];
            }
            int temp = X.GetLength(0) - 1;
            double[,] Y = new double[temp, temp];
            for (int i = 0; i < X.GetLength(0); i++)
            {
                int counter = 0;
                for (int j = 0; j < temp + 1; j++)
                {
                    if (j == 0)
                        continue;
                    for (int c = 0; c < temp + 1; c++)
                    {
                        if (c == i)
                            continue;
                        Y[counter / temp, counter % temp] = X[j, c];
                        counter++;
                    }
                }

                res += Math.Pow(-1, i + 1 + 1) * X[0, i] * Determinant(Y);
            }
            return res;
        }

        public Element(Point[] ps, int ID)
        {
            points = ps;
            id = ID;
            Dimentions = ps.Length - 1;
            A = 0;
        }

        public Element(Point p1, Point p2, Point p3, int ID)
        {
            points = new Point[3];
            points[0] = p1;
            points[1] = p2;
            points[2] = p3;
            id = ID;
            Dimentions = 2;
            A = 0;
        }

        public int GetID()
        {
            return id;
        }

        public int GetDimentions()
        {
            return Dimentions;
        }

        public void CalcA()
        {
            double[,] C = new double[Dimentions + 1, Dimentions + 1];
            for (int i = 0; i < Dimentions + 1; i++)
            {
                C[i, 0] = 1;
                for (int j = 1; j < Dimentions + 1; j++)
                {
                    C[i, j] = points[i].point[j - 1];
                }
            }
            A = Math.Abs(Determinant(C) / 2);
        }

        public bool points_Contains(int ID)
        {
            for (int i = 0; i < points.Length; i++)
            {
                if (points[i].id == ID)
                    return true;
            }
            return false;
        }
    }

    public class Sketch
    {
        private List<Point> points = new List<Point> { };
        private List<Element> elements = new List<Element> { };
        private int dimentions = 0;

        private int GetPointIndexByID(int ID)
        {
            for(int i = 0; i < points.Count; i++)
            {
                if (points[i].id == ID)
                    return i;
            }
            return -1;
        }

        public void AddPoint(Point p)
        {
            if (dimentions == 0)
            {
                dimentions = p.GetDimentions();
            }
            if(p.GetDimentions() == dimentions) 
                points.Add(p);
        }

        public void AddPoint(double x, double y, int id)
        {
            Point point = new Point(x, y, id);
            if (dimentions == 0)
            {
                dimentions = point.GetDimentions();
            }
            if (point.GetDimentions() == dimentions)
                points.Add(point);
        }

        public void FixPoint(int ID)
        {
            points[ID].Fixed = true;
        }

        public void AddElement(int[] IDs)
        {
            int ln = IDs.Length;
            Point[] pts = new Point[ln];
            for (int i = 0; i < ln; i++)
            {
                int index = GetPointIndexByID(IDs[i]);
                pts[i] = points[index];
            }
            int newNum = elements.Count;
            Element element = new Element(pts, newNum);
            elements.Add(element);
        }

        public int GetPointsCount()
        {
            return points.Count;
        }

        public Point GetPointByID(int ID)
        {
            int index = GetPointIndexByID(ID);
            return points[index];
        }

        public Element GetElementByID(int ID)
        {
            return elements[ID];
        }

        public int GetElementsCount()
        {
            return elements.Count;
        }
    }
}

namespace SimplexElements
{

    public struct VectorU
    {
        public double[] U;
        public VectorU(int d /* измерения*/)
        {
            U = new double[d];
        }
        public VectorU(double X)
        {
            U = new double[1];
            U[0] = X;
        }
        public VectorU(double X, double Y)
        {
            U = new double[2];
            U[0] = X;
            U[1] = Y;
        }
        public VectorU(double X, double Y, double Z)
        {
            U = new double[3];
            U[0] = X;
            U[1] = Y;
            U[2] = Z;
        }
        public VectorU(double[] V)
        {
            int l = V.Length;
            U = new double[l];
            for (int i = 0; i < l; i++)
            {
                U[i] = V[i];
            }
        }

        public int GetDimentions()
        {
            return U.Length;
        }

        public double GetLength()
        {
            double Sigm = 0;
            for (int i = 0; i < U.Length; i++)
            {
                Sigm += Math.Pow(U[i], 2);
            }
            return Math.Sqrt(Sigm);
        }
    }

    public struct Point
    {
        public double[] point;
        public Point(int d /* измерения*/)
        {
            point = new double[d];
        }
        public Point(double X)
        {
            point = new double[1];
            point[0] = X;
        }
        public Point(double X, double Y)
        {
            point = new double[2];
            point[0] = X;
            point[1] = Y;
        }
        public Point(double X, double Y, double Z)
        {
            point = new double[3];
            point[0] = X;
            point[1] = Y;
            point[2] = Z;
        }
        public Point(double[] P)
        {
            int l = P.Length;
            point = new double[l];
            for (int i = 0; i < l; i++)
            {
                point[i] = P[i];
            }
        }

        public int GetDimentions()
        {
            return point.Length;
        }
    }

    public class MKE : MatrixMath.MatrixOperations   // Переделать элемент или скетч, запихнуть С в элемент
    {
        private Geometry.Sketch sketch;
        private int Q = 0;
        private double[,] GlobalStiffnessMatrix;
        private double[,] GlobalFunction;

        public MKE()
        {
            sketch = new Geometry.Sketch();
        }

        public void AddPoint(Point p)
        {
            Geometry.Point point = new Geometry.Point(p.point, Q);
            sketch.AddPoint(point);
            Q++;
        }

        public void FixPoint(int ID)
        {
            sketch.GetPointByID(ID).Fixed = true;
        }

        public void AddElement(int[] IDs)
        {
            sketch.AddElement(IDs);
        }

        //  интеграл вдоль сторон элемента
        private double LCordIntegral(int d, double A, int a = 1 /*доработать a!b!c!...*/)
        {
            return 1 / factorial(a + d) * A * factorial(d);
        }

        public double[,] RotationFunction(double G, double TO, int ID)
        {
            Element element = sketch.GetElementByID(ID);
            int d = element.GetDimentions();
            int d1 = d + 1;
            int pc = sketch.GetPointsCount();
            double[,] B = new double[pc, 1];
            if (element.A == 0)
                element.CalcA();
            for (int i = 0; i < pc; i++)
            {
                if (element.points_Contains(i))
                {
//                    double t = LCordIntegral(element.GetDimentions(), element.A);
                    B[i, 0] = 2 * G * TO * LCordIntegral(element.GetDimentions(), element.A);
                }
                else
                    B[i, 0] = 0;
            }
            return B;
        }

        //  Матрица жесткости
        public double[,] StiffnessMatrix(    // изменить доступ
            int[] IDs,          //   Номера точек элемента 
            ref double A
        )
        {
            Geometry.Point[] points = new Geometry.Point[IDs.Length];
            for (int i = 0; i < IDs.Length; i++)
            {
                points[i] = sketch.GetPointByID(IDs[i]);
            }
            int d = points[0].GetDimentions();
            if (points.Length != d + 1 || IDs.Length != d + 1)
                return null;
            double[,] C = new double[d + 1, d + 1];
            for (int i = 0; i < d + 1; i++)
            {
                C[i, 0] = 1;
                for (int j = 1; j < d + 1; j++)
                {
                    C[i, j] = points[i].point[j - 1];
                }
            }
            A = Determinant(C) / 2;

            C = ReverseMatrix(C);

            int pc = sketch.GetPointsCount();
            double[,] B = new double[d, pc];
            for (int i = 1; i < d + 1; i++)
            {
                int J = 0;
                for (int j = 0; j < pc; j++)
                {
                    if (IDs.Contains(j))
                    {
                        B[i - 1, j] = C[i, J];
                        J++;
                    }
                    else
                        B[i - 1, j] = 0;
                }
            }
            double[,] Bt = T(B);
            double[,] m = MatrixMultiply(Bt, B);
            double[,] k = MatrixMultiply(m, A);
            return k;
        }

        public double[,] StiffnessMatrix(    // изменить доступ
            int[] IDs          //   Номера точек элемента 
        )
        {
            Geometry.Point[] points = new Geometry.Point[IDs.Length];
            for (int i = 0; i < IDs.Length; i++)
            {
                points[i] = sketch.GetPointByID(IDs[i]);
            }
            int d = points[0].GetDimentions();
            if (points.Length != d + 1 || IDs.Length != d + 1)
                return null;
            double[,] C = new double[d + 1, d + 1];
            for (int i = 0; i < d + 1; i++)
            {
                C[i, 0] = 1;
                for (int j = 1; j < d + 1; j++)
                {
                    C[i, j] = points[i].point[j - 1];
                }
            }
            double A = Determinant(C) / 2;

            C = ReverseMatrix(C);

            int pc = sketch.GetPointsCount();
            double[,] B = new double[d, pc];
            for (int i = 1; i < d + 1; i++)
            {
                int J = 0;
                for (int j = 0; j < pc; j++)
                {
                    if (IDs.Contains(j))//!!!
                    {
                        B[i - 1, j] = C[i, J];
                        J++;
                    }
                    else
                        B[i - 1, j] = 0;
                }
            }
            double[,] Bt = T(B);
            double[,] m = MatrixMultiply(Bt, B);
            double[,] k = MatrixMultiply(m, A);
            return k;
        }

        //
        public double[,] StiffnessMatrix(    // изменить доступ
            int ElementNum          //   Номера точек элемента 
        )
        {
            Element element = sketch.GetElementByID(ElementNum);
            int d = element.GetDimentions();
            double[,] C = new double[d + 1, d + 1];
            for (int i = 0; i < d + 1; i++)
            {
                C[i, 0] = 1;
                for (int j = 1; j < d + 1; j++)
                {
                    C[i, j] = element.points[i].point[j - 1];
                }
            }
            element.A = Math.Abs(Determinant(C) / 2);

            C = ReverseMatrix(C);

            int pc = sketch.GetPointsCount();
            double[,] B = new double[d, pc];
            for (int i = 1; i < d + 1; i++)
            {
                int J = 0;
                for (int j = 0; j < pc; j++)
                {
                    if (element.points_Contains(j))
                    {
                        B[i - 1, j] = C[i, J];
                        J++;
                    }
                    else
                        B[i - 1, j] = 0;
                }
            }
            double[,] Bt = T(B);
            double[,] m = MatrixMultiply(Bt, B);
            double[,] k = MatrixMultiply(m, element.A);
            return k;
        }

        //  Вычисление глобальной матрицы жесткости
        public void ComputeGlobalStiffnessMatrix()
        {
            int pc = sketch.GetPointsCount();
            GlobalStiffnessMatrix = new double[pc, pc];
            int elCount = sketch.GetElementsCount();
            for (int i = 0; i < elCount; i++)
            {
                double[,] SM = StiffnessMatrix(i);
                GlobalStiffnessMatrix = AddMatrix(GlobalStiffnessMatrix, SM);
            }
        }

        // Вычисление глобальной матрицы минимизации потенциальной энергии
        public void ComputeGlobalFunction(double G, double TO)
        {
            int pc = sketch.GetPointsCount();
            GlobalFunction = new double[pc, 1];
            int elCount = sketch.GetElementsCount();
            for (int i = 0; i < elCount; i++)
            {
                double[,] RM = RotationFunction(G, TO, i);
                GlobalFunction = AddMatrix(GlobalFunction, RM);
            }
        }

        public double[] ComputeAll()
        {
            int ec = sketch.GetPointsCount();
            double[] a = new double[ec];
            double[,] SM = new double[ec,ec];
            double[,] GF = new double[ec,1];
            for (int i = 0; i < ec; i++)
            {
                for (int j = 0; j < ec; j++)
                {
                    SM[i, j] = GlobalStiffnessMatrix[i, j];
                }
                GF[i, 0] = GlobalFunction[i, 0];
            }
            for (int i = 0; i < ec; i++)
            {
                if(sketch.GetPointByID(i).Fixed)
                {
                    for (int j = 0; j < ec; j++)
                    {
                        if (i == j)
                            continue;
                        SM[i,j] = 0;
                    }
                    GF[i, 0] = SM[i, i] * GF[i, 0];
                    for (int j = 0; j < ec; j++)
                    {
                        GF[j, 0] -= SM[j, i] * GF[i, 0];                       
                        SM[j, i] = 0;
                    }
                }
                a[i] = GlobalFunction[i, 0];
            }
            double[] res = LinSystemGauss(SM, a);
            return res;
        }
    }

    internal class Program: MatrixMath.MatrixOperations
    {
        public static VectorU OneDimensional_Vector_simplex_element(
            double Xi,  //  Начало элемента
            double Xj,  //  Конец элемента
            VectorU Ui,
            VectorU Uj,
            double X    //  Искомая точка
        )
        {
            if (Ui.GetDimentions() != 1 || Uj.GetDimentions() != 1)
                return new VectorU(1);
            double[,] u;
            double[,] N = new double[1, 2];
            double L = Math.Abs(Xj - Xi);
            N[0, 0] = (Xj - X) / L;
            N[0, 1] = (X - Xi) / L;
            double[,] U = new double[2, 1];
            U[0, 0] = Ui.U[0];
            U[1, 0] = Ui.U[1];
            u = MatrixMultiply(N, U);
            return new VectorU(u[0, 0]);
        }


        public static double OneDimensional_simplex_element(
            double Fi,  //  Узловое значение в точке i
            double Fj,  //  Узловое значение в точке j
            double Xi,  //  Начало элемента
            double Xj,  //  Конец элемента
            double X    //  Искомая точка
        )
        {
            double fi;
            double L = Math.Abs(Xj - Xi);
            fi = ((Xj - X) / L) * Fi + ((X - Xi) / L) * Fj;
            return fi;
        }

        public static double TwoDimensional_simplex_element(
            double Fi,              //  Узловое значение в точке i
            double Fj,              //  Узловое значение в точке j
            double Fk,              //  Узловое значение в точке k
            double Xi, double Yi,   //  Координата узла
            double Xj, double Yj,   //  Координата узла
            double Xk, double Yk,   //  Координата узла
            double X, double Y      //  Искомая точка
        )
        {
            double fi = 0;
            double[,] x = {
                {1, Xi, Yi },
                {1, Xj, Yj },
                {1, Xk, Yk }
            };
            double A2 = 1 / DeterminantS(x);
   
            double a = Xj * Yk - Xk * Yj;
            double b = Yj - Yk;
            double c = Xk - Xj;
            fi += Fi * (a + b * X + c * Y);

            a = Xk * Yi - Yk*Xi;
            b = Yk - Yi;
            c = Xi - Xk;
            fi += Fj * (a + b * X + c * Y);
            
            a = Xi * Yj - Xj * Yi;
            b = Yi - Yj;
            c = Xj - Xi;
            fi += Fk * (a + b * X + c * Y);

            fi *= A2;
            return fi;
        }

        private class Point3D
        {
            public double[] Point = new double[3];
            public Point3D(double X = 0, double Y = 0, double Z = 0)
            {
                Point[0] = X;
                Point[1] = Y;
                Point[2] = Z;
            }
        }

        public static double ThreeDimensional_simplex_element(
            double Fi,                          //  Узловое значение в точке i
            double Fj,                          //  Узловое значение в точке j
            double Fk,                          //  Узловое значение в точке k
            double Fl,                          //  Узловое значение в точке l
            double Xi, double Yi, double Zi,    //  Координата узла
            double Xj, double Yj, double Zj,    //  Координата узла
            double Xk, double Yk, double Zk,    //  Координата узла
            double Xl, double Yl, double Zl,    //  Координата узла
            double X, double Y, double Z        //  Искомая точка
        )
        {
            double[,] cord = new double[1, 4];
            cord[0, 0] = 1;
            cord[0, 1] = X;
            cord[0, 2] = Y;
            cord[0, 3] = Z;
            double[,] C = new double[4, 4];
            Point3D[] t = new Point3D[4];
            t[0] = new Point3D(Xi, Yi, Zi);
            t[1] = new Point3D(Xj, Yj, Zj);
            t[2] = new Point3D(Xk, Yk, Zk);
            t[3] = new Point3D(Xl, Yl, Zl);
            for (int i = 0; i < C.GetLength(1); i++)
            {
                for (int j = 0; j < C.GetLength(0); j++)
                {
                    if (j == 0)
                    {
                        C[i, j] = 1;
                        continue;
                    }
                    C[i, j] = t[i].Point[j-1];
                }
            }
            //            C = ReverseMatrix(C);
            double[,] F = new double[4, 1];
            F[0, 0] = Fi;
            F[1, 0] = Fj;
            F[2, 0] = Fk;
            F[3, 0] = Fl;
            double[,] res = MatrixMultiply(cord, ReverseMatrix(C));
            res = MatrixMultiply(res, F);
            return res[0, 0];
        }

        public static double Simplex_element(
            Point[] points, //  Массив координат вершин
            double[] F,     //  Массив значений
            Point p         //  Искомая точка
        )
        {
            int d = p.GetDimentions();
            if (points.Length != F.Length || 
                F.Length != d+1)
                return -1;
            for (int i = 0; i < points.Length; i++)
                if (points[i].GetDimentions() != d)
                    return -1;
            double[,] X = new double[1, d + 1];
            X[0, 0] = 1;
            for (int i = 1; i < d + 1; i++)
                X[0, i] = p.point[i - 1];
            double[,] C = new double[d + 1, d + 1];
            for (int i = 0; i < d + 1; i++)
            {
                C[i, 0] = 1;
                for (int j = 1; j < d + 1; j++)
                {
                    C[i, j] = points[i].point[j - 1]; 
                }
            }
            double[,] Fi = new double[d + 1, 1];
            for (int i = 0; i < d + 1; i++)
                Fi[i, 0] = F[i];
            C = ReverseMatrix(C);
            double[,] res = MatrixMultiply(X, C);
            res = MatrixMultiply(res, Fi);
            return res[0,0];
        }

        public static Point[] LocalSystemOfCoordinate(Point[] points)
        {
            if(points == null || points.Length < 2)
                return null;
            int d = points[0].GetDimentions();
            if (d + 1 != points.Length)
                return null;
//            Point[] res = new Point[d + 1];
            Point center = new Point(d);
            for (int i = 0; i < d; i++)
            {
                for (int j = 0; j < d + 1; j++)
                {
                    center.point[i] += points[j].point[i];
                }
                center.point[i] /= (d + 1);
            }
            for (int i = 0; i < d + 1; i++)
                for (int j = 0; j < d; j++)
                    points[i].point[j] -= center.point[j]; 
            return points;
        }

        public static VectorU Vector_simplex_element(
            Point[] points,     //  Массив координат вершин
            VectorU[] vectors,  //  Массив векторов соответствующий вершинам
            Point p             //  Искомая точка
        )
        {
            //  Проверки                            (ДОПИЛИТИЬ!)
            if (points.Length != vectors.Length || points.Length == 0
                || vectors.Length == 0)
                return new VectorU(1);
            //  Считаем N
            int n = points[0].GetDimentions();  //  количество измерений
            double[] N = new double[n+1];
            double[,] N_temp; //    временный массив
            double[,] cord = new double[1, n+1];
            for (int i = 0; i < n+1; i++)
            {
                if (i == 0)
                    cord[0, i] = 1;
                else
                    cord[0, i] = p.point[i-1];
            }
            double[,] C = new double[n+1, n+1];
            for (int i = 0; i < n+1; i++)
            {
                for (int j = 0; j < n+1; j++)
                {
                    if (j == 0)
                        cord[j, i] = 1;
                    else
                        cord[j, i] = points[i].point[j - 1];
                }
            }
            C = ReverseMatrix(C);
            N_temp = MatrixMultiply(cord, C);
            for (int i = 0; i < n+1; i++)
            {
                N[i] = C[0, i];
            }
            N_temp = null;
            C = null;
            //  Получаем вектор
            VectorU vector = new VectorU(n);
            for (int i = 0; i < n + 1; i++)
            {
                vector.U[i] = 0;
                for (int j = 0; j < n*(n + 1); j++)
                {
                    if (j % n == i)
                        vector.U[i] += N[j / n] * vectors[i].U[j % n];
                }
            }
            return vector;
        }

        //  Матрица жесткости (локальная, для одного элемента)
        public static double[,] StiffnessMatrix(
            Point[] points     //  Массив координат вершин
        )
        {
            int d = points[0].GetDimentions();
            if (points.Length != d + 1)
                return null;
            double[,] C = new double[d + 1, d + 1];
            for (int i = 0; i < d + 1; i++)
            {
                C[i, 0] = 1;
                for (int j = 1; j < d + 1; j++)
                {
                    C[i, j] = points[i].point[j - 1];
                }
            }
            C = ReverseMatrix(C);

            double[,] B = new double[d, d + 1];
            for (int i = 1; i < d + 1; i++)
            {
                for (int j = 0; j < d + 1; j++)
                {
                    B[i - 1, j] = C[i, j];
                }
            }
            double[,] Bt = T(B);
            double[,] k = MatrixMultiply(B, Bt);
            return k;
        }

        private static void PrintArray(double[,] y)
        {
            for (int i = 0; i < y.GetLength(0); i++)
            {
                for (int j = 0; j < y.GetLength(1); j++)
                {
                    Console.Write(y[i, j] + "  ");
                }
                Console.Write(Environment.NewLine);
            }
        }

        private static void PrintArray(double[] y, bool vertical = true)
        {
            for (int i = 0; i < y.Length; i++)
            {
                if (vertical)
                    Console.WriteLine(y[i]);
                else
                    Console.Write(y[i] + "   ");
            }
        }

        static void Main(string[] args)
        {
            MKE test = new MKE();
            test.AddPoint(new Point(0, 0));
            test.AddPoint(new Point(0.25, 0));
            test.AddPoint(new Point(0.5, 0));
            test.AddPoint(new Point(0.25, 0.25));
            test.AddPoint(new Point(0.5, 0.25));
            test.AddPoint(new Point(0.5, 0.5));
            test.FixPoint(2);
            test.FixPoint(4);
            test.FixPoint(5);
            test.AddElement(new int[] { 0, 1, 3 });
            test.AddElement(new int[] { 1, 2, 4 });
            test.AddElement(new int[] { 1, 3, 4 });
            test.AddElement(new int[] { 3, 4, 5 });
            test.ComputeGlobalStiffnessMatrix();

            double G = 7.995 * Math.Pow(10, 6) / 100;
            test.ComputeGlobalFunction(G, Math.PI / 180);

            double[] y = test.ComputeAll();
            PrintArray(y);

            Console.WriteLine();
            Console.Write("Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}
