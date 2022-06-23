namespace Integrals;

public static class IntegralMath
{
    public static double RiemannSum(double a, double b, double n, Func<double, double> f, Action<double, double>? callback = null)
    {
        double h = Math.Abs(b - a) / n;

        double sum = f(a);

        var x = 0d;

        for (int i = 1; i < n; i++)
        {
            var y = f(a + i * h);

            sum += y;

            x += h;

            callback?.Invoke(x, y);
        }

        return h * sum;
    }

    public static double TrapezoidalRule(double a, double b, double n, Func<double, double> f, Action<double, double>? callback = null)
    {
        double h = Math.Abs(b - a) / n;

        double sum = f(a) + f(b);

        var x = 0d;

        for (int i = 1; i < n; i++)
        {
            var y = 2 * f(a + i * h);

            sum += y;

            x += h;

            callback?.Invoke(x, y);
        }

        return h * sum / 2;
    }

    public static double SimpsonsRule(double a, double b, double n, Func<double, double> f, Action<double, double>? callback = null)
    {
        double h = Math.Abs(b - a) / n;

        double sum = f(a) + f(b);

        var x = 0d;

        for (int i = 1; i < n; i++)
        {
            var y = (2 + 2 * (i % 2)) * f(a + i * h);

            sum += y;

            x += h;

            callback?.Invoke(x, y);
        }

        return h * sum / 3;
    }
}
