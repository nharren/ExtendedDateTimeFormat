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
using System.ExtendedDateTimeFormat;

namespace Validator
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ExtendedDateTimeFormatParser.ParseAll(inputBox.Text);
            }
            catch (ParseException pe)
            {
                errorMessage.Foreground = Brushes.Red;
                errorMessage.Text = pe.Message;

                return;
            }

            errorMessage.Foreground = Brushes.LimeGreen;
            errorMessage.Text = "Success!";
        }
    }
}
