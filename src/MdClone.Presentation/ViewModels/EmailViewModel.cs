using System.Threading.Tasks;
using JetBrains.Annotations;
using LogoFX.Client.Mvvm.ViewModel.Extensions;
using MdClone.Model.Contracts;

namespace MdClone.Presentation.ViewModels
{
    [UsedImplicitly]
    public class EmailViewModel : EditableObjectViewModel<IEmailModel>
    {
        private readonly IEmailService _emailService;

        public EmailViewModel(IEmailModel model, IEmailService emailService) 
            : base(model)
        {
            _emailService = emailService;
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

            await _emailService.SendEmail(model);

            return true;
        }
    }
}