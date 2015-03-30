using System.ExtendedDateTimeFormat;
using System.Windows;
using System.Windows.Media;

namespace Validator
{
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
                ExtendedDateTimeFormatParser.Parse(inputBox.Text);
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