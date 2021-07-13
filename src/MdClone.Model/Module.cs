using System.Reflection;
using AutoMapper;
using MdClone.Model.Contracts;
using MdClone.Model.Mappers;
using Solid.Practices.IoC;
using Solid.Practices.Modularity;

namespace MdClone.Model
{
    public class Module : ICompositionModule<IDependencyRegistrator>
    {
        public void RegisterModule(IDependencyRegistrator dependencyRegistrator)
        {
            dependencyRegistrator
                .RegisterAutomagically(
                    Assembly.LoadFrom(AssemblyInfo.AssemblyName),
                    Assembly.GetExecutingAssembly());

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = config.CreateMapper();
            dependencyRegistrator
                .AddInstance(mapper)
                .AddSingleton<EmailRecipientModelMapper>()
                .AddSingleton<EmailModelMapper>()
                .AddSingleton<FileModelMapper>()
                .AddSingleton<RowDataModelMapper>()
                .AddSingleton<TableDataModelMapper>();
        }
    }
}