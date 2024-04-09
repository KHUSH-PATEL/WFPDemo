using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ProductDbContext dbContext;
        //Product NewProduct = new Product();
        //private readonly int pageSize = 10; // Number of items per page
        //private int currentPage = 1; // Current page index

        public MainWindow(ProductDbContext dbContext)
        {
            this.dbContext = dbContext;
            InitializeComponent();
            NavigateToHomePage(null, null);
            //GetProducts();
            //AddNewProductGrid.DataContext = NewProduct;
        }       

        private void NavigateToHomePage(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new HomePage();
        }
        private void NavigateToProductListPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new ProductListPage(dbContext);
        }

        private void NavigateToAddProductPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new AddProductPage(dbContext);
        }
        //private void GetProducts()
        //{
        //    int skip = (currentPage - 1) * pageSize;
        //    List<Product> products = dbContext.Products.Skip(skip).Take(pageSize).ToList();
        //    ProductDG.ItemsSource = products;
        //}

        //private void AddProduct(object s, RoutedEventArgs e)
        //{
        //    dbContext.Products.Add(NewProduct);
        //    dbContext.SaveChanges();
        //    GetProducts();
        //    NewProduct = new Product();
        //    AddNewProductGrid.DataContext = NewProduct;
        //}

        //Product selectedProduct = new Product();
        //private void UpdateProductForEdit(object s, RoutedEventArgs e)
        //{
        //    selectedProduct = (s as FrameworkElement).DataContext as Product;
        //    UpdateProductGrid.DataContext = selectedProduct;
        //}

        //private void UpdateProduct(object s, RoutedEventArgs e)
        //{
        //    dbContext.Update(selectedProduct);
        //    dbContext.SaveChanges();
        //    UpdateProductGrid.DataContext = new Product();
        //    GetProducts();
        //}

        //private void DeleteProduct(object s, RoutedEventArgs e)
        //{
        //    var productToBeDeleted = (s as FrameworkElement).DataContext as Product;
        //    dbContext.Products.Remove(productToBeDeleted);
        //    dbContext.SaveChanges();
        //    GetProducts();
        //}

        private void ProductDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        //private void NextPage_Click(object sender, RoutedEventArgs e)
        //{
        //    currentPage++;
        //    GetProducts();
        //}

        //private void PreviousPage_Click(object sender, RoutedEventArgs e)
        //{
        //    if (currentPage > 1)
        //    {
        //        currentPage--;
        //        GetProducts();
        //    }
        //}
    }
}
