namespace Integrals;

public static class PostfixNotationExpression
{
    private static readonly HashSet<string> _operators = new()
    {
        "(", ")", "+", "-", "*", "/", "%", "^"
    };


    static PostfixNotationExpression()
    {
        foreach (var method in typeof(Math).GetMethods())
            _operators.Add(method.Name.ToLower());

        _operators.Add("ln");
    }


    public static Queue<string> ConvertToPostfixNotation(string input)
    {
        List<string> outputSeparated = new();

        Stack<string> stack = new();

        foreach (string c in Separate(input))
        {
            if (string.IsNullOrWhiteSpace(c))
                continue;

            if (_operators.Contains(c))
            {
                if (stack.Count > 0 && !c.Equals("("))
                {
                    if (c.Equals(")"))
                    {
                        string s = stack.Pop();

                        while (s != "(")
                        {
                            outputSeparated.Add(s);
                            s = stack.Pop();
                        }
                    }
                    else if (GetPriority(c) > GetPriority(stack.Peek()))
                        stack.Push(c);
                    else
                    {
                        while (stack.Count > 0 && GetPriority(c) <= GetPriority(stack.Peek()))
                            outputSeparated.Add(stack.Pop());

                        stack.Push(c);
                    }
                }
                else
                    stack.Push(c);
            }
            else
                outputSeparated.Add(c);
        }

        if (stack.Count > 0)
            foreach (string c in stack)
                outputSeparated.Add(c);

        outputSeparated = outputSeparated.FindAll(s => !string.IsNullOrWhiteSpace(s));

        return new(outputSeparated);
    }


    private static IEnumerable<string> Separate(string input)
    {
        int pos = 0;

        while (pos < input.Length)
        {
            if (input[pos] == ',')
            {
                pos++;

                continue;
            }

            var s = input[pos].ToString();

            if (!_operators.Contains(s))
            {
                if (char.IsDigit(input[pos]))
                    for (int i = pos + 1; i < input.Length && (char.IsDigit(input[i]) || input[i] == ',' || input[i] == '.'); i++)
                        s += input[i];

                else if (char.IsLetter(input[pos]))
                    for (int i = pos + 1; i < input.Length && input[i] != ',' && (char.IsLetter(input[i]) || char.IsDigit(input[i])); i++)
                        s += input[i];
            }

            yield return s;

            pos += s.Length;
        }
    }

    private static byte GetPriority(string s) => s switch
    {
        "(" or ")" => 0,
        "+" or "-" => 1,
        "*" or "/" or "%" => 2,
        "^" => 3,
        _ => 4,
    };
}
