using LogoFX.Client.Bootstrapping;
using LogoFX.Client.Mvvm.Commanding;
using LogoFX.Client.Mvvm.ViewModelFactory.SimpleContainer;

namespace MdClone.Launcher
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public App()
        {
            var bootstrapper =
                new AppBootstrapper()
                    .UseResolver()
                    .UseCommanding()
                    .UseViewModelFactory();

            bootstrapper.Initialize();
        }
    }
}
