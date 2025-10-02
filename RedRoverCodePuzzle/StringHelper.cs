using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedRoverCodePuzzle
{
    public enum FormatOptions
    {
        Standard,
        Alphabetical
    }

    public class Line
    {
        public string Token { get; }
        public int Depth { get; }

        public Line(string token, int depth)
        {
            Token = token;
            Depth = depth;
        }

        public override string ToString() =>
            $"{new string(' ', Depth * 2)}- {Token}";
    }

    public static class StringHelper
    {
        public static string FormatString(string? input, FormatOptions option = FormatOptions.Standard)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            var lines = new List<Line>();
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
                            lines.Add(new Line(parent, depth));
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

            if (option == FormatOptions.Alphabetical)
            {
                // Apply sorting in "the depths" per parent without losing nesting
                SortInTheDepths(lines, 0, lines.Count, 0);
            }

            return string.Join(Environment.NewLine, lines.Select(l => l.ToString()));
        }

        private static void AddLine(string token, int depth, List<Line> lines)
        {
            token = token.Trim();
            if (token.Length == 0) return;
            lines.Add(new Line(token, depth));
        }

        private static void SortInTheDepths(List<Line> lines, int start, int end, int depth)
        {
            // 1) Project out each depth grouping
            var groups =
                Enumerable.Range(start, end - start)
                    .Where(i => lines[i].Depth == depth)
                    .Select(i => (
                        key: lines[i].Token,
                        start: i,
                        end: FindGroupEnd(lines, i, end, depth)
                    ))
                    .ToList();

            if (groups.Count == 0) return;

            // 2) Sort the groups by their root token.
            var sortedGroups = groups.OrderBy(g => g.key, StringComparer.OrdinalIgnoreCase).ToList();

            // 3) Rebuild with groups in sorted order.
            var temp = new List<Line>(end - start);
            foreach (var g in sortedGroups)
                temp.AddRange(lines.GetRange(g.start, g.end - g.start));

            for (int k = 0; k < temp.Count; k++)
                lines[start + k] = temp[k];

            // 4) To understand recursion you must first understand recursion.
            int cursor = start;
            foreach (var group in sortedGroups)
            {
                int newGroupStart = cursor;
                int newGroupEnd = cursor + (group.end - group.start);
                // Children of this group are all lines deeper than 'depth' within the current group.
                SortInTheDepths(lines, newGroupStart + 1, newGroupEnd, depth + 1);
                cursor = newGroupEnd;
            }
        }
        
        private static int FindGroupEnd(List<Line> lines, int rootIndex, int limit, int depth)
        {
            int endOfGroup = rootIndex + 1;

            while (endOfGroup < limit && lines[endOfGroup].Depth > depth)
                endOfGroup++;

            return endOfGroup;
        }
    }
}