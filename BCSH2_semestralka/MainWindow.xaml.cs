﻿
namespace BTEJA_BCSH2_semestralka;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{

    public MainWindow()
    {
        InitializeComponent();
        DataContext = App.Current.InterpreterMV;
    }

    /*
    private void ClickBuild(object sender, RoutedEventArgs e)
    {
        PrintLine("");
        string sourceCode = textBox_editor.Text;
        Interpreter interpreter = new();
        void WriteCallback(object sender, string message) => PrintLine(message);
        interpreter.WriteEvent += WriteCallback;
        interpreter.Build(sourceCode);
    }

    private void ClickInterpret(object sender, RoutedEventArgs e)
    {
        PrintLine("");
        string sourceCode = textBox_editor.Text;
        Interpreter interpreter = new();
        void WriteCallback(object sender, string message) => PrintLine(message);
        interpreter.WriteEvent += WriteCallback;
        interpreter.Interpret(sourceCode);
    }

    private void Print(string message) => textBox_console.Text += message;
    private void PrintLine(string message) => textBox_console.Text += $"{message}\n";

    private void ClickLoad(object sender, RoutedEventArgs e)
    {
        Print("\nLoad ...\n");
    }

    private void ClickSave(object sender, RoutedEventArgs e)
    {
        Print("\nSave ...\n");
    }
    */
}
