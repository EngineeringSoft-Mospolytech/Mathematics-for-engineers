using System.Windows;

namespace Integrals;

public static class IntegralMath
{
    public static double LeftRiemannSum(double a, double b, int n, Func<double, double> f, Action<double, double>? callback = null)
    {
        double h = Math.Abs(b - a) / n;

        var res = 0d;

        for (double x = a; x < b; x += h)
        {
            var y = f(x);

            callback?.Invoke(x, y);

            res += y;
        }

        return res * h;
    }
    
    public static double RightRiemannSum(double a, double b, int n, Func<double, double> f, Action<double, double>? callback = null)
    {
        double h = Math.Abs(b - a) / n;

        var res = 0d;

        for (double x = a + h; x < b; x += h)
        {
            var y = f(x);

            callback?.Invoke(x + h, y);

            res += y;
        }

        return res * h;
    }

    public static double MiddleRiemannSum(double a, double b, int n, Func<double, double> f, Action<double, double>? callback = null)
    {
        double h = Math.Abs(b - a) / n;

        var res = 0d;

        for (double x = a + h / 2; x < b; x += h)
        {
            var y = f(x);

            callback?.Invoke(x + h / 2, y);

            res += y;
        }

        return res * h;
    }

    public static double TrapezoidalRule(double a, double b, int n, Func<double, double> f, Action<double, double>? callback = null)
    {
        double h = Math.Abs(b - a) / n;

        double sum = f(a) + f(b);

        for (double x = a; x <= b; x += h)
        {
            var y = (f(x) + f(x + h)) / 2;

            sum += y;

            callback?.Invoke(x, y);
        }

        return sum * h;
    }

    public static double SimpsonsRule(double a, double b, int n, Func<double, double> f, Action<double, double>? callback = null)
    {
        double h = (b - a) / n;

        double sum = 0;
        var x = a;
        for (int step = 0; step < n; step++)
        {
            double x1 = a + step * h;
            double x2 = a + (step + 1) * h;

            var y = (f(x1) + 4.0 * f(0.5 * (x1 + x2)) + f(x2)) * (x2 - x1);

            sum += y;

            callback?.Invoke(x, y);

            x += h;
        }

        return sum / 6.0;
    }

    public static double MonteCarlo(double a, double b, int n, Func<double, double> f, Action<double, double>? callback = null)
    {
        double h = Math.Abs(b - a) / n;

        var yMax = double.MinValue;
        var yMin = double.MaxValue;

        for (double x = a; x < b; x += h)
        {
            var y = f(x);

            if (y > yMax)
                yMax = y;

            if (y < yMin)
                yMin = y;
        }

        var acc = 0.1;
        var res = 0d;

        for (var i = a; i < b;)
        {
            var leftBias = i - h / 2;
            var rightBias = i + h / 2;


            var x = Random.Shared.NextDouble() * (rightBias - leftBias) + leftBias;

            var y = Random.Shared.NextDouble() * (yMax - yMin) + yMin;

            var actualValue = Math.Abs(Math.Abs(y - f(x)));

            var expectedValue = Math.Abs(f(i));

            var calcError = Math.Abs(expectedValue) > 0.0001
                ? actualValue / expectedValue
                : actualValue;

            if (calcError <= acc)
            {
                res += y;

                i += h;

                callback?.Invoke(x, y);
            }
        }

        return (b - a) * res / n;
    }

}
