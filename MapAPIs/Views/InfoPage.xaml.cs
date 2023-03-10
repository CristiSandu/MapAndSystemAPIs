using MapAPIs.ViewModels;

namespace MapAPIs.Views;

public partial class InfoPage : ContentPage
{
    public InfoPage(InfoPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}