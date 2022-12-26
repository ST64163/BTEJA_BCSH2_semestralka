﻿
using System;
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
        SetInputState(false);
        textBox_console.TextChanged += (sender, args) => textBox_console.ScrollToEnd();
    }

    public void SetInputState(bool isEnabled)
    {
        textBox_consoleInput.IsEnabled = isEnabled;
        button_insertInput.IsEnabled = isEnabled;
    }

    public void SetInterpretState(bool isEnabled)
    { 
        button_build.IsEnabled = isEnabled;
        button_interpret.IsEnabled = isEnabled;
    }
}
