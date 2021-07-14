using System;
using System.Threading.Tasks;
using Caliburn.Micro;
using JetBrains.Annotations;
using LogoFX.Client.Mvvm.ViewModel.Services;
using LogoFX.Client.Mvvm.ViewModel.Shared;
using MdClone.Presentation.Shared.UIServices.ViewModels;

namespace MdClone.Presentation.Shared.UIServices
{
    [UsedImplicitly]
    internal sealed class MessageBoxService : IMessageService
    {
        private readonly IViewModelCreatorService _viewModelCreatorService;
        private readonly IWindowManager _windowManager;

        public MessageBoxService(IViewModelCreatorService viewModelCreatorService, IWindowManager windowManager)
        {
            _viewModelCreatorService = viewModelCreatorService;
            _windowManager = windowManager;
        }

        public MessageResult Show(string message, string caption = "Message", MessageButton button = MessageButton.OK, MessageImage icon = MessageImage.None)
        {
            return ShowMessageBox(message, caption, null, button, icon);
        }

        public Task<MessageResult> ShowAsync(string message, string caption = "Message", MessageButton button = MessageButton.OK, MessageImage icon = MessageImage.None)
        {
            return Task.Run(() => ShowMessageBox(message, caption, null, button, icon));
        }

        public MessageResult ShowError(Exception error, string caption = "Message")
        {
            var messageBoxText = string.Empty;

            messageBoxText = GetExceptionDetails(error, messageBoxText, out var innerMessage);

            return ShowMessageBox(messageBoxText, error.ToString(), innerMessage, MessageButton.OK, MessageImage.Error);
        }

        private MessageResult ShowMessageBox(string message, string caption, string innerMessage, MessageButton button, MessageImage icon)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException(nameof(message));
            }

            var vm = _viewModelCreatorService.CreateViewModel<MessageBoxViewModel>();
            vm.Message = message;
            vm.Title = caption;
            vm.Buttons = button;
            vm.Image = icon;
            vm.CanBeClosedViaChrome = true;
            vm.InnerMessageDetails = innerMessage;

            Execute.OnUIThread(() => _windowManager.ShowDialog(vm));

            // Dispatch.Current.OnUiThread(() => _windowManager.ShowDialog(vm));

            return vm.MessageBoxResult;
        }


        //the method exception tree as text and returns it. It returns the exception 
        private string GetExceptionDetails(Exception exp, string textMessage, out string details)
        {
            details = string.Empty;

            string retText = string.Empty;

            try
            {
                // Write Message tree of inner exception into textual representation
                retText = exp.Message;

                var innerEx = exp.InnerException;

                for (int i = 0; innerEx != null; i++, innerEx = innerEx.InnerException)
                {
                    string spaces = string.Empty;

                    for (int j = 0; j < i; j++)
                        spaces += "  ";

                    retText += "\n" + spaces + "└─>" + innerEx.Message;
                }

                // Label message tree with meaningful info: "Error while reading file X."
                if (textMessage != null)
                {
                    if (textMessage.Length > 0)
                    {
                        retText = $"{textMessage}\n\n{retText}";
                    }
                }

                // Write complete stack trace info into details section
                details = exp.ToString();
            }
            catch
            {
                // ignored
            }

            return retText;
        }
    }
}