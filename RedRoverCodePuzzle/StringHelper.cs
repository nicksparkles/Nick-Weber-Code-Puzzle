using System;
using System.Collections.Generic;
using System.Text;

namespace RedRoverCodePuzzle
{
    public enum FormatOptions
    {
        Standard,
        Alphabetical
    }

    public static class StringHelper
    {
        public static string FormatString(string? input, FormatOptions formatOptions = FormatOptions.Standard)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            var lines = new List<string>();
            var sb = new StringBuilder();
            int depth = 0;

            foreach (char c in input)
            {
                switch (c)
                {
                    case '(':
                        var parent = sb.ToString().Trim();
                        sb.Clear();
                        if (parent.Length > 0)
                        {
                            AddLine(parent, depth, lines);
                            depth++;
                        }
                        break;

                    case ')':
                        AddLine(sb.ToString(), depth, lines);
                        sb.Clear();
                        if (depth > 0) depth--;
                        break;

                    case ',':
                        AddLine(sb.ToString(), depth, lines);
                        sb.Clear();
                        break;

                    default:
                        sb.Append(c);
                        break;
                }
            }

            AddLine(sb.ToString(), depth, lines);

            return string.Join(Environment.NewLine, lines);
        }
        
        private static void AddLine(string token, int depth, List<string> lines)
        {
            token = token.Trim();
            if (token.Length == 0) return;
            lines.Add($"{new string(' ', depth * 2)}- {token}");
        }
    }
}