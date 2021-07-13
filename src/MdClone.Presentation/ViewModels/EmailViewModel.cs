using System.Threading.Tasks;
using JetBrains.Annotations;
using LogoFX.Client.Mvvm.ViewModel.Extensions;
using LogoFX.Client.Mvvm.ViewModel.Services;
using MdClone.Model.Contracts;

namespace MdClone.Presentation.ViewModels
{
    [UsedImplicitly]
    public class EmailViewModel : EditableObjectViewModel<IEmailModel>
    {
        private readonly IEmailService _emailService;
        private readonly IViewModelCreatorService _viewModelCreatorService;

        public EmailViewModel(
            IEmailModel model,
            IEmailService emailService,
            IViewModelCreatorService viewModelCreatorService)
            : base(model)
        {
            _emailService = emailService;
            _viewModelCreatorService = viewModelCreatorService;
        }

        private RecipientsViewModel _to;

        public RecipientsViewModel To => _to ??=
            _viewModelCreatorService.CreateViewModel<IEmailRecipientModel[], RecipientsViewModel>(Model.To);

        private RecipientsViewModel _cc;

        public RecipientsViewModel Cc => _cc ??=
            _viewModelCreatorService.CreateViewModel<IEmailRecipientModel[], RecipientsViewModel>(Model.Cc);

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