using BogdanCristinaLab7.Models;
using Plugin.LocalNotification;


namespace BogdanCristinaLab7;

public partial class ShopPage : ContentPage
{
    public ShopPage(Shop shop)
    {
        InitializeComponent();
    }
    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var shop = (Shop)BindingContext;
        await App.Database.SaveShopAsync(shop);
        await Navigation.PopAsync();
    }
    async void OnShowMapButtonClicked(object sender, EventArgs e)
    {
        var shop = (Shop)BindingContext;
        var address = shop.Address;
        var locations = await Geocoding.GetLocationsAsync(address);

        var options = new MapLaunchOptions { Name = "Magazinul meu preferat" };
        var location = locations?.FirstOrDefault();


        var myLocation = new Location(46.7731796289, 23.6213886738);

        var distance = Location.CalculateDistance(myLocation, location, DistanceUnits.Kilometers);
        if (distance < 17) {
            var request = new NotificationRequest
            {
                Title = "Ai de facut cumparaturi in apropiere!",
                Description = address,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = DateTime.Now.AddSeconds(3)
                }
            };
            LocalNotificationCenter.Current.Show(request); 
        }

        await Map.OpenAsync(location, options);
        }
    }



