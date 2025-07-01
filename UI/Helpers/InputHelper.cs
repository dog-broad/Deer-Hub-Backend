namespace Deer_Hub_Backend.UI.Helpers
{
    public static class InputHelper
    {
        public static string Prompt(string label, bool required = true)
        {
            while (true)
            {
                Console.Write($"{label}: ");
                string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input) && !required)
                    return null;
                if (!required || !string.IsNullOrWhiteSpace(input))
                    return input.Trim();

                Console.WriteLine("This field is required.");
            }
        }

        public static int PromptInt(string label)
        {
            while (true)
            {
                Console.Write($"{label}: ");
                if (int.TryParse(Console.ReadLine(), out int result))
                    return result;

                Console.WriteLine("Invalid number.");
            }
        }

        public static bool PromptBool(string label)
        {
            while (true)
            {
                Console.Write($"{label} (y/n): ");
                string input = Console.ReadLine()?.Trim().ToLower();
                if (input == "y" || input == "yes")
                    return true;
                if (input == "n" || input == "no")
                    return false;
                Console.WriteLine("Please enter 'y' or 'n'.");
            }
        }

        public static DateTime PromptDate(string label)
        {
            while (true)
            {
                Console.Write($"{label} (YYYY-MM-DD): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime result))
                    return result;

                Console.WriteLine("Invalid date.");
            }
        }
    }
}
