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

        private void Button_Click_1(object sender, RoutedEventArgs e)
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
                TextBox1.Text = filename;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {            
            Bitmap originbmp = new Bitmap(TextBox1.Text);

////            var px = originbmp.GetPixel(originbmp.Width / 2, originbmp.Height / 2);

////            TextBlock1.Text = string.Format(@"
////Width : {0}
////Height : {1}
////ARGB : {2}
////A : {3}
////R : {4}
////G : {5}
////B : {6}
////", originbmp.Width, originbmp.Height, px.ToArgb(), px.A, px.R, px.G, px.B);



            var dic = new Dictionary<string, int>();
            for (int i = 0; i < originbmp.Width; i++)
            {
                for (int j = 0; j < originbmp.Height; j++)
                {
                    var p = originbmp.GetPixel(i,j);
                    var key = string.Format("A:{0} R:{1} G:{2} B:{3}",p.A, p.R, p.G, p.B);
                    if (dic.Keys.Contains(key)) {
                        dic[key] = dic[key] + 1;
                    }
                    else
                    {
                        dic.Add(key, 1);
                    }
                }
            }

            TextBlock1.Text = string.Format(@"Dic Keys Count : {0}", dic.Keys.Count);




            int w = 510;
            int h = 510;
            Bitmap resizedbmp = new Bitmap(w, h);

            Graphics g = Graphics.FromImage(resizedbmp);
            //g.DrawImage(originbmp, new System.Drawing.Rectangle(0, 0, w, h), new System.Drawing.Rectangle(0, 0, originbmp.Width, originbmp.Height), GraphicsUnit.Pixel);

            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j > 256; j--)
                {
                    resizedbmp.SetPixel(i, j, System.Drawing.Color.FromArgb(px.R, px.G, 0));
                }
            }

            //resizedbmp.Save("b.jpg");

            g.Dispose();
            resizedbmp.Dispose();
            originbmp.Dispose();

        }
    }
}
