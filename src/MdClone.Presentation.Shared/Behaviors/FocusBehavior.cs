using System;
using System.Windows;
using System.Windows.Interactivity;

namespace MdClone.Presentation.Shared.Behaviors
{
    public class FocusBehavior : Behavior<FrameworkElement>
    {
        #region Fields

        private static readonly Type _thisType = typeof(FocusBehavior);

        private readonly RoutedEventHandler _onLoaded;
        private readonly RoutedEventHandler _onUnloaded;
        private readonly RoutedEventHandler _onGotFocus;
        private readonly RoutedEventHandler _onLostFocus;

        #endregion

        #region Constructors

        public FocusBehavior()
        {
            _onLoaded = OnLoaded;
            _onUnloaded = OnUnload;
            _onGotFocus = OnGotFocus;
            _onLostFocus = OnLostFocus;
        }

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty IsFocusedProperty =
            DependencyProperty.Register(
                "IsFocused",
                typeof(bool),
                _thisType,
                new PropertyMetadata(
                    false,
                    IsFocusedChanged));

        public bool IsFocused
        {
            get { return (bool)GetValue(IsFocusedProperty); }
            set { SetValue(IsFocusedProperty, value); }
        }

        #endregion

        #region Private Members

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            AssociatedObject.Loaded -= _onLoaded;
            UpdateFocus();
        }

        private void OnUnload(object sender, RoutedEventArgs e)
        {
            Detach();
        }

        private static void IsFocusedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue == e.NewValue)
            {
                return;
            }

            ((FocusBehavior)d).IsFocusedChanged((bool)e.NewValue);
        }

        private void IsFocusedChanged(bool newValue)
        {
            if (AssociatedObject == null || !AssociatedObject.IsLoaded)
            {
                return;
            }

            if (newValue)
            {
                AssociatedObject.Focus();
            }
        }

        private void UpdateFocus()
        {
            IsFocusedChanged(IsFocused);
        }

        private void OnLostFocus(object sender, RoutedEventArgs e)
        {
            IsFocused = false;
        }

        private void OnGotFocus(object sender, RoutedEventArgs e)
        {
            IsFocused = true;
        }

        #endregion

        #region Overrides

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.Unloaded += _onUnloaded;
            AssociatedObject.GotFocus += _onGotFocus;
            AssociatedObject.LostFocus += _onLostFocus;

            if (AssociatedObject.IsLoaded)
            {
                UpdateFocus();
            }
            else
            {
                AssociatedObject.Loaded += _onLoaded;
            }
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Unloaded -= _onUnloaded;
            AssociatedObject.GotFocus -= _onGotFocus;
            AssociatedObject.LostFocus -= _onLostFocus;

            base.OnDetaching();
        }

        #endregion
    }
}