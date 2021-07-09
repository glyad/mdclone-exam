using System.Reflection;
using JetBrains.Annotations;
using MdClone.Data.Contracts.Providers;
using Solid.Practices.IoC;
using Solid.Practices.Modularity;

namespace MdClone.Data.Real.Providers
{
    [UsedImplicitly]
    internal sealed class Module : ICompositionModule<IDependencyRegistrator>
    {
        public void RegisterModule(IDependencyRegistrator dependencyRegistrator) => dependencyRegistrator
            .RegisterAutomagically(
                Assembly.LoadFrom(AssemblyInfo.AssemblyName),
                Assembly.GetExecutingAssembly());
    }
}