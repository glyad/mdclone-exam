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
    public class ShellViewModel : Conductor<INotifyPropertyChanged>.Collection.OneActive
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
                ActivateItem(vm);
            });

        private ICommand _addTableCommand;

        public ICommand AddTableCommand => _addTableCommand ??= ActionCommand
            .When(() => true)
            .Do(() =>
            {
                var vm = _viewModelCreatorService.CreateViewModel<TableScreenViewModel>();
                ActivateItem(vm);
            });

        private ICommand _closeTabCommand;

        [UsedImplicitly]
        public ICommand CloseTabCommand => _closeTabCommand ??= ActionCommand<INotifyPropertyChanged>
            .Do(item =>
            {
                var index = Items.IndexOf(item);
                Items.RemoveAt(index);

                if (ActiveItem != null)
                {
                    return;
                }

                if (index >= Items.Count)
                {
                    index -= 1;
                }

                if (index >= 0)
                {
                    ActivateItem(Items[index]);
                }
            });


        public override string DisplayName
        {
            get => "MdClone";
            set { }
        }
    }
}