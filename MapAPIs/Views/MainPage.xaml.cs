using MapAPIs.ViewModels;

namespace MapAPIs.Views;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}

