using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using JetBrains.Annotations;
using LogoFX.Client.Mvvm.Commanding;
using LogoFX.Client.Mvvm.ViewModel.Extensions;
using LogoFX.Client.Mvvm.ViewModel.Services;
using LogoFX.Client.Mvvm.ViewModel.Shared;
using MdClone.Model.Contracts;

namespace MdClone.Presentation.ViewModels
{
    [UsedImplicitly]
    public class EmailViewModel : EditableObjectViewModel<IEmailModel>
    {
        private readonly IEmailService _emailService;
        private readonly IOpenFileService _openFileService;
        private readonly IMessageService _messageService;
        private readonly IViewModelCreatorService _viewModelCreatorService;

        public EmailViewModel(
            IEmailModel model,
            IEmailService emailService,
            IOpenFileService openFileService,
            IMessageService messageService,
            IViewModelCreatorService viewModelCreatorService)
            : base(model)
        {
            _emailService = emailService;
            _openFileService = openFileService;
            _messageService = messageService;
            _viewModelCreatorService = viewModelCreatorService;
        }

        private ICommand _attachCommand;

        public ICommand AttachCommand => _attachCommand ??= ActionCommand
            .When(() => !IsAttaching && Model.AttachedFiles.Count() <= 5)
            .Do(Attach)
            .RequeryOnPropertyChanged(this, () => IsAttaching)
            .RequeryOnCollectionChanged(Model.AttachedFiles as INotifyCollectionChanged);

        private async void Attach()
        {
            _openFileService.Filter = "All files (*.*)|*.*";
            _openFileService.Multiselect = false;
            _openFileService.Title = "Attach file";
            if (!_openFileService.DetermineFile())
            {
                return;
            }

            IsAttaching = true;
            var retVal = await _emailService.Attach(Model, _openFileService.File.FullName);
            if (retVal == null)
            {
                await _messageService.ShowAsync("File cannot be attached.", "Attach file", MessageButton.OK, MessageImage.Information);
            }
            IsAttaching = false;
        }

        private ICommand _detachCommand;
        public ICommand DetachCommand => _detachCommand ??= ActionCommand<IAttachedFile>
            .Do(attachedFile =>
            {
                _emailService.Detach(Model, attachedFile);
            });


        private RecipientsViewModel _to;

        public RecipientsViewModel To => _to ??=
            _viewModelCreatorService.CreateViewModel<IEmailRecipientsModel, RecipientsViewModel>(Model.To);

        private RecipientsViewModel _cc;

        public RecipientsViewModel Cc => _cc ??=
            _viewModelCreatorService.CreateViewModel<IEmailRecipientsModel, RecipientsViewModel>(Model.Cc);

        private bool _isActiveSaving;
        public bool IsActiveSaving
        {
            get => _isActiveSaving;
            set => SetProperty(ref _isActiveSaving, value);
        }

        private bool _isAttaching;
        public bool IsAttaching
        {
            get => _isAttaching;
            set => SetProperty(ref _isAttaching, value);
        }

        private bool _messageIsEmpty;

        public bool MessageIsEmpty
        {
            get => _messageIsEmpty;
            set => SetProperty(ref _messageIsEmpty, value);
        }

        protected override async Task<bool> SaveMethod(IEmailModel model)
        {
            bool allowSendMail = true;

            if (MessageIsEmpty)
            {
                var retVal = await _messageService.ShowAsync("Do you want to send mail without a message?", "Sending mail", MessageButton.YesNo, MessageImage.Question);
                allowSendMail = retVal == MessageResult.Yes;
            }

            if (!allowSendMail)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(Model.Subject))
            {
                var retVal = await _messageService.ShowAsync("Do you want to send mail without a subject?", "Sending mail", MessageButton.YesNo, MessageImage.Question);
                allowSendMail = retVal == MessageResult.Yes;
            }

            if (!allowSendMail)
            {
                return false;
            }

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