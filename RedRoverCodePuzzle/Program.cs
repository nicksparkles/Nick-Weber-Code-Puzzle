using System;

namespace RedRoverCodePuzzle
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Entry Point - with default arg value
            var stringToFormat = args.Length > 0 ?
                args[0]
                :
                "(id, name, email, type(id, name, customFields(c1, c2, c3)), externalId)";

            Console.WriteLine($"{Environment.NewLine}***Standard Format***");
            Console.WriteLine(StringHelper.FormatString(stringToFormat));

            Console.WriteLine($"{Environment.NewLine}***Alphabetical Format***");         
            Console.WriteLine(StringHelper.FormatString(stringToFormat, FormatOptions.Alphabetical));

        }
    }
}