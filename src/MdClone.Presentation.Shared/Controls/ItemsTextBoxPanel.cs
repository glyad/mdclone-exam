using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MdClone.Presentation.Shared.Controls
{
    public sealed class ItemsTextBoxPanel : Panel
    {
        private readonly List<double> _rows = new List<double>();

        protected override Size MeasureOverride(Size availableSize)
        {
            _rows.Clear();
            double left = 0;
            double rowHeight = 0;

            for (int index = 0; index < InternalChildren.Count; ++index)
            {
                var child = InternalChildren[index];
                child.Measure(availableSize);
                double childHeight = child.DesiredSize.Height;
                rowHeight = Math.Max(childHeight, rowHeight);
                double childWidth = child.DesiredSize.Width;

                if (left + childWidth > availableSize.Width)
                {
                    left = 0;
                    _rows.Add(rowHeight);
                    rowHeight = childHeight;
                }

                left += childWidth;
            }

            _rows.Add(rowHeight);
            return new Size(availableSize.Width, _rows.Sum());
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            double left = 0;
            double top = 0;
            int row = 0;

            for (int index = 0; index < InternalChildren.Count; ++index)
            {
                var child = InternalChildren[index];
                double childHeight = child.DesiredSize.Height;
                double childWidth = child.DesiredSize.Width;

                if (left + childWidth > finalSize.Width)
                {
                    left = 0;
                    top += _rows[row];
                    ++row;
                }

                if (index == InternalChildren.Count - 1)
                {
                    childWidth = finalSize.Width - left;
                }

                double rowHeight = _rows[row];
                double childTop = top + (rowHeight - childHeight) / 2;
                child.Arrange(new Rect(left, childTop, childWidth, childHeight));
                left += childWidth;
            }

            return finalSize;
        }
    }

}