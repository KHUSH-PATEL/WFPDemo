using System;
using System.Collections.Generic;
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
    /// Interaction logic for AddProductPage.xaml
    /// </summary>
    public partial class AddProductPage : UserControl
    {
        ProductDbContext dbContext;
        private Product product = new Product();
        public AddProductPage(ProductDbContext dbContext)
        {
            this.dbContext = dbContext;
            InitializeComponent();
        }

        private void AddProduct(object s, RoutedEventArgs e)
        {
            product.Name = txtName.Text;
            product.Description = txtDescription.Text;

            // Handle price parsing
            if (double.TryParse(txtPrice.Text, out double price))
            {
                product.Price = price;
            }

            product.Unit = Convert.ToInt32(txtUnit.Text);
            dbContext.Products.Add(product);
            // Save changes to database
            dbContext.SaveChanges();

            // Optionally notify the user that the product has been updated
            MessageBox.Show("Product updated successfully.");

            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                if (mainWindow.MainFrame != null)
                {
                    mainWindow.MainFrame.Content = new ProductListPage(dbContext);
                }
            }
        }
    }
}
