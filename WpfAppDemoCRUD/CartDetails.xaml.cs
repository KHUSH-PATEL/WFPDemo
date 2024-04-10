using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for CartDetails.xaml
    /// </summary>
    public partial class CartDetails : UserControl
    {
        ProductDbContext dbContext;
        private ObservableCollection<Cart> productsCollection;
        public CartDetails(ProductDbContext dbContext)
        {
            this.dbContext = dbContext;
            InitializeComponent();
            GetCartProducts();
        }
        private void GetCartProducts()
        {

            List<Cart> productCart = dbContext.Cart.ToList();
            ProductDG.ItemsSource = productCart;
            productsCollection = new ObservableCollection<Cart>(productCart);
            ProductDG.ItemsSource = productsCollection;
        }
        private void RemoveProduct(object s, RoutedEventArgs e)
        {
            var productToBeDeleted = (s as FrameworkElement).DataContext as Cart;

            if (productToBeDeleted != null)
            {
                dbContext.Cart.Remove(productToBeDeleted);
                dbContext.SaveChanges();

                productsCollection.Remove(productToBeDeleted);

                MessageBox.Show("Product Removed Successfully.");
            }
            GetCartProducts();
        }
    }
}
