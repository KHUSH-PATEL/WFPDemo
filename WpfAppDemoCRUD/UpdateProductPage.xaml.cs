using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

            if (product != null)
            {
                txtName.Text = product.Name;
                txtDescription.Text = product.Description;
                txtPrice.Text = product.Price.ToString();
                txtUnit.Text = product.Unit.ToString();

                if (product.Image != null)
                {
                    BitmapImage imageSource = ByteArrayToBitmapImage(product.Image);
                    imgPreview.Source = imageSource;
                }
            }
        }
        public BitmapImage ByteArrayToBitmapImage(byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0)
                return null;

            BitmapImage bitmapImage = new BitmapImage();
            using (MemoryStream stream = new MemoryStream(byteArray))
            {
                stream.Position = 0;
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = stream;
                bitmapImage.EndInit();
            }
            bitmapImage.Freeze();
            return bitmapImage;
        }
        private void SelectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string imagePath = openFileDialog.FileName;
                BitmapImage bitmap = new BitmapImage(new Uri(imagePath));
                imgPreview.Source = bitmap; // Ensure imgPreview is recognized here

                // Convert the selected image to byte array and store in Product object
                product.Image = ConvertImageToByteArray(bitmap);
            }
        }

        private byte[] ConvertImageToByteArray(BitmapImage bitmapImage)
        {
            if (bitmapImage == null)
                return null;

            // Determine the file format based on the image URI
            string imageFormat = GetImageFormatFromUri(bitmapImage.UriSource);

            if (imageFormat == null)
            {
                // Default to PNG encoding if the format cannot be determined
                imageFormat = "png";
            }

            using (MemoryStream stream = new MemoryStream())
            {
                BitmapEncoder encoder = null;

                // Select the appropriate encoder based on the image format
                switch (imageFormat.ToLower())
                {
                    case "png":
                        encoder = new PngBitmapEncoder();
                        break;
                    case "jpeg":
                    case "jpg":
                        encoder = new JpegBitmapEncoder();
                        break;
                    case "bmp":
                        encoder = new BmpBitmapEncoder();
                        break;
                    // Add support for other image formats as needed
                    default:
                        throw new NotSupportedException($"Image format '{imageFormat}' is not supported.");
                }

                // Encode and save the bitmap image to the memory stream
                encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                encoder.Save(stream);
                return stream.ToArray();
            }
        }

        private string GetImageFormatFromUri(Uri uri)
        {
            if (uri == null)
                return null;

            string extension = System.IO.Path.GetExtension(uri.AbsoluteUri);

            // Remove leading dot from the extension
            if (!string.IsNullOrEmpty(extension) && extension.Length > 1 && extension[0] == '.')
            {
                extension = extension.Substring(1);
            }

            return extension.ToLower();
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
                    product.Image = ConvertImageToByteArray(imgPreview.Source as BitmapImage);

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
