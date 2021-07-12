using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Input;
using Caliburn.Micro;
using JetBrains.Annotations;
using LogoFX.Client.Mvvm.Commanding;
using LogoFX.Client.Mvvm.Model.Contracts;
using LogoFX.Client.Mvvm.ViewModel.Extensions;
using LogoFX.Client.Mvvm.ViewModel.Services;
using MdClone.Model.Contracts;

namespace MdClone.Presentation.ViewModels
{
    [UsedImplicitly]
    public class EmailScreenViewModel : Conductor<EmailViewModel>
    {
        private readonly IDataService _dataService;
        private readonly IViewModelCreatorService _viewModelCreatorService;

        public EmailScreenViewModel(IDataService dataService, IViewModelCreatorService viewModelCreatorService)
        {
            _dataService = dataService;
            _viewModelCreatorService = viewModelCreatorService;
        }

        private ICommand _sendEmailCommand;

        [UsedImplicitly]
        public ICommand SendEmailCommand => _sendEmailCommand ??= ActionCommand<IEmailModel>
            .When(model => Editable && !IsEmailSending && !((IHaveErrors) model).HasErrors)
            .Do(SendEmail)
            .RequeryOnPropertyChanged(this, () => Editable)
            .RequeryOnPropertyChanged(this, () => IsEmailSending);

        private  void SendEmail(IEmailModel model)
        {
            ActiveItem.ApplyCommand.Execute(null);
        }

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

        protected override void OnActivate()
        {
            base.OnActivate();

            var emailModel = _dataService.CreateNewEmail();
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