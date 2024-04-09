using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Automation.Text;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using WpfAppDemoCRUD.Data;

namespace WpfAppDemoCRUD
{
    /// <summary>
    /// Interaction logic for UpdateProduct.xaml
    /// </summary>
    public partial class UpdateProductPage : UserControl
    {
        ProductDbContext dbContext;
        private Product product;
        public UpdateProductPage(ProductDbContext dbContext, Product product)
        {
            this.dbContext = dbContext;
            this.product = product;
            InitializeComponent();

            // Set the initial values of text boxes
            if (product != null)
            {
                txtName.Text = product.Name;
                txtDescription.Text = product.Description;
                txtPrice.Text = product.Price.ToString();
                txtUnit.Text = product.Unit.ToString();
            }
        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (product != null)
            {
                //// Update the product properties from text boxes
                product.Name = txtName.Text;
                product.Description = txtDescription.Text;

                // Handle price parsing
                if (decimal.TryParse(txtPrice.Text, out decimal price))
                {
                    product.Price = Convert.ToDouble(price);
                }

                product.Unit = Convert.ToInt32(txtUnit.Text);
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
}
