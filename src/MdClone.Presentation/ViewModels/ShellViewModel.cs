using System.ComponentModel;
using Caliburn.Micro;
using JetBrains.Annotations;
using LogoFX.Client.Mvvm.ViewModel.Services;

namespace MdClone.Presentation.ViewModels
{
    [UsedImplicitly]
    public class ShellViewModel : Conductor<INotifyPropertyChanged>.Collection.AllActive
    {
        private readonly IViewModelCreatorService _viewModelCreatorService;

        public ShellViewModel(IViewModelCreatorService viewModelCreatorService)
        {
            _viewModelCreatorService = viewModelCreatorService;
        }

        protected override void OnActivate()
        {
            base.OnActivate();
        }

        public override string DisplayName
        {
            get => "MdClone";
            set { }
        }
    }
}