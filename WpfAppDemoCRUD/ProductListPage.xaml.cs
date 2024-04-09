using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
            ProductDG.ItemsSource = products;
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            currentPage++;
            GetProducts();
        }

        private void PreviousPage_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                GetProducts();
            }
        }
        private void DeleteProduct(object s, RoutedEventArgs e)
        {
            var productToBeDeleted = (s as FrameworkElement).DataContext as Product;
            dbContext.Products.Remove(productToBeDeleted);
            dbContext.SaveChanges();
            MessageBox.Show("Product Deleted Successfully.");
            GetProducts();
        }
        Product selectedProduct = new Product();
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

        //private void UpdateProduct(object s, RoutedEventArgs e)
        //{
        //    dbContext.Update(selectedProduct);
        //    dbContext.SaveChanges();
        //    UpdateProductGrid.DataContext = new Product();
        //    GetProducts();
        //}
    }
}
