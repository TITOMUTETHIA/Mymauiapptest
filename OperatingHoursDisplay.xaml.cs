namespace MyMauiApp;

public partial class OperatingHoursDisplay : ContentView
{
    public static readonly BindableProperty HoursTextProperty =
        BindableProperty.Create(nameof(HoursText), typeof(string), typeof(OperatingHoursDisplay), string.Empty);

    public string HoursText
    {
        get => (string)GetValue(HoursTextProperty);
        set => SetValue(HoursTextProperty, value);
    }

    public OperatingHoursDisplay()
    {
        InitializeComponent();
        // Set the BindingContext to itself so BindableProperties can be used directly in XAML
        BindingContext = this;
    }
}