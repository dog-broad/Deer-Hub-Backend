using System.Reflection;

namespace Deer_Hub_Backend.UI.Helpers
{
    public static class AsciiTableHelper
    {
        public static void PrintTable<T>(List<T> items)
        {
            if (items == null || items.Count == 0)
            {
                Console.WriteLine("No records found.");
                return;
            }

            var props = typeof(T).GetProperties();
            var headers = props.Select(p => p.Name).ToArray();

            int[] widths = headers.Select(h => h.Length).ToArray();

            foreach (var item in items)
            {
                for (int i = 0; i < props.Length; i++)
                {
                    var val = props[i].GetValue(item)?.ToString() ?? "";
                    widths[i] = Math.Max(widths[i], val.Length);
                }
            }

            string separator = "+" + string.Join("+", widths.Select(w => new string('-', w + 2))) + "+";
            Console.WriteLine(separator);
            Console.WriteLine("| " + string.Join(" | ", headers.Select((h, i) => h.PadRight(widths[i]))) + " |");
            Console.WriteLine(separator);

            foreach (var item in items)
            {
                Console.WriteLine("| " + string.Join(" | ", props.Select((p, i) => (p.GetValue(item)?.ToString() ?? "").PadRight(widths[i]))) + " |");
            }

            Console.WriteLine(separator);
        }
    }
}
