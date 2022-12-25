
using System.Windows;

namespace BTEJA_BCSH2_semestralka;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{

    public MainWindow()
    {
        InitializeComponent();
        DataContext = App.Current.MainMV;
    }
}
