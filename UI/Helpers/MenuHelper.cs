namespace Deer_Hub_Backend.UI.Helpers
{
    public static class MenuHelper
    {
        public static int ShowMenu(string title, List<string> options)
        {
            Console.WriteLine($"=== {title.ToUpper()} ===");
            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"  {i + 1}. {options[i]}");
            }

            int choice;
            while (true)
            {
                Console.Write("\nSelect an option: ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out choice) && choice > 0 && choice <= options.Count)
                    break;

                Console.WriteLine("Invalid choice. Try again.");
            }

            return choice - 1;
        }
    }
}
