using System.Windows;

namespace IdeSK.View
{
    /// <summary>
    /// Interaction logic for FileWindow.xaml
    /// </summary>
    public partial class FileWindow : Window
    {
        public FileWindow(bool load)
        {
            InitializeComponent();
            Title = load ? "Load File" : "Save File";
            button_execute.Content = load ? "Load" : "Save";
        }
    }
}
