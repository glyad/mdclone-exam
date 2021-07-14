using JetBrains.Annotations;
using LogoFX.Client.Mvvm.ViewModel.Services;
using MdClone.Presentation.Shared.UIServices;
using Solid.Practices.IoC;
using Solid.Practices.Modularity;

namespace MdClone.Presentation.Shared
{
    [UsedImplicitly]
    internal sealed class Module : ICompositionModule<IDependencyRegistrator>
    {
        public void RegisterModule(IDependencyRegistrator dependencyRegistrator)
        {
            dependencyRegistrator
                .AddSingleton<IBrowseFolderService, BrowseFolderService>()
                .AddSingleton<IOpenFileService, OpenFileService>()
                .AddSingleton<IMessageService, MessageBoxService>()
                .AddSingleton<INotificationService, NotificationService>();
        }
    }
}