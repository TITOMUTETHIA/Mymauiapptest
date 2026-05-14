namespace MyMauiApp;

public partial class ContactPageHeader : ContentView
{
    public static readonly BindableProperty HeaderTitleProperty =
        BindableProperty.Create(nameof(HeaderTitle), typeof(string), typeof(ContactPageHeader), string.Empty);

    public static readonly BindableProperty HeaderSubtitleProperty =
        BindableProperty.Create(nameof(HeaderSubtitle), typeof(string), typeof(ContactPageHeader), string.Empty);

    public string HeaderTitle
    {
        get => (string)GetValue(HeaderTitleProperty);
        set => SetValue(HeaderTitleProperty, value);
    }

    public string HeaderSubtitle
    {
        get => (string)GetValue(HeaderSubtitleProperty);
        set => SetValue(HeaderSubtitleProperty, value);
    }

    public ContactPageHeader()
    {
        InitializeComponent();
        // Set the BindingContext to itself so BindableProperties can be used directly in XAML
        BindingContext = this;
    }
}