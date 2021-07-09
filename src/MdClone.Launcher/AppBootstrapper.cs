using System;
using LogoFX.Client.Bootstrapping;
using LogoFX.Client.Bootstrapping.Adapters.SimpleContainer;
using MdClone.Presentation.ViewModels;
using Solid.Practices.Composition;

namespace MdClone.Launcher
{
    public sealed class AppBootstrapper : BootstrapperContainerBase<SimpleContainerAdapter>
        .WithRootObject<ShellViewModel>
    {
        private static readonly SimpleContainerAdapter _container = new SimpleContainerAdapter();

        public AppBootstrapper()
            : base(_container)
        {
        }

        public override CompositionOptions CompositionOptions => new CompositionOptions
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
