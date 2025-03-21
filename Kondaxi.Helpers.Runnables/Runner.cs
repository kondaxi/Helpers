using System.Reflection;

namespace Kondaxi.Helpers.Runnables
{
    public static class Runner
    {
        public static void Run(Assembly assembly)
        {
            Type[] runnables = AssemblyHelpers.GetRunnables(assembly).ToArray();

            while (true)
            {
                Console.WriteLine("Available runnable:");
                Console.WriteLine();

                for (int i = 0; i < runnables.Length; i++)
                {
                    Console.WriteLine($"{i}. {runnables[i].FullName[AssemblyHelpers.GetBaseNamespaceEndIndex(assembly)..]}");
                }

                Console.Write("\nSelect code to run: ");

                string? input = Console.ReadLine();

                if (input == "exit")
                {
                    Environment.Exit(0);
                }

                if (int.TryParse(input, out int selectedRunnableIndex))
                {
                    if (selectedRunnableIndex >= runnables.Length || selectedRunnableIndex < 0)
                    {
                        ExecuteWithColor(ConsoleColor.Red, () => Console.WriteLine("Selected index not valid."));
                    }
                    else
                    {
                        Type selectedRunnableType = runnables[selectedRunnableIndex];
                        ExecuteRunnableType(selectedRunnableType);
                    }
                }

                Type? runnableByName = runnables.FirstOrDefault(r => string.Equals(r.Name, input, StringComparison.OrdinalIgnoreCase));

                if (runnableByName is not null)
                {
                    ExecuteRunnableType(runnableByName);
                }
            }

            void ExecuteWithColor(ConsoleColor color, Action action)
            {
                try
                {
                    Console.ForegroundColor = color;
                    action();
                }
                finally
                {
                    Console.ResetColor();
                }
            }

            void ExecuteRunnableType(Type selectedRunnableType)
            {
                IRunnable? runnableInstance = (IRunnable?)Activator.CreateInstance(selectedRunnableType);

                ExecuteWithColor(ConsoleColor.Green, () => runnableInstance?.Run());

                Console.WriteLine();
                Console.Write("Pres any key to continue or ESC to exit.");
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Escape)
                {
                    Environment.Exit(0);
                }
                Console.Clear();
            }
        }
    }
}
