using System.IO;
using System.Runtime.InteropServices;
using System.Windows;

namespace IdeSK.View;

/// <summary>
/// Interaction logic for HelpWindow.xaml
/// </summary>
public partial class HelpWindow : Window
{
    public HelpWindow(string content)
    {
        InitializeComponent();
        string body = string.Empty;
        string header = string.Empty;
        if (content == "Grammar")
        {
            header = content;
            body = Texts.GrammarHelp;
        }
        else if (content == "Library")
        {
            header = "Library Functions";
            body = Texts.LibraryHelp;
        }
        else
        { 
            header = "Error";
            body = "Invalid content type";
        }

        Title = $"{content} Help";
        label_header.Content = header;
        textBlock_body.Text = body;
    }
}
