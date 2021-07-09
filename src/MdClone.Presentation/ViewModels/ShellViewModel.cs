using System.ComponentModel;
using System.Windows.Input;
using Caliburn.Micro;
using JetBrains.Annotations;
using LogoFX.Client.Mvvm.Commanding;
using LogoFX.Client.Mvvm.ViewModel.Services;
using MdClone.Model.Contracts;

namespace MdClone.Presentation.ViewModels
{
    [UsedImplicitly]
    public class ShellViewModel : Conductor<INotifyPropertyChanged>.Collection.AllActive
    {
        private readonly IDataService _dataService;
        private readonly IViewModelCreatorService _viewModelCreatorService;

        public ShellViewModel(
            IDataService dataService,
            IViewModelCreatorService viewModelCreatorService)
        {
            _dataService = dataService;
            _viewModelCreatorService = viewModelCreatorService;
        }

        private ICommand _addEmailCommand;

        public ICommand AddEmailCommand => _addEmailCommand ??= ActionCommand
            .When(() => true)
            .Do(() =>
            {
                var model = _dataService.CreateNewEmail();
                var vm = _viewModelCreatorService.CreateViewModel<EmailScreenViewModel>();
                vm.ActivateItem(_viewModelCreatorService.CreateViewModel<IEmailModel, EmailViewModel>(model));
                Items.Add(vm);
            });

        private ICommand _addTableCommand;

        public ICommand AddTableCommand => _addTableCommand ??= ActionCommand
            .When(() => true)
            .Do(() =>
            {
                var model = _dataService.CreateNewTable();
                var vm = _viewModelCreatorService.CreateViewModel<ITableDataModel, TableScreenViewModel>(model);
                Items.Add(vm);
            });

        public override string DisplayName
        {
            get => "MdClone";
            set { }
        }
    }
}