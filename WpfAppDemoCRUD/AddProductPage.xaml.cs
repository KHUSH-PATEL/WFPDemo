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
            double.TryParse(txtPrice.Text, out double price);
            int.TryParse(txtUnit.Text, out int productUnit);
            if (string.IsNullOrEmpty(txtName.Text)) {
                MessageBox.Show("Please enter product name.");
            } else if (string.IsNullOrEmpty(txtDescription.Text)) {
                MessageBox.Show("Please enter product description.");
            }
            else if (txtPrice.Text == null || price <= 0)
            {
                MessageBox.Show("Product price shoud be  greater than zero.");
            } else if (txtUnit.Text == null || productUnit <= 0)
            {
                MessageBox.Show("Product unit shoud be greater than zero.");
            }
            else
            {
                product.Name = txtName.Text;
                product.Description = txtDescription.Text;

                product.Price = price;

                product.Unit = Convert.ToInt32(txtUnit.Text);
                dbContext.Products.Add(product);
                // Save changes to database
                dbContext.SaveChanges();

            // Optionally notify the user that the product has been updated
            MessageBox.Show("Product Added successfully.");

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
}
