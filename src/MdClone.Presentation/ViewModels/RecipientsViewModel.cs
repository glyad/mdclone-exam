using System.Collections;
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

        private string _text;

        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        private WrappingCollection _recipients;

        public IEnumerable Recipients => _recipients ??= CreateRecipients();

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