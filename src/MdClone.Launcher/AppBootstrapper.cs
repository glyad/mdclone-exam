using System;
using LogoFX.Client.Bootstrapping;
using LogoFX.Client.Bootstrapping.Adapters.SimpleContainer;
using MdClone.Presentation.ViewModels;
using Solid.Practices.Composition;

namespace MdClone.Launcher
{
    public sealed class AppBootstrapper : BootstrapperContainerBase<ExtendedSimpleContainerAdapter>
        .WithRootObject<ShellViewModel>
    {
        private static readonly ExtendedSimpleContainerAdapter _container = new();

        public AppBootstrapper()
            : base(_container)
        {
        }

        public override CompositionOptions CompositionOptions => new()
        {
            Prefixes = new[]
            {
                "MdClone"
            }
        };

        protected override void OnExit(object sender, EventArgs e)
        {
            base.OnExit(sender, e);
            _container?.Dispose();
        }
    }
}
