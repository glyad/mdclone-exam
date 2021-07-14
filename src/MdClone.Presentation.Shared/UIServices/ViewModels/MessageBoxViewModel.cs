using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;
using Caliburn.Micro;
using JetBrains.Annotations;
using LogoFX.Client.Mvvm.Commanding;
using LogoFX.Client.Mvvm.ViewModel.Services;
using LogoFX.Client.Mvvm.ViewModel.Shared;

namespace MdClone.Presentation.Shared.UIServices.ViewModels
{
	public partial class MessageBoxViewModel : Screen
    {

        private string _title;

        [UsedImplicitly]
        public string Title
        {
            get => _title;
            set
            {
                if (_title == value) return;
                _title = value;
                NotifyOfPropertyChange();
            }
        }


        private string _message;

        [UsedImplicitly]
        public string Message
        {
            get => _message;
            set
            {
                if (_message == value) return;
                _message = value;
                NotifyOfPropertyChange();
            }
        }

        private string _innerMessageDetails;

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public string InnerMessageDetails
        {
            get => _innerMessageDetails;
            set
            {
                if (_innerMessageDetails == value) return;
                _innerMessageDetails = value;
                NotifyOfPropertyChange();
            }
        }

        public bool IsCancelButtonVisible => Buttons == MessageButton.OKCancel || Buttons == MessageButton.YesNoCancel;
        public bool IsNoButtonVisible => Buttons == MessageButton.YesNoCancel || Buttons == MessageButton.YesNo;
        public bool IsOkButtonVisible => Buttons == MessageButton.OKCancel || Buttons == MessageButton.OK;
        public bool IsYesButtonVisible => Buttons == MessageButton.YesNo || Buttons == MessageButton.YesNoCancel;

        private MessageButton _buttons;
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        public MessageButton Buttons
        {
            get => _buttons;
            set
            {
                if (_buttons == value)
                {
                    return;
                }

                _buttons = value;
                NotifyOfPropertyChange();
                NotifyOfPropertyChange(() => IsCancelButtonVisible);
                NotifyOfPropertyChange(() => IsNoButtonVisible);
                NotifyOfPropertyChange(() => IsOkButtonVisible);
                NotifyOfPropertyChange(() => IsYesButtonVisible);
            }
        }

        private MessageImage _image;
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public MessageImage Image
        {
            get => _image;
            set
            {
                if (_image == value) return;
                _image = value;
                NotifyOfPropertyChange();
            }
        }


        private MessageResult _messageBoxResult;
       
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        public MessageResult MessageBoxResult
        {
            get => _messageBoxResult;
            set
            {
                if (_messageBoxResult == value) return;
                _messageBoxResult = value;
                NotifyOfPropertyChange();
            }
        }

        
        private bool _canBeClosedViaChrome;
        
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public bool CanBeClosedViaChrome
        {
            get => _canBeClosedViaChrome;
            set
            {
                if (_canBeClosedViaChrome == value) return;
                _canBeClosedViaChrome = value;
                NotifyOfPropertyChange();
            }
        }


        private ICommand _closeCommand;

        [UsedImplicitly]
        public ICommand CloseCommand =>
	        _closeCommand ??= ActionCommand.Do(() =>
	        {
		        MessageBoxResult = MessageResult.Cancel;
		        ScreenExtensions.TryDeactivate(this, true);
	        });


        private ICommand _okCommand;

        [UsedImplicitly]
        public ICommand OkCommand =>
	        _okCommand ??= ActionCommand.Do(() =>
	        {
		        MessageBoxResult = MessageResult.OK;
		        ScreenExtensions.TryDeactivate(this, true);
	        });


        private ICommand _cancelCommand;
        
        [UsedImplicitly]
        public ICommand CancelCommand =>
	        _cancelCommand ??= ActionCommand.Do(() =>
	        {
		        MessageBoxResult = MessageResult.Cancel;
		        ScreenExtensions.TryDeactivate(this, true);
	        });


        private ICommand _yesCommand;

        [UsedImplicitly]
        public ICommand YesCommand =>
	        _yesCommand ??= ActionCommand.Do(() =>
	        {
		        MessageBoxResult = MessageResult.Yes;
		        ScreenExtensions.TryDeactivate(this, true);
	        });


        private ICommand _noCommand;

        [UsedImplicitly]
        public ICommand NoCommand =>
	        _noCommand ??= ActionCommand.Do(() =>
	        {
		        MessageBoxResult = MessageResult.No;
		        ScreenExtensions.TryDeactivate(this, true);
	        });
    }
}