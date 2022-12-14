using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;

namespace Integrals;


public static class FormulaBuilder
{
    private static readonly Dictionary<string, MethodInfo> _methods;

    private static readonly Dictionary<string, double> _constants;

    private static readonly Dictionary<string, Func<double, double>> _cache;

    static FormulaBuilder()
    {
        _cache = new()
        {
            { "sin(x)", x => Math.Sin(x) }
        };

        _constants = new()
        {
            { "pi", Math.PI },
            { "e", Math.E },
            { "tau", Math.Tau }
        };

        var typeMethods = typeof(FormulaBuilder)
            .GetMethods(BindingFlags.Static | BindingFlags.NonPublic);

        var mathMethods = typeof(Math)
            .GetMethods()
            .Where(mi => mi.Name != "Log")
            .Concat(typeMethods)
            .ToList();

        _methods = mathMethods
            .Where(mi => mi
                .GetParameters()
                .All(p => p.ParameterType == typeof(double)))
            .ToDictionary(mi => mi.Name.ToLower());

        var lnMethod = typeof(Math).GetMethod("Log", new Type[] { typeof(double) })!;
        var logNMethod = typeof(Math).GetMethod("Log", new Type[] { typeof(double), typeof(double) })!;

        _methods["+"] = _methods["add"];
        _methods["-"] = _methods["sub"];
        _methods["*"] = _methods["mul"];
        _methods["/"] = _methods["div"];
        _methods["%"] = _methods["mod"];
        _methods["^"] = _methods["pow"];
        _methods["log"] = logNMethod;
        _methods["ln"] = lnMethod;
    }


    public static bool TryBuildFormula(string input, [NotNullWhen(true)] out Func<double, double>? func)
    {
        if (_cache.TryGetValue(input, out func))
            return true;

        func = null;

        try
        {
            var expressionQueue = PostfixNotationExpression.ConvertToPostfixNotation(input);

            Stack<Expression> operands = new();

            var parameter = Expression.Parameter(typeof(double), "x");

            while (expressionQueue.Count > 0)
            {
                var token = expressionQueue.Dequeue();

                if (!_methods.TryGetValue(token, out var op))
                {
                    if (double.TryParse(token, out var num) || _constants.TryGetValue(token, out num))
                    {
                        var constant = Expression.Constant(num);

                        operands.Push(constant);

                        continue;
                    }

                    if (token == "x")
                    {
                        operands.Push(parameter);

                        continue;
                    }

                    return false;
                }


                var paramsCount = op.GetParameters().Length;

                if (operands.Count < paramsCount)
                    return false;


                var arguments = new List<Expression>();

                for (int i = 0; i < paramsCount; i++)
                {
                    var expression = operands.Pop();

                    arguments.Add(expression);
                }

                arguments.Reverse();

                var operation = Expression.Call(op, arguments);

                operands.Push(operation);
            }

            Expression totalExpression = operands.Pop();

            if (operands.Count > 0)
                return false;

            var lambda = Expression.Lambda<Func<double, double>>(totalExpression, parameter);

            func = lambda.Compile();

            _cache.TryAdd(input, func);

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }


#pragma warning disable IDE0051 // Удалите неиспользуемые закрытые члены
    private static double Add(double d1, double d2) => d1 + d2;

    private static double Sub(double d1, double d2) => d1 - d2;

    private static double Mul(double d1, double d2) => d1 * d2;

    private static double Div(double d1, double d2) => d1 / d2;

    private static double Mod(double d1, double d2) => d1 % d2;
#pragma warning restore IDE0051 // Удалите неиспользуемые закрытые члены
}
