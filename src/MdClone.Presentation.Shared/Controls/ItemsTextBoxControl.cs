using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Caliburn.Micro;
using LogoFX.Client.Mvvm.Commanding;
using MahApps.Metro.Controls;

namespace MdClone.Presentation.Shared.Controls
{
    [TemplatePart(Name = "PART_Popup", Type = typeof(Popup))]
    [TemplatePart(Name = "PART_ListBox", Type = typeof(ListBox))]
    [TemplatePart(Name = "PART_SelectedItems", Type = typeof(ItemsControl))]
    public class ItemsTextBoxControl : ItemsControl
    {
        #region Nested Types

        #region Item

        public abstract class Item : PropertyChangedBase
        {

        }

        #endregion

        #region ObjectItem

        public sealed class ObjectItem : Item
        {
            #region Constructors

            public ObjectItem(object value)
            {
                Value = value;
            }

            #endregion

            #region Events

            public event EventHandler Removed = delegate { };

            #endregion

            #region Commands

            private ICommand _removeCommand;

            public ICommand RemoveCommand
            {
                get
                {
                    return _removeCommand ??
                           (_removeCommand = ActionCommand
                               .Do(() => Removed(this, EventArgs.Empty)));
                }
            }

            #endregion

            #region Public Properties

            public object Value { get; private set; }

            #endregion
        }

        #endregion

        #region TextEventArgs

        public sealed class TextEventArgs : EventArgs
        {
            public string Text { get; private set; }

            public TextEventArgs(string text)
            {
                Text = text;
            }
        }

        #endregion

        #region TextItem

        public sealed class TextItem : Item
        {
            #region Events

            public event EventHandler<KeyEventArgs> KeyDown = delegate { };

            public event EventHandler<KeyEventArgs> PreviewKeyDown = delegate { };

            public event EventHandler<TextEventArgs> TextChanged = delegate { };

            public event EventHandler ActualHeightChanged = delegate { };

            #endregion

            #region Commands

            private ICommand _keyDownCommand;

            public ICommand KeyDownCommand
            {
                get
                {
                    return _keyDownCommand ??
                           (_keyDownCommand = ActionCommand<KeyEventArgs>
                               .Do(args => KeyDown(this, args)));
                }
            }

            private ICommand _previewKeyDownCommand;

            public ICommand PreviewKeyDownCommand
            {
                get
                {
                    return _previewKeyDownCommand ??
                           (_previewKeyDownCommand = ActionCommand<KeyEventArgs>
                               .Do(args => PreviewKeyDown(this, args)));
                }
            }


            #endregion

            #region Public Properties

            private string _text;

            public string Text
            {
                get { return _text; }
                set
                {
                    if (_text == value)
                    {
                        return;
                    }

                    _text = value;
                    NotifyOfPropertyChange();
                    TextChanged(this, new TextEventArgs(_text));
                }
            }

            private bool _isFocused;

            public bool IsFocused
            {
                get { return _isFocused; }
                set
                {
                    if (_isFocused == value)
                    {
                        return;
                    }

                    _isFocused = value;
                    NotifyOfPropertyChange();
                }
            }

            private string _watermark;

            public string Watermark
            {
                get { return _watermark; }
                set
                {
                    if (_watermark == value)
                    {
                        return;
                    }

                    _watermark = value;
                    NotifyOfPropertyChange();
                }
            }

            private double _actualHeight;

            public double ActualHeight
            {
                get { return _actualHeight; }
                set
                {
                    if (value.Equals(_actualHeight))
                    {
                        return;
                    }

                    _actualHeight = value;
                    NotifyOfPropertyChange();
                    ActualHeightChanged(this, EventArgs.Empty);
                }
            }

            #endregion
        }

        #endregion

        #endregion

        #region Fields

        private static readonly Type _thisType = typeof(ItemsTextBoxControl);

        private readonly TextItem _textItem;

        private FrameworkElement _textControl;
        private Popup _popup;
        private ListBox _listBox;
        private ItemsControl _selectedItems;

        private readonly ObservableCollection<Item> _innerItems;

        #endregion

        #region Constructors

        public ItemsTextBoxControl()
        {
            _innerItems = new ObservableCollection<Item>();
            _textItem = new TextItem();
            _textItem.KeyDown += OnTextBoxKeyDown;
            _textItem.TextChanged += OnTextChanged;
            _textItem.PreviewKeyDown += OnTextBoxPreviewKeyDown;
            _textItem.ActualHeightChanged += OnTextControlActualHeightChanged;
            _innerItems.Add(_textItem);
        }

        ~ItemsTextBoxControl()
        {
            _textItem.KeyDown -= OnTextBoxKeyDown;
            _textItem.TextChanged -= OnTextChanged;
            _textItem.PreviewKeyDown -= OnTextBoxPreviewKeyDown;
            _textItem.ActualHeightChanged -= OnTextControlActualHeightChanged;
        }

        static ItemsTextBoxControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(_thisType, new FrameworkPropertyMetadata(_thisType));
        }

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty AutocompleteItemsSourceProperty =
            DependencyProperty.Register(
                "AutocompleteItemsSource",
                typeof(IEnumerable),
                _thisType);

        public static readonly DependencyProperty WatermarkProperty =
            DependencyProperty.Register(
                "Watermark",
                typeof(string),
                _thisType,
                new PropertyMetadata(
                    string.Empty,
                    WatermarkChanged));

        private static void WatermarkChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ItemsTextBoxControl)d).UpdateWatermark();
        }

        public static readonly DependencyProperty AutogenerateItemsProperty =
            DependencyProperty.Register(
                "AutogenerateItems",
                typeof(bool),
                _thisType,
                new PropertyMetadata(false));

        #endregion

        #region Public Properties

        public IEnumerable AutocompleteItemsSource
        {
            get { return (IEnumerable)GetValue(AutocompleteItemsSourceProperty); }
            set { SetValue(AutocompleteItemsSourceProperty, value); }
        }

        public string Watermark
        {
            get { return (string)GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }

        public bool AutogenerateItems
        {
            get { return (bool)GetValue(AutogenerateItemsProperty); }
            set { SetValue(AutogenerateItemsProperty, value); }
        }

        public Func<object, string> ItemToStringFunc = o => o.ToString();

        public Func<string, object> StringToItemFunc = o => o;

        #endregion

        #region Private Members

        private void OnTextControlActualHeightChanged(object sender, EventArgs e)
        {
            _popup.VerticalOffset = ((TextItem)sender).ActualHeight;
        }

        private void OnTextBoxPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down && _listBox.Items.Count > 0 && !(e.OriginalSource is ListBoxItem))
            {
                _listBox.Focus();
                _listBox.SelectedIndex = 0;
                var lbi = (ListBoxItem)_listBox.ItemContainerGenerator.ContainerFromIndex(_listBox.SelectedIndex);
                lbi.Focus();
                e.Handled = true;
                return;
            }

            var list = (IList)ItemsSource;
            if (e.Key == Key.Back && list.Count > 0 && string.IsNullOrEmpty(_textItem.Text))
            {
                object value = list[list.Count - 1];
                list.Remove(value);
                if (!(list is INotifyCollectionChanged))
                {
                    var item = _innerItems.OfType<ObjectItem>().Single(x => x.Value == value);
                    RemoveInnerItem(item);
                }
            }
        }

        private void OnTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                _popup.IsOpen = false;

                if (AutogenerateItems)
                {
                    var str = _textItem.Text;
                    var item = StringToItemFunc(str);
                    MoveToItems(item);
                }
            }
        }

        private void MoveToItems(object item)
        {
            ((IList)ItemsSource).Add(item);
            if (!(ItemsSource is INotifyCollectionChanged))
            {
                AddInnerItem(item);
            }
            _textItem.Text = string.Empty;
            _textItem.IsFocused = true;
        }

        private void OnRemoved(object sender, EventArgs e)
        {
            var item = (ObjectItem)sender;

            ((IList)ItemsSource).Remove(item.Value);
            if (!(ItemsSource is INotifyCollectionChanged))
            {
                RemoveInnerItem(item);
            }
        }

        private bool Test(object obj, string str)
        {
            if (Items.Contains(obj))
            {
                return false;
            }

            return ItemToStringFunc(obj).ToLower().StartsWith(str);
        }

        private void OnTextChanged(object sender, TextEventArgs e)
        {
            string lower = e.Text.ToLower();
            if (lower.Length == 0 || AutocompleteItemsSource == null)
            {
                _popup.IsOpen = false;
                return;
            }

            _popup.IsOpen = lower.Length > 0 && AutocompleteItemsSource.OfType<object>().Any(x => Test(x, lower));

            if (_popup.IsOpen)
            {
                _listBox.Items.Filter = o => !((IList)ItemsSource).Contains(o) && Test(o, lower);
            }
        }

        private void OnListBoxPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                ListBoxItem listBoxItem = ((DependencyObject)e.OriginalSource).TryFindParent<ListBoxItem>();

                if (listBoxItem != null)
                {
                    MoveToItems(listBoxItem.DataContext);
                    _popup.IsOpen = false;
                    e.Handled = true;
                }
            }
        }

        private void OnListBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.OriginalSource is ListBoxItem)
            {
                var tb = e.OriginalSource as ListBoxItem;
                if (e.Key == Key.Enter)
                {
                    _popup.IsOpen = false;
                    // ReSharper disable once PossibleNullReferenceException
                    MoveToItems(tb.DataContext);
                }
            }
        }

        private void RemoveInnerItem(int index)
        {
            RemoveInnerItem(_innerItems[index] as ObjectItem);
        }

        private void RemoveInnerItem(ObjectItem item)
        {
            item.Removed -= OnRemoved;
            _innerItems.Remove(item);
        }

        private void AddInnerItem(object value)
        {
            var objectItem = new ObjectItem(value);
            objectItem.Removed += OnRemoved;
            _innerItems.Insert(_innerItems.Count - 1, objectItem);
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var obj in e.NewItems)
                    {
                        AddInnerItem(obj);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (var obj in e.OldItems)
                    {
                        var item = _innerItems.OfType<ObjectItem>().Single(x => x.Value == obj);
                        RemoveInnerItem(item);
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    while (_innerItems.Count > 1)
                    {
                        RemoveInnerItem(0);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            UpdateWatermark();
        }

        private void UpdateWatermark()
        {
            _textItem.Watermark = _innerItems.Count > 1 ? string.Empty : Watermark;
        }

        #endregion

        #region Overrides

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var root = (UIElement)Template.FindName("Root", this);
            root.GotFocus += Root_GotFocus;

            _selectedItems = (ItemsControl)Template.FindName("PART_SelectedItems", this);
            _selectedItems.ItemContainerGenerator.StatusChanged += ItemContainerGenerator_StatusChanged;
            _selectedItems.ItemsSource = _innerItems;
            _selectedItems.ApplyTemplate();

            _popup = (Popup)Template.FindName("PART_Popup", this);

            _listBox = (ListBox)Template.FindName("PART_ListBox", this);
            _listBox.KeyDown += OnListBoxKeyDown;
            _listBox.PreviewMouseDown += OnListBoxPreviewMouseDown;
        }

        private void Root_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void ItemContainerGenerator_StatusChanged(object sender, EventArgs e)
        {
            ItemContainerGenerator itemContainerGenerator = (ItemContainerGenerator)sender;
            if (itemContainerGenerator.Status != GeneratorStatus.ContainersGenerated)
            {
                return;
            }

            itemContainerGenerator.StatusChanged -= ItemContainerGenerator_StatusChanged;

            _textControl = (FrameworkElement)_selectedItems.ItemContainerGenerator.ContainerFromItem(_textItem);
            _popup.PlacementTarget = _textControl;
            _popup.Placement = PlacementMode.Relative;
        }

        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            var oldCollection = oldValue as INotifyCollectionChanged;
            if (oldCollection != null)
            {
                oldCollection.CollectionChanged -= OnCollectionChanged;
            }

            while (_innerItems.Count > 1)
            {
                RemoveInnerItem(0);
            }

            var newCollection = newValue as INotifyCollectionChanged;
            if (newCollection != null)
            {
                newCollection.CollectionChanged += OnCollectionChanged;
            }

            if (newValue != null)
            {
                foreach (var obj in newValue)
                {
                    AddInnerItem(obj);
                }
            }

            UpdateWatermark();
        }

        protected override async void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            await Task.Delay(100);
            _textItem.IsFocused = true;
        }

        //protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        //{
        //    if (_popup.IsOpen)
        //    {
        //        _popup.IsOpen = false;
        //        e.Handled = true;
        //    }
        //    base.OnMouseLeftButtonUp(e);
        //}

        #endregion
    }
}