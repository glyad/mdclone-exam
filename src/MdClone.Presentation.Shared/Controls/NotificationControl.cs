using System.Windows.Controls;
using System.Windows.Data;
using Caliburn.Micro;
using MdClone.Presentation.Shared.UIServices;

namespace MdClone.Presentation.Shared.Controls
{
    public class NotificationControl : ItemsControl
    {
        public NotificationControl()
        {
            var notificationService = IoC.Get<INotificationService>();
            SetBinding(ItemsSourceProperty, new Binding("Items") {Source = notificationService});
        }

        //protected override DependencyObject GetContainerForItemOverride()
        //{
        //    return new NotificationControlItem();
        //}
    }
}