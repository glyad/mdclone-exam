using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Interactivity;

namespace MdClone.Presentation.Shared.Behaviors
{
    public class RichTextBoxBehavior : Behavior<RichTextBox>
    {
        private bool _preventEvents;

        public static readonly DependencyProperty RichDataProperty =
            DependencyProperty.Register(
                "RichData", 
                typeof(byte[]), 
                typeof(RichTextBoxBehavior), 
                new PropertyMetadata(RichDataPropertyChanged));

        private static void RichDataPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((RichTextBoxBehavior) d).RichDataChanged((byte[]) e.NewValue, (byte[]) e.OldValue);
        }

        [SuppressMessage("ReSharper", "UnusedParameter.Local")]
        private void RichDataChanged(byte[] newValue, byte[] oldValue)
        {
            if (_preventEvents)
            {
                return;
            }

            LoadData(newValue);
        }

        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register(
                "IsActive",
                typeof(bool),
                typeof(RichTextBoxBehavior),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, IsActivePropertyChanged));

        private static void IsActivePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((RichTextBoxBehavior) d).IsActiveChanged((bool) e.NewValue, (bool) e.OldValue);
        }

        private async void IsActiveChanged(bool newValue, bool oldValue)
        {
            if (!oldValue && newValue)
            {
                SaveData();
                await Task.Delay(100);
                IsActive = false;
            }
        }

        public bool IsActive
        {
            get => (bool) GetValue(IsActiveProperty);
            set => SetValue(IsActiveProperty, value);
        }

        private void LoadData(byte[] newValue)
        {
            if (newValue == null || newValue.Length == 0)
            {
                AssociatedObject.Document.Blocks.Clear();
                return;
            }

            using (var stream = new MemoryStream(newValue))
            {
                AssociatedObject.Selection.Load(stream, DataFormats.Rtf);
            }
        }

        private void SaveData()
        {
            var range = new TextRange(AssociatedObject.Document.ContentStart, AssociatedObject.Document.ContentEnd);

            using (var stream = new MemoryStream())
            {
                range.Save(stream, DataFormats.Rtf);
                _preventEvents = true;
                RichData = stream.ToArray();
                _preventEvents = false;
            }
        }

        public byte[] RichData
        {
            get => (byte[]) GetValue(RichDataProperty);
            set => SetValue(RichDataProperty, value);
        }
    }
}