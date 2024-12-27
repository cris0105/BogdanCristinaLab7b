using System;
using System.Collections.Generic;
using System.Collections;
using BogdanCristinaLab7.Data; 
using BogdanCristinaLab7.Models;

namespace BogdanCristinaLab7;

public partial class ProductPage : ContentPage
{
    ShopList sl;
	public ProductPage(ShopList slist)
	{
		InitializeComponent();
        sl = slist;
    }
	async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var product = (Product)BindingContext;
        await App.Database.SaveProductAsync(product);
        listView.ItemsSource = await App.Database.GetProductsAsync();
    }
    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var product = listView.SelectedItem as Product;
        await App.Database.DeleteProductAsync(product);
        listView.ItemsSource = await App.Database.GetProductsAsync();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        listView.ItemsSource = await App.Database.GetProductsAsync();
    }
    async void OnAddButtonClicked(object sender, EventArgs e)
    {
        Product p;
        if (listView.SelectedItem != null)
        {
            p = listView.SelectedItem as Product;
            if (p != null && sl != null)
            {
                var lp = new ListProduct()
                {
                    ShopListId = (int)sl.ID,
                    ProductId = p.ID
                };
                await App.Database.SaveListProductAsync(lp);
                p.ListProducts = new List<ListProduct> { lp };

                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "Product or ShopList is not selected.", "OK");
            }
        }
    }
}