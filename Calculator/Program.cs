using System.Collections.Generic;

internal class Program
{
    private static bool isOperator(string name)
    {
        switch (name)
        {
            case "+":
            case "-":
            case "*":
            case "/":
            case "%":
                return true;
        }
        return false;
    }
    private static bool isValidKey(ConsoleKey keyid)
    {
        if (keyid >= ConsoleKey.NumPad0 && keyid <= ConsoleKey.NumPad9) return true;

        switch (keyid)
        {
            case ConsoleKey.Add:
            case ConsoleKey.Subtract:
            case ConsoleKey.Multiply:
            case ConsoleKey.Divide:
            case ConsoleKey.Backspace:
            case ConsoleKey.Decimal:
            case ConsoleKey.Enter:
            case ConsoleKey.D5: // module or % sign
                return true;
        }
        return false;
    }
    private static void Main(string[] args)
    {
        float tmp;
        string keylogs = "";

        List<string> keys = new List<string>();
        keys.Add("0");

        while (true)
        {

            Console.Clear();


            Console.WriteLine("Simple Calculator in C#\n");

            Console.Write("┌───────────────────\n");
            Console.Write($"│ {string.Join(" ", keys.Select(x => x.ToString()))}             \n");
            Console.Write("│──────────────────┐\n");
            Console.Write("│   AC  ()  %   /  │\n");
            Console.Write("│   7   8   9   x  │\n");
            Console.Write("│   4   5   6   -  │\n");
            Console.Write("│   1   2   3   +  │\n");
            Console.Write("│   0   .   C   =  │\n");
            Console.Write("└──────────────────┘\n");
            //Console.WriteLine($"\n\nKEYLOG : {keylogs}");
            //Console.WriteLine($"keys.Count : {keys.Count} \nkeys : {string.Join(" ", keys.Select(x => x.ToString()))}");

            ConsoleKeyInfo key = Console.ReadKey();
            keylogs += key.Key.ToString() + " ";
            if (!isValidKey(key.Key)) continue;

            if (key.Key == ConsoleKey.Decimal)
            {
                if(float.TryParse(keys.Last() , out tmp) && !keys.Last().Contains("."))
                {
                    keys[keys.Count - 1] += ".";
                }
                continue;
            }

            // to prevent more than one consecutive operators like ++ , 23 --/
            if (keys.Count > 0 && isOperator(keys.Last()) && isOperator(key.KeyChar.ToString()))
            {
                if (keys.Last() != key.KeyChar.ToString())
                    keys[keys.Count - 1] = key.KeyChar.ToString();
                continue;
            }

            if (key.Key == ConsoleKey.Enter)
            {
                // calculate the keys
                calculate(keys);
                continue;
            }

            // to delete the keys on backspace
            if (key.Key == ConsoleKey.Backspace)
            {

                if (keys.Count > 0) { 
                    if(keys.Last().Length == 0) keys.RemoveAt(keys.Count - 1);
                    else keys[keys.Count -1] = keys.Last().Substring(0, keys.Last().Length - 1);
                } 
                continue; // skip if screen is empty
            }

            if (keys.Count > 0 && // checking if the keys list is empty
                float.TryParse(key.KeyChar.ToString(), out tmp) && // checking if current input is numeric
                float.TryParse(keys.Last(), out tmp) // checking if last input was numeric
                )
                keys[keys.Count - 1] = keys.Last() + key.KeyChar.ToString();
            else keys.Add(key.KeyChar.ToString());
        }


    }

    private static void calculate(List<string> keys)
    {
        for (int i = 1; i < keys.Count - 1; i++)
        {
            string key = keys[i];

            if (isOperator(key))
            {
                float res = 0;
                float num1;
                float num2;
                float.TryParse((string)keys[i - 1], out num1);
                float.TryParse((string)keys[i + 1], out num2);

                switch (key)
                {
                    case "+": res = num1 + num2; break;
                    case "-": res = num1 - num2; break;
                    case "*": res = num1 * num2; break;
                    case "/": res = num1 / num2; break;
                    case "%": res = num1 % num2; break;
                }

                keys[i - 1] = res.ToString();
                keys.RemoveAt(i);
                keys.RemoveAt(i);
            }
        }
    }
}