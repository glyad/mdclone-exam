using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using LogoFX.Client.Mvvm.ViewModel;
using LogoFX.Client.Mvvm.ViewModel.Services;
using MdClone.Model.Contracts;

namespace MdClone.Presentation.ViewModels
{
    [UsedImplicitly]
    public class RecipientsViewModel : ObjectViewModel<IEmailRecipientModel[]>
    {
        private readonly IEmailService _emailService;
        private readonly IViewModelCreatorService _viewModelCreatorService;

        public RecipientsViewModel(
            IEmailRecipientModel[] model,
            IEmailService emailService,
            IViewModelCreatorService viewModelCreatorService)
            : base(model)
        {
            _emailService = emailService;
            _viewModelCreatorService = viewModelCreatorService;
        }

        private WrappingCollection _recipients;

        public IEnumerable Recipients => _recipients ??= CreateRecipients();

        private List<EmailRecipientViewModel> _selectedRecipients = new List<EmailRecipientViewModel>();
        public IEnumerable SelectedRecipients => _selectedRecipients;

        private WrappingCollection CreateRecipients()
        {
            var result = new WrappingCollection
            {
                FactoryMethod = o =>
                    _viewModelCreatorService.CreateViewModel<IEmailRecipientModel, EmailRecipientViewModel>(
                        (IEmailRecipientModel) o)
            };

            AddSource(result);

            return result;
        }

        private async void AddSource(WrappingCollection wc)
        {
            var recipients = await _emailService.GetRecipients();
            wc.AddSource(recipients);
        }
    }
}