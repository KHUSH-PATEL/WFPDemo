﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfAppDemoCRUD.Data;

namespace WpfAppDemoCRUD
{
    /// <summary>
    /// Interaction logic for ProductListPage.xaml
    /// </summary>
    
    public partial class ProductListPage : UserControl
    {
        ProductDbContext dbContext;
        private readonly int pageSize = 10; // Number of items per page
        private int currentPage = 1; // Current page index
        private ObservableCollection<Product> productsCollection;
        public ProductListPage(ProductDbContext dbContext)
        {
            this.dbContext = dbContext;
            InitializeComponent();
            GetProducts();
        }

        private void GetProducts()
        {
            int skip = (currentPage - 1) * pageSize;
            List<Product> products = dbContext.Products.Skip(skip).Take(pageSize).ToList();
            skip= ((currentPage+1) - 1) * pageSize;
            List<Product> productAvailable = dbContext.Products.Skip(skip).Take(pageSize).ToList();
            if(products.Count > 0)
            {
                
                ProductDG.ItemsSource = products;
                productsCollection = new ObservableCollection<Product>(products);
                ProductDG.ItemsSource = productsCollection;
            }
            if (productAvailable.Count <= 0)
            {
                nextButton.Visibility = Visibility.Hidden;
            }
            else
            {
                nextButton.Visibility = Visibility.Visible;
            }                     
        }
       
        private void RefreshProducts()
        {
            int skip = (currentPage - 1) * pageSize;
            List<Product> products = dbContext.Products.Skip(skip).Take(pageSize).ToList();

            // Update the existing ObservableCollection with new data
            productsCollection.Clear(); // Clear existing items
            foreach (var product in products)
            {
                productsCollection.Add(product); // Add updated items
            }
            skip = ((currentPage + 1) - 1) * pageSize;
            List<Product> productAvailable = dbContext.Products.Skip(skip).Take(pageSize).ToList();
            if (productAvailable.Count <= 0)
            {
                nextButton.Visibility = Visibility.Hidden;
            }
            else
            {
                nextButton.Visibility = Visibility.Visible;
            }
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            currentPage++;

            RefreshProducts();
        }

        private void PreviousPage_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                RefreshProducts();
            }
        }
        private void DeleteProduct(object s, RoutedEventArgs e)
        {
            var productToBeDeleted = (s as FrameworkElement).DataContext as Product;

            if (productToBeDeleted != null)
            {
                dbContext.Products.Remove(productToBeDeleted);
                dbContext.SaveChanges();

                // Remove the deleted product from the collection
                productsCollection.Remove(productToBeDeleted);

                MessageBox.Show("Product Deleted Successfully.");
            }
            //RefreshProducts();
        }
        private void UpdateProductForEdit(object s, RoutedEventArgs e)
        {
            if (s is FrameworkElement element && element.DataContext is Product product)
            {
                // Create an instance of UpdateProductPage with the specified product
                UpdateProductPage updateProductPage = new UpdateProductPage(dbContext, product);

                // Access the MainWindow's MainFrame and set its content to the UpdateProductPage
                if (Application.Current.MainWindow is MainWindow mainWindow)
                {
                    if (mainWindow.MainFrame != null)
                    {
                        mainWindow.MainFrame.Content = updateProductPage;
                    }
                }
            }
        }
        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Product product)
            {
                Cart productCart = new Cart();
                productCart.ProductId = product.Id;
                productCart.Name = product.Name;
                productCart.Price = product.Price;
                productCart.Unit = product.Unit;

                dbContext.Cart.Add(productCart);
                dbContext.SaveChanges();
                MessageBox.Show("Product added to cart successfully.");
            }
        }

        //private void UpdateProduct(object s, RoutedEventArgs e)
        //{
        //    dbContext.Update(selectedProduct);
        //    dbContext.SaveChanges();
        //    UpdateProductGrid.DataContext = new Product();
        //    GetProducts();
        //}
    }
}
