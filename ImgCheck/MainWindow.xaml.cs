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
        public List<ColorRGB> ColorMap;
        public MainWindow()
        {
            InitializeComponent();

            ColorMap = new List<ColorRGB>();

            RefreshColorMap();
        }

        private void RefreshColorMap()
        {
            for (double h = 0; h <= 360; h += 20)
            {
                for (double s = 0; s <= 1; s += 0.2)
                {
                    for (double b = 0; b <= 1; b += 0.2)
                    {
                        var colorRGB = new ColorRGB(h,s,b);

                        ColorMap.Add(colorRGB);
                    }
                }
            }
        }

        private ColorRGB GetSimlarColorFromColorMap(ColorRGB input) {
            var gapH = 10;
            var gapS = 0.1;
            var gapB = 0.1;

            var results = new List<ColorRGB>();

            results = ColorMap.Where(d =>
                    (d.Hue >= input.Hue - gapH && d.Hue < input.Hue + gapH)
                    &&
                    (d.Saturation >= input.Saturation - gapS && d.Saturation < input.Saturation + gapS)
                    &&
                    (d.Brightness >= input.Brightness - gapB && d.Brightness < input.Brightness + gapB)
                ).ToList();

            if (results.Count > 0) {
                return results.First();
            }

            return null;
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
                    var pxColor = (ColorRGB)originbmp.GetPixel(i, j);
                    var simlarColor = GetSimlarColorFromColorMap(pxColor);

                    if (simlarColor != null) {
                        var colorData = new ColorData(simlarColor);

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
            }

            var result = string.Format(@"Dic Keys Count : {0}", dicColor.Count);
            txtResult.Text = result;

            var listColow = dicColor.Values.OrderByDescending(d => d.Count).ToList();
            listResult.ItemsSource = listColow;

            originbmp.Dispose();
        }

        private void btnGetColorMap_Click(object sender, RoutedEventArgs e)
        {
            var result = string.Format(@"Dic Keys Count : {0}", ColorMap.Count);
            txtResult.Text = result;

            var listColow = ColorMap.OrderByDescending(d => d.HLSString).ToList();
            listResult.ItemsSource = listColow;
        }

        private void ListViewItem_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListBoxItem;
            if (item != null && item.IsSelected)
            {
                if (item.Content is ColorData)
                {
                    var colorData = item.Content as ColorData;
                    var z = (System.Drawing.Color)colorData.MyColor;
                    labelShowColor.Background = (System.Windows.Media.Brush)new BrushConverter().ConvertFromString("#" + z.Name);
                }
                else if (item.Content is ColorRGB)
                {
                    var colorRGB = item.Content as ColorRGB;
                    var z = System.Drawing.Color.FromArgb(colorRGB.Red, colorRGB.Green, colorRGB.Blue);
                    labelShowColor.Background = (System.Windows.Media.Brush)new BrushConverter().ConvertFromString("#" + z.Name);
                }
            }
        }
    }
}
