using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Interactivity;
using System.Windows.Markup;
using LogoFX.Core;
using MahApps.Metro.Controls;

namespace MdClone.Presentation.Shared.Behaviors
{
    public sealed class DataGridColumnsBehavior : Behavior<DataGrid>
    {
        private sealed class RowToIndexConverter : MarkupExtension, IValueConverter
        {
            static RowToIndexConverter _converter;

            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value is DataGridRow row)
                {
                    var dataGrid = row.TryFindParent<DataGrid>();
                    return dataGrid.Items.IndexOf(row.Item) + 1;
                } 
                
                return -1;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }

            public override object ProvideValue(IServiceProvider serviceProvider)
            {
                return _converter ?? (_converter = new RowToIndexConverter());
            }
        }

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

        private void AddIndexColumn()
        {
            var dataGridColumn = new DataGridTextColumn
            {
                Header = "#",
                CanUserSort = false,
                Binding = new Binding()
                {
                    RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(DataGridRow), 1),
                    Converter = new RowToIndexConverter()
                }
            };

            AssociatedObject.Columns.Add(dataGridColumn);
        }

        private void UpdateColumns(IEnumerable columns)
        {
            AssociatedObject.Columns.Clear();
            // AddIndexColumn();
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