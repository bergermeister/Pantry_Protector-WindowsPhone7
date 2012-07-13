using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

using Microsoft.Phone.Controls.Primitives;

namespace PantryProtector
{
    /// <summary>
    /// An infinitely scrolling, UI- and data-virtualizing selection control.
    /// </summary>
    public class DigitLoopingSelector : LoopingSelector
    {

        public DigitLoopingSelector()
        {
            this.Loaded += (obj, args) =>
            {
                DataSource = new helpers.DigitDataSource(MinValue, MaxValue, Step, DefaultValue, StringFormat);

                DataSource.SelectionChanged += (obj1, arg1) =>
                {
                    this.SelectedItem = Convert.ToInt32(arg1.AddedItems[0]);
                };
            };
        }

        public int MinValue
        {
            get
            {
                return (int)GetValue(MinValueProperty);
            }
            set
            {
                SetValue(MinValueProperty, value);
            }
        }

        public int MaxValue
        {
            get
            {
                return (int)GetValue(MaxValueProperty);
            }
            set
            {
                SetValue(MaxValueProperty, value);
            }
        }

        public int Step
        {
            get
            {
                return (int)GetValue(StepProperty);
            }
            set
            {
                SetValue(StepProperty, value);
            }
        }

        public int SelectedItem
        {
            get
            {
                return (int)GetValue(SelectedItemProperty);
            }
            set
            {
                SetValue(SelectedItemProperty, value);
            }
        }

        public int DefaultValue
        {
            get
            {
                return (int)GetValue(DefaultValueProperty);
            }
            set
            {
                SetValue(DefaultValueProperty, value);
            }
        }

        /// <summary>
        /// Standard Numeric Format Strings (http://msdn.microsoft.com/en-us/library/dwhawy9k.aspx)
        /// </summary>
        public string StringFormat
        {
            get
            {
                return GetValue(StringFormatProperty).ToString();
            }
            set
            {
                SetValue(StringFormatProperty, value);
            }
        }

        /// <summary>
        /// The MaxValue DependencyProperty.
        /// </summary>
        public static readonly DependencyProperty MaxValueProperty =
                DependencyProperty.Register("MaxValue", typeof(int), typeof(DigitLoopingSelector), new PropertyMetadata(100, ValueChanged));

        /// <summary>
        /// The MinValue DependencyProperty.
        /// </summary>
        public static readonly DependencyProperty MinValueProperty =
                DependencyProperty.Register("MinValue", typeof(int), typeof(DigitLoopingSelector), new PropertyMetadata(0, ValueChanged));

        /// <summary>
        /// The Step DependencyProperty.
        /// </summary>
        public static readonly DependencyProperty StepProperty =
                DependencyProperty.Register("Step", typeof(int), typeof(DigitLoopingSelector), new PropertyMetadata(1, ValueChanged));

        /// <summary>
        /// The Step DependencyProperty.
        /// </summary>
        public static readonly DependencyProperty SelectedItemProperty =
                DependencyProperty.Register("SelectedItem", typeof(int), typeof(DigitLoopingSelector)
                , new PropertyMetadata(new PropertyChangedCallback((sender, e) =>
                {
                    DigitLoopingSelector _this = (DigitLoopingSelector)sender;
                    _this.DataSource.SelectedItem = e.NewValue;
                })));

        /// <summary>
        /// The Step DependencyProperty.
        /// </summary>
        public static readonly DependencyProperty DefaultValueProperty =
                DependencyProperty.Register("DefaultValue", typeof(int), typeof(DigitLoopingSelector), new PropertyMetadata(0, ValueChanged));

        /// <summary>
        /// The Step DependencyProperty.
        /// </summary>
        public static readonly DependencyProperty StringFormatProperty =
                DependencyProperty.Register("StringFormat", typeof(string), typeof(DigitLoopingSelector), new PropertyMetadata(string.Empty, ValueChanged));

        private static void ValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            DigitLoopingSelector picker = (DigitLoopingSelector)obj;
            picker.DataSource = new helpers.DigitDataSource(picker.MinValue, picker.MaxValue, picker.Step, picker.DefaultValue, picker.StringFormat);
        }
    }
}
