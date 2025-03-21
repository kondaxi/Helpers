using System.Reflection;

namespace Kondaxi.Helpers.Runnables
{
    internal static class AssemblyHelpers
    {
        public static IEnumerable<Type> GetRunnables(Assembly assembly)
        {
            return assembly
               .GetTypes()
               .Where(t => typeof(IRunnable).IsAssignableFrom(t) && !t.IsInterface)
               .OrderBy(t => t.FullName[GetBaseNamespaceEndIndex(assembly)..])
               .ToList();
        }

        public static int GetBaseNamespaceEndIndex(Assembly assembly) =>
            (assembly.GetName().Name?.Length ?? -1) + 1;
    }
}
