using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BogdanCristinaLab7.Models;

namespace BogdanCristinaLab7;

public partial class ListPage : ContentPage
{
    public ListPage()
    {
        InitializeComponent();
        BindingContext = new ShopList(); 
    }
    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
       slist.Date = DateTime.UtcNow;
        Shop selectedShop = (ShopPicker.SelectedItem as Shop);
        slist.ShopID = selectedShop.Id;
        await App.Database.SaveShopListAsync(slist);
        await Navigation.PopAsync();
    }
    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        await App.Database.DeleteShopListAsync(slist);
        await Navigation.PopAsync();
    }

    async void OnChooseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProductPage((ShopList)this.BindingContext)
        {
            BindingContext = new Product()
        });
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var items = await App.Database.GetShopListsAsync();
        ShopPicker.ItemsSource = (System.Collections.IList)items;
        ShopPicker.ItemDisplayBinding = new Binding("ShopDetails"); 
        var shopl = (ShopList)BindingContext;
        listView.ItemsSource = await App.Database.GetListProductsAsync(shopl.Id);
    }
    async void OnDeleteProductButtonClicked(object sender, EventArgs e)
    {
        if (listView.SelectedItem != null)
        {
            var listProduct = listView.SelectedItem as ListProduct;
            if (listProduct != null)
            {
                bool confirm = await DisplayAlert("Confirm", "Are you sure you want to delete this item?", "Yes", "No");
                if (confirm)
                {
                    await App.Database.DeleteProductAsync(listProduct);
                    var shopl = (ShopList)BindingContext;
                    listView.ItemsSource = await App.Database.GetListProductsAsync(shopl.Id);
                }
            }
            else
            {
                await DisplayAlert("Error", "No product selected.", "OK");
            }
        }
        else
        {
            await DisplayAlert("Error", "No product selected.", "OK");
        }
    }
}