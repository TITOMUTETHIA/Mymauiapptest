namespace MyMauiApp;

public partial class ContactDetailItem : ContentView
{
    public static readonly BindableProperty DetailTitleProperty =
        BindableProperty.Create(nameof(DetailTitle), typeof(string), typeof(ContactDetailItem), string.Empty);

    public static readonly BindableProperty DetailValueProperty =
        BindableProperty.Create(nameof(DetailValue), typeof(string), typeof(ContactDetailItem), string.Empty);

    public string DetailTitle
    {
        get => (string)GetValue(DetailTitleProperty);
        set => SetValue(DetailTitleProperty, value);
    }

    public string DetailValue
    {
        get => (string)GetValue(DetailValueProperty);
        set => SetValue(DetailValueProperty, value);
    }

    public ContactDetailItem()
    {
        InitializeComponent();
        // Set the BindingContext to itself so BindableProperties can be used directly in XAML
        BindingContext = this;
    }
}