using System;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using LogoFX.Client.Mvvm.ViewModel;
using LogoFX.Client.Mvvm.ViewModel.Services;
using LogoFX.Core;
using MdClone.Model.Contracts;

namespace MdClone.Presentation.ViewModels
{
    [UsedImplicitly]
    public class RecipientsViewModel : ObjectViewModel<IEmailRecipientsModel>
    {
        private readonly IEmailService _emailService;
        private readonly IViewModelCreatorService _viewModelCreatorService;

        public RecipientsViewModel(
            IEmailRecipientsModel model,
            IEmailService emailService,
            IViewModelCreatorService viewModelCreatorService)
            : base(model)
        {
            if (model is {Items: {Length: > 0}})
            {
                _selectedRecipients.AddRange(model.Items.Select(x => _viewModelCreatorService.CreateViewModel<IEmailRecipientModel, EmailRecipientViewModel>(x)));
            }

            _selectedRecipients.CollectionChanged += RecipientsCollectionChanged;
            _emailService = emailService;
            _viewModelCreatorService = viewModelCreatorService;
        }

        public Func<string, object> StringToItemFunc
        {
            get
            {
                return address =>
                {
                    var model = _emailService.CreateNewEmailRecipient(address);
                    return _viewModelCreatorService.CreateViewModel<IEmailRecipientModel, EmailRecipientViewModel>(model);
                };
            }
        }

        private WrappingCollection _recipients;

        public IEnumerable Recipients => _recipients ??= CreateRecipients();

        [SuppressMessage("ReSharper", "CollectionNeverUpdated.Local")] 
        private readonly RangeObservableCollection<EmailRecipientViewModel> _selectedRecipients = new();
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

        private void RecipientsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Model.Items = SelectedRecipients.OfType<EmailRecipientViewModel>().Select(x => x.Model).ToArray();
        }

        private async void AddSource(WrappingCollection wc)
        {
            var recipients = await _emailService.GetRecipients();
            wc.AddSource(recipients);
        }
    }
}