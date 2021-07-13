using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;
using System.Windows.Threading;
using JetBrains.Annotations;
using LogoFX.Client.Mvvm.Commanding;

namespace MdClone.Presentation.Shared.UIServices
{
    public interface INotificationService
    {
        void Show(string message);
    }

    [UsedImplicitly]
    internal sealed class NotificationService : INotificationService
    {
        internal sealed class NotificationItemViewModel
        {
            private readonly NotificationService _parent;
            private static readonly TimeSpan _showInterval = TimeSpan.FromSeconds(20);

            private readonly DispatcherTimer _timer;

            public NotificationItemViewModel(NotificationService parent, string message)
            {
                _parent = parent;
                Message = message;

                _timer = new DispatcherTimer(_showInterval, DispatcherPriority.Background, OnTimer, Dispatcher.CurrentDispatcher);
                _timer.Start();
            }

            private void OnTimer(object sender, EventArgs e)
            {
                RemoveItself();
            }

            [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
            public string Message { [UsedImplicitly] get; }

            private ICommand _closeCommand;

            public ICommand CloseCommand => _closeCommand ??= ActionCommand
                .Do(RemoveItself);

            private void RemoveItself()
            {
                _timer.Stop();
                _parent._items.Remove(this);
            }
        }

        private readonly ObservableCollection<NotificationItemViewModel> _items =
            new ObservableCollection<NotificationItemViewModel>();

        public void Show(string message)
        {
            _items.Insert(0, new NotificationItemViewModel(this, message));
        }

        public IEnumerable<NotificationItemViewModel> Items => _items;
    }
}