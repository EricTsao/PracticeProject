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
using System.Drawing;

namespace ImgCheck
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = "*.jpg";
            dlg.Filter = "JPG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|JPEG Files (*.jpeg)|*.jepg|GIF Files (*.gif)|*.gif";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                txtFilePath.Text = filename;
            }
        }

        private void btnAnalysis_Click(object sender, RoutedEventArgs e)
        {            
            Bitmap originbmp = new Bitmap(txtFilePath.Text);

            imgShow.Source = new BitmapImage(new Uri(txtFilePath.Text));

            var dicColor = new Dictionary<string, ColorData>();
            for (int i = 0; i < originbmp.Width; i++)
            {
                for (int j = 0; j < originbmp.Height; j++)
                {
                    var pxColor = (ColorRGB)originbmp.GetPixel(i,j);
                    var colorData = new ColorData(pxColor);

                    if (dicColor.Keys.Contains(colorData.HLSString))
                    {
                        dicColor[colorData.HLSString].Count += 1;
                    }
                    else
                    {
                        dicColor.Add(colorData.HLSString, colorData);
                    }
                }
            }

            var result = string.Format(@"Dic Keys Count : {0}", dicColor.Count);
            txtResult.Text = result;

            var listColow = dicColor.Values.OrderByDescending(d => d.Count).ToList();
            listResult.ItemsSource = listColow;

            originbmp.Dispose();
        }

        private void ListViewItem_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListBoxItem;
            if (item != null && item.IsSelected)
            {
                var colorData = item.Content as ColorData;
                if (colorData != null) {
                    var z = (System.Drawing.Color)colorData.MyColor;
                    labelShowColor.Background = (System.Windows.Media.Brush)new BrushConverter().ConvertFromString("#"+z.Name); ;
                }
            }
        }
    }
}
