using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using JetBrains.Annotations;

namespace MdClone.Presentation.Shared.Behaviors
{
    [UsedImplicitly]
    public class MoveWindowBehavior : Behavior<Window>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.MouseLeftButtonDown += AssociatedObject_MouseLeftButtonDown;
        }

        private void AssociatedObject_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AssociatedObject.DragMove();
        }
    }
}