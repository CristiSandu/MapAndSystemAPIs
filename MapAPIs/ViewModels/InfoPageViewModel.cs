using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MapAPIs.Models;
using Microsoft.Maui.Controls.Maps;

namespace MapAPIs.ViewModels;

public partial class InfoPageViewModel : BaseViewModel, IQueryAttributable
{
    [ObservableProperty]
    MonkeyModel monkey = new();

    [ObservableProperty]
    List<Pin> pins = new List<Pin>();

    [ObservableProperty]
    string toSave;

    [ObservableProperty]
    string savedTo;

    private CancellationTokenSource _cancelTokenSource;
    private bool _isCheckingLocation;

    public InfoPageViewModel()
    {

    }

    [RelayCommand]
    private async void CopyToClipboard()
    {
        await Clipboard.Default.SetTextAsync("This text was highlighted in the UI.");
    }

    [RelayCommand]
    private async void SaveToPreferences()
    {
        Preferences.Set("text", ToSave);
    }

    [RelayCommand]
    private async void SendSms()
    {
        if (Sms.Default.IsComposeSupported)
        {
            string[] recipients = new[] { "0775592486" };
            string text = "Hello, I'm interested in to learn .NET MAUI";

            var message = new SmsMessage(text, recipients);

            await Sms.Default.ComposeAsync(message);
        }
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Monkey = query["Monkey"] as MonkeyModel;
        Pin currentLocation = new Pin();

        SavedTo = Preferences.Get("text", "undefined");

        try
        {
            _isCheckingLocation = true;

            GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

            _cancelTokenSource = new CancellationTokenSource();

            Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

            if (location != null)
            {
                currentLocation.Location = new Location(location.Latitude, location.Longitude);
                currentLocation.Label = "Sunt aici!";
                currentLocation.Type = PinType.SearchResult;
            }
        }
        catch (FeatureNotSupportedException fnsEx)
        {
            // Handle not supported on device exception
        }
        catch (FeatureNotEnabledException fneEx)
        {
            // Handle not enabled on device exception
        }
        catch (PermissionException pEx)
        {
            // Handle permission exception
        }
        catch (Exception ex)
        {
            // Unable to get location
        }

        Pins = new List<Pin>
        {
            new Pin
            {
                Location = new Location(Monkey.Latitude, Monkey.Longitude),
                Label = Monkey.Name,
                Address = Monkey.Location,
                Type = PinType.SearchResult
            },
            currentLocation
        };
    }
}
