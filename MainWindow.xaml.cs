using Microsoft.Win32;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace FullScreenStillApp;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private DoubleAnimation FadeInAnimation = new DoubleAnimation(0.0, 1.0, TimeSpan.FromSeconds(2));
    private DoubleAnimation FadeOutAnimation = new DoubleAnimation(0.0, TimeSpan.FromSeconds(2));

    private SolidColorBrush TransparentBrush = new SolidColorBrush(Colors.Transparent);
    private SolidColorBrush DefaultBgBrush = new SolidColorBrush(Color.FromArgb(0x11, 0, 0, 0xFF));

    public MainWindow()
    {
        InitializeComponent();
    }

    private void Border_PreviewMouseDown(object sender, MouseButtonEventArgs e)
    {
        var ofd = new OpenFileDialog();

        if (ofd.ShowDialog() != true)
            return;

        var filePath = ofd.FileName;

        if (string.IsNullOrEmpty(filePath))
            return;



        this.imgHolder.Source = new BitmapImage(new Uri(filePath));

        this.imgHolder.Opacity = 0;
        this.DoFade();

    }
   

    private void Window_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            if(this.WindowState == WindowState.Maximized)
            {
                DoFade();
            }
            else
            {
                this.DragMove();
            }
        }
        else if (e.RightButton == MouseButtonState.Pressed)
        {
            switch (this.WindowState)
            {
                case WindowState.Maximized:
                    this.WindowState = WindowState.Normal;
                    this.Background = this.DefaultBgBrush;
                    break;
                default:
                    this.WindowState = WindowState.Maximized;
                    this.Background = this.TransparentBrush;
                    break;
            }
        }
    }

    private void DoFade()
    {
        if (this.imgHolder.Opacity > 0)
        {
            this.imgHolder.BeginAnimation(Image.OpacityProperty, this.FadeOutAnimation);
        }
        else
        {
            this.imgHolder.BeginAnimation(Image.OpacityProperty, this.FadeInAnimation);
        }
    }
}
