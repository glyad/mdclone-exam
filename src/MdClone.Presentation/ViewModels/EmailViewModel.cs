using System.Threading.Tasks;
using JetBrains.Annotations;
using LogoFX.Client.Mvvm.ViewModel.Extensions;
using MdClone.Model.Contracts;

namespace MdClone.Presentation.ViewModels
{
    [UsedImplicitly]
    public class EmailViewModel : EditableObjectViewModel<IEmailModel>
    {
        private readonly IDataService _dataService;

        public EmailViewModel(IEmailModel model, IDataService dataService) 
            : base(model)
        {
            _dataService = dataService;
        }

        private bool _isActiveSaving;

        public bool IsActiveSaving
        {
            get => _isActiveSaving;
            set => SetProperty(ref _isActiveSaving, value);
        }

        protected override async Task<bool> SaveMethod(IEmailModel model)
        {
            IsActiveSaving = true;
            while (IsActiveSaving)
            {
                await Task.Delay(100);
            }

            await _dataService.SendEmail(model);

            return true;
        }
    }
}