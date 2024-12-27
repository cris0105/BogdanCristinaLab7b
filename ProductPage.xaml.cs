using System;
using System.Collections.Generic;
using BogdanCristinaLab7.Data;
using BogdanCristinaLab7.Models;

namespace BogdanCristinaLab7;

public partial class ProductPage : ContentPage
{
    private ShopList _shopList;

    public ProductPage(ShopList shopList)
    {
        InitializeComponent();
        _shopList = shopList;
    }

    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        try
        {
            if (BindingContext is Product product)
            {
                await App.Database.SaveProductAsync(product);
                listView.ItemsSource = await App.Database.GetProductsAsync();
            }
            else
            {
                await DisplayAlert("Error", "BindingContext is not a valid Product.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }

    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        try
        {
            var selectedProduct = listView.SelectedItem as Product;
            if (selectedProduct != null)
            {
                await App.Database.DeleteProductAsync(selectedProduct);
                listView.ItemsSource = await App.Database.GetProductsAsync();
            }
            else
            {
                await DisplayAlert("Error", "Please select a product to delete.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        try
        {
            listView.ItemsSource = await App.Database.GetProductsAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occurred while loading products: {ex.Message}", "OK");
        }
    }

    async void OnAddButtonClicked(object sender, EventArgs e)
    {
        try
        {
            if (listView.SelectedItem is Product selectedProduct)
            {
                if (_shopList != null)
                {
                    var listProduct = new ListProduct
                    {
                        ShopListId = (int)_shopList.ID,
                        ProductId = selectedProduct.ID
                    };

                    await App.Database.SaveListProductAsync(listProduct);
                    selectedProduct.ListProducts = new List<ListProduct> { listProduct };

                    await DisplayAlert("Success", "Product added to the shopping list.", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "No shopping list is associated with this page.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "Please select a product to add.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }
}
