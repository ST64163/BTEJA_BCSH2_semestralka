using System.Windows;

namespace IdeSK.View
{
    /// <summary>
    /// Interaction logic for InputDialog.xaml
    /// </summary>
    public partial class InputDialog : Window
    {
        public string Input { get => textBox_input.Text; }

        public InputDialog()
        {
            InitializeComponent();
        }

        private void ClickInsert(object sender, RoutedEventArgs args)
        {
            DialogResult = true;
        }
    }
}
