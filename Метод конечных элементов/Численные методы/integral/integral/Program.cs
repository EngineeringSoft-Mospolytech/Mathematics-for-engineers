double f(double x)
{
	return Math.Pow(x, 2) / (Math.Pow(x, 2) + 1);
}

double riemann_sum(double n, double a, double b)
{
    double h = Math.Abs(b - a) / n;
    double f_sum = f(a);
    for (int i = 1; i < n; i++)
    {
        f_sum += f(a + i * h);
    }
    return h * f_sum;
}

double trapezoidal_rule(double n, double a, double b)
{
    double h = Math.Abs(b - a) / n;
    double f_sum = f(a) + f(b);
    for (int i = 1; i < n; i++)
    {
        f_sum += 2 * f(a + i * h);
    }
    return h * f_sum / 2;
}

double simpsons_rule(double n, double a, double b)
{
    double h = Math.Abs(b - a) / n;
    double f_sum = f(a) + f(b);
    for (int i = 1; i < n; i++)
    {
        f_sum += (2 + 2 * (i % 2)) * f(a + i * h);
    }
    return h * f_sum / 3;
}

double A = 0;
double B = 1 / Math.Sqrt(3);

double n = Convert.ToDouble(Console.ReadLine());

Console.WriteLine(riemann_sum(n, A, B)); // сумма Римана
Console.WriteLine(trapezoidal_rule(n, A, B)); // Правило трапеций
Console.WriteLine(simpsons_rule(n, A, B)); // правило Симпсона