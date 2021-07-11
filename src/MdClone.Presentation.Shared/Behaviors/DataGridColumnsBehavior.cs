using System.Collections;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Interactivity;
using LogoFX.Core;

namespace MdClone.Presentation.Shared.Behaviors
{
    public class DataGridColumnsBehavior : Behavior<DataGrid>
    {
        public static readonly DependencyProperty ColumnsProperty =
            DependencyProperty.Register(
                "Columns",
                typeof(IEnumerable),
                typeof(DataGridColumnsBehavior),
                new PropertyMetadata(ColumnsPropertyChanged));

        private static void ColumnsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DataGridColumnsBehavior) d).ColumnsChanged((IEnumerable) e.NewValue, (IEnumerable) e.OldValue);
        }

        private void ColumnsChanged(IEnumerable newValue, IEnumerable oldValue)
        {
            //if (oldValue is INotifyCollectionChanged oldCollection)
            //{
            //    oldCollection.CollectionChanged -= ColumnsCollectionChanged;
            //}

            if (newValue is INotifyCollectionChanged newCollection)
            {
                newCollection.CollectionChanged += WeakDelegate.From(ColumnsCollectionChanged);
            }

            UpdateColumns(newValue);
        }

        private void UpdateColumns(IEnumerable columns)
        {
            AssociatedObject.Columns.Clear();
            var index = 0;
            foreach (var column in columns)
            {
                var dataGridColumn = new DataGridTextColumn
                {
                    Header = column,
                    Binding = new Binding($"Items[{index}].Value")
                };
                AssociatedObject.Columns.Add(dataGridColumn);
                index += 1;
            }
        }

        private void ColumnsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateColumns(Columns);
        }

        public IEnumerable Columns
        {
            get => (IEnumerable) GetValue(ColumnsProperty);
            set => SetValue(ColumnsProperty, value);
        }
    }
}