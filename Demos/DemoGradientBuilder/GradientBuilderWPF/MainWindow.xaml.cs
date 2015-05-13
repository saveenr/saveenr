using System;
using System.Collections.Generic;
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

namespace GradientBuilderWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }


        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.updatecolor();
        }

        private void slider2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.updatecolor();
        }

        private void slider3_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.updatecolor();

        }

        public void updatecolor()
        {
            if (this.slider2 == null)
            {
                return;
            }

            var f = rectangle1.Fill;

            var b = (System.Windows.Media.LinearGradientBrush)f;

            var first_stop = b.GradientStops[0];
            var last_stop = b.GradientStops[1];

            var start_hsl = new Viziblr.Colorspace.ColorHSL(this.base_hue(),1.0,0.5);

            double new_lightness = Viziblr.Colorspace.ColorUtil.NormalizeLightness(start_hsl.L + this.lightness_delta() - 0.5);
            double new_hue = Viziblr.Colorspace.ColorUtil.NormalizeHue(start_hsl.H + this.get_hue_delta());


            var end_hsl = new Viziblr.Colorspace.ColorHSL(
                new_hue,
                start_hsl.S,
                new_lightness);

            first_stop.Color = start_hsl.to_wpf_color();
            last_stop.Color = end_hsl.to_wpf_color();
        }
        public double get_hue_delta()
        {
            return this.slider1.Value;
        }

        public double lightness_delta()
        {
            return this.slider2.Value;
        }



        public double base_hue()
        {
            return this.slider3.Value;
        }

    }

    public static class extensions
    {
        public static System.Windows.Media.Color to_wpf_color(this Viziblr.Colorspace.ColorRGB rgb)
        {
            var x = new Viziblr.Colorspace.ColorRGB32Bit(rgb);
            var y = System.Windows.Media.Color.FromArgb(x.A, x.R, x.G, x.B);
            return y;
        }

        public static System.Windows.Media.Color to_wpf_color(this Viziblr.Colorspace.ColorHSL rgb)
        {
            var x = new Viziblr.Colorspace.ColorRGB32Bit(rgb);
            var y = System.Windows.Media.Color.FromArgb(x.A, x.R, x.G, x.B);
            return y;
        }


        public static Viziblr.Colorspace.ColorRGB to_viziblr_color(this System.Windows.Media.Color c)
        {
            var x = new Viziblr.Colorspace.ColorRGB32Bit(c.A,c.R,c.G,c.B);
            var y = new Viziblr.Colorspace.ColorRGB(x);
            return y;
        }



    }
}
