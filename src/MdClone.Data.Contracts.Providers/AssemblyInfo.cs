using System.Reflection;

namespace MdClone.Data.Contracts.Providers
{
    public static class AssemblyInfo
    {
        public static string AssemblyName { get; } = $"{Assembly.GetExecutingAssembly().GetName().Name}.dll";
    }
}