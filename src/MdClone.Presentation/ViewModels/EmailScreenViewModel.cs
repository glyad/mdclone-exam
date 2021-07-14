using System;
using System.Globalization;
using System.Windows.Data;
using Caliburn.Micro;
using JetBrains.Annotations;
using LogoFX.Client.Mvvm.ViewModel.Extensions;
using LogoFX.Client.Mvvm.ViewModel.Services;
using MdClone.Model.Contracts;
using MdClone.Presentation.Shared.UIServices;

namespace MdClone.Presentation.ViewModels
{
    [UsedImplicitly]
    public class EmailScreenViewModel : Conductor<EmailViewModel>
    {
        private readonly IEmailService _emailService;
        private readonly IViewModelCreatorService _viewModelCreatorService;
        private readonly INotificationService _notificationService;

        public EmailScreenViewModel(
            IEmailService emailService, 
            IViewModelCreatorService viewModelCreatorService, 
            INotificationService notificationService)
        {
            _emailService = emailService;
            _viewModelCreatorService = viewModelCreatorService;
            _notificationService = notificationService;
        }

        //private ICommand _sendEmailCommand;

        //[UsedImplicitly]
        //public ICommand SendEmailCommand => _sendEmailCommand ??= ActionCommand<IEmailModel>
        //    .When(_ => Editable && !IsEmailSending)
        //    .Do(SendEmail)
        //    .RequeryOnPropertyChanged(this, () => Editable)
        //    .RequeryOnPropertyChanged(this, () => IsEmailSending);

        //private  void SendEmail(IEmailModel model)
        //{
        //    if (!ActiveItem.ApplyCommand.CanExecute(null))
        //    {
        //        _notificationService.Show("E-Mail cannot be send");
        //    }
        //    ActiveItem.ApplyCommand.Execute(null);
        //}

        private bool _editable = true;

        public bool Editable
        {
            get => _editable;
            private set => Set(ref _editable, value);
        }

        private bool _isEmailSending;

        public bool IsEmailSending
        {
            get => _isEmailSending;
            private set => Set(ref _isEmailSending, value);
        }

        public override string DisplayName
        {
            get => "E-Mail";
            set { }
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            var emailModel = _emailService.CreateNewEmail();
            var emailViewModel = _viewModelCreatorService.CreateViewModel<IEmailModel, EmailViewModel>(emailModel);
            ActivateItem(emailViewModel);
        }

        public override void ActivateItem(EmailViewModel item)
        {
            base.ActivateItem(item);
            item.Saving += OnSaving;
            item.Saved += OnSaved;
        }

        public override void DeactivateItem(EmailViewModel item, bool close)
        {
            item.Saving -= OnSaving;
            item.Saved -= OnSaved;
            base.DeactivateItem(item, close);
        }

        private void OnSaved(object sender, ResultEventArgs e)
        {
            Editable = false;
            IsEmailSending = false;

            _notificationService.Show($"E-Mail has sent.");
        }

        private void OnSaving(object sender, EventArgs e)
        {
            IsEmailSending = true;
        }
    }

    public sealed class EmailScreenContextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null && (bool) value ? "Edit" : "Readonly";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}