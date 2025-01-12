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
    // public Picker ShopPicker { get; set; }

    public ShopEntryPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        listView.ItemsSource = await App.Database.GetShopsAsync();
    }

    async void OnShopAddedClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ShopPage
        {
            BindingContext = new Shop()
        });
    }

    async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            await Navigation.PushAsync(new ShopPage
            {
                BindingContext = e.SelectedItem as Shop
            });
        }
    }

    public async Task<List<Shop>> GetShopListsAsync()
    {
        return await App.Database.GetShopsAsync();
    }
}
