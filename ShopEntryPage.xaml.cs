using BogdanCristinaLab7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BogdanCristinaLab7;

public partial class ShopEntryPage : ContentPage
{
    public Picker ShopPicker { get; set; }

    public ShopEntryPage()
    {
        InitializeComponent();
        ShopPicker = new Picker();
        Content = new StackLayout
        {
            Children = { ShopPicker }
        };
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var items = await App.Database.GetShopListsAsync();
        if (items == null || !items.Any())
        {
            await DisplayAlert("Error", "No shops available.", "OK");
        }
        else
        {
            ShopPicker.ItemsSource = items;
            ShopPicker.ItemDisplayBinding = new Binding("ShopName");
        }
        var shopl = (ShopList)BindingContext;
        listView.ItemsSource = await App.Database.GetListProductsAsync(shopl.Id);
    }

    async void OnShopAddedClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ShopPage(new Shop()));
    }

    async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            await Navigation.PushAsync(new ShopPage(e.SelectedItem as Shop));
        }
    }

    public async Task<List<Shop>> GetShopListsAsync()
    {
        return await App.Database.GetShopsAsync();
    }
}
