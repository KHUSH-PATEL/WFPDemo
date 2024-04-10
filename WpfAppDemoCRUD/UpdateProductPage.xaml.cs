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
                double.TryParse(txtPrice.Text, out double price);
                int.TryParse(txtUnit.Text, out int productUnit);
                if (string.IsNullOrEmpty(txtName.Text))
                {
                    MessageBox.Show("Please enter product name.");
                }
                else if (string.IsNullOrEmpty(txtDescription.Text))
                {
                    MessageBox.Show("Please enter product description.");
                }
                else if (txtPrice.Text == null || price <= 0)
                {
                    MessageBox.Show("Product price should be  greater than zero.");
                }
                else if (txtUnit.Text == null || productUnit <= 0)
                {
                    MessageBox.Show("Product unit should be greater than zero.");
                }
                else
                {
                  
                    product.Name = txtName.Text;
                    product.Description = txtDescription.Text;
                    product.Price = Convert.ToDouble(price);
                    product.Unit = Convert.ToInt32(txtUnit.Text);
            
                    dbContext.SaveChanges();

                   
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
}
