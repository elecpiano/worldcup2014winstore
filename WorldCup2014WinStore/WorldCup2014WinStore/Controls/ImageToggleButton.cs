using Windows.UI.Xaml;
using Windows.UI.Xaml.Automation;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;

namespace WorldCup2014WinStore.Controls
{
    public class ImageToggleButton : ToggleButton
    {
        #region CheckedBackground

        public ImageSource CheckedBackgroundNormal
        {
            get { return (ImageSource)GetValue(CheckedBackgroundNormalProperty); }
            set
            {
                SetValue(CheckedBackgroundNormalProperty, value);
            }
        }

        public static readonly DependencyProperty CheckedBackgroundNormalProperty =
            DependencyProperty.Register("CheckedBackgroundNormal", typeof(ImageSource), typeof(ImageToggleButton), new PropertyMetadata(null));

        public ImageSource CheckedBackgroundPressed
        {
            get { return (ImageSource)GetValue(CheckedBackgroundPressedProperty); }
            set
            {
                SetValue(CheckedBackgroundPressedProperty, value);
            }
        }

        public static readonly DependencyProperty CheckedBackgroundPressedProperty =
            DependencyProperty.Register("CheckedBackgroundPressed", typeof(ImageSource), typeof(ImageToggleButton), new PropertyMetadata(null));

        public ImageSource CheckedBackgroundHover
        {
            get { return (ImageSource)GetValue(CheckedBackgroundHoverProperty); }
            set
            {
                SetValue(CheckedBackgroundHoverProperty, value);
            }
        }

        public static readonly DependencyProperty CheckedBackgroundHoverProperty =
            DependencyProperty.Register("CheckedBackgroundHover", typeof(ImageSource), typeof(ImageToggleButton), new PropertyMetadata(null));

        public ImageSource CheckedBackgroundDisabled
        {
            get { return (ImageSource)GetValue(CheckedBackgroundDisabledProperty); }
            set
            {
                SetValue(CheckedBackgroundDisabledProperty, value);
            }
        }

        public static readonly DependencyProperty CheckedBackgroundDisabledProperty =
            DependencyProperty.Register("CheckedBackgroundDisabled", typeof(ImageSource), typeof(ImageToggleButton), new PropertyMetadata(null));

        #endregion

        #region UnCheckedBackground

        public ImageSource UnCheckedBackgroundNormal
        {
            get { return (ImageSource)GetValue(UnCheckedBackgroundNormalProperty); }
            set
            {
                SetValue(UnCheckedBackgroundNormalProperty, value);
            }
        }

        public static readonly DependencyProperty UnCheckedBackgroundNormalProperty =
            DependencyProperty.Register("UnCheckedBackgroundNormal", typeof(ImageSource), typeof(ImageToggleButton), new PropertyMetadata(null));

        public ImageSource UnCheckedBackgroundPressed
        {
            get { return (ImageSource)GetValue(UnCheckedBackgroundPressedProperty); }
            set
            {
                SetValue(UnCheckedBackgroundPressedProperty, value);
            }
        }

        public static readonly DependencyProperty UnCheckedBackgroundPressedProperty =
            DependencyProperty.Register("UnCheckedBackgroundPressed", typeof(ImageSource), typeof(ImageToggleButton), new PropertyMetadata(null));

        public ImageSource UnCheckedBackgroundHover
        {
            get { return (ImageSource)GetValue(UnCheckedBackgroundHoverProperty); }
            set
            {
                SetValue(UnCheckedBackgroundHoverProperty, value);
            }
        }

        public static readonly DependencyProperty UnCheckedBackgroundHoverProperty =
            DependencyProperty.Register("UnCheckedBackgroundHover", typeof(ImageSource), typeof(ImageToggleButton), new PropertyMetadata(null));

        public ImageSource UnCheckedBackgroundDisabled
        {
            get { return (ImageSource)GetValue(UnCheckedBackgroundDisabledProperty); }
            set
            {
                SetValue(UnCheckedBackgroundDisabledProperty, value);
            }
        }

        public static readonly DependencyProperty UnCheckedBackgroundDisabledProperty =
            DependencyProperty.Register("UnCheckedBackgroundDisabled", typeof(ImageSource), typeof(ImageToggleButton), new PropertyMetadata(null));

        #endregion

        #region Caption

        public string CheckedCaption
        {
            get { return (string)GetValue(CheckedCaptionProperty); }
            set
            {
                SetValue(CheckedCaptionProperty, value);
            }
        }

        public static readonly DependencyProperty CheckedCaptionProperty =
            DependencyProperty.Register("CheckedCaption", typeof(string), typeof(ImageToggleButton), new PropertyMetadata(string.Empty));

        public string UnCheckedCaption
        {
            get { return (string)GetValue(UnCheckedCaptionProperty); }
            set
            {
                SetValue(UnCheckedCaptionProperty, value);
            }
        }

        public static readonly DependencyProperty UnCheckedCaptionProperty =
            DependencyProperty.Register("UnCheckedCaption", typeof(string), typeof(ImageToggleButton), new PropertyMetadata(string.Empty));


        #endregion

        #region Constructor

        public ImageToggleButton()
        {
            this.Checked += ImageToggleButton_Checked;
            this.Unchecked += ImageToggleButton_Unchecked;
        }

        void ImageToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            VisualStateManager.GoToState(this, "Normal", false);
        }

        void ImageToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            VisualStateManager.GoToState(this, "Checked", false);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (this.IsChecked==true)
            {
                this.SetValue(AutomationProperties.NameProperty, CheckedCaption);
                VisualStateManager.GoToState(this, "Checked", false);
            }
            else
            {
                this.SetValue(AutomationProperties.NameProperty, UnCheckedCaption);
            }
        }

        #endregion


    }
}
