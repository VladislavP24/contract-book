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

namespace contact_notebook
{
    /// <summary>
    /// Interaction logic for NoteBookView.xaml
    /// </summary>
    public partial class NoteBookView : Window
    {
        public NoteBookView()
        {
            InitializeComponent();

            //Првязка ViewModel к DataContext
            DataContext = new NoteBookViewModel();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                textBox.Text = string.Empty;
                textBox.Foreground = Brushes.Black;
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !char.IsDigit(e.Text, 0);
        }
    }
}