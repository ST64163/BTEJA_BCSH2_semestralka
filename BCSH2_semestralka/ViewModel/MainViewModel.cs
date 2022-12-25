
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IdeSK.View;
using InterpreterSK;

namespace IdeSK.ViewModel;

public class MainViewModel : ObservableObject
{

    private readonly Interpreter _interpreter;
    private string _output;
    private string _fileName;

    public string ConsoleOutput
    {
        get => _output;
        set
        {
            _output = value;
            OnPropertyChanged();
        }
    }

    public IRelayCommand<string> BuildCommand { get; }
    public IRelayCommand<string> InterpretCommand { get; }
    public IRelayCommand LoadCommand { get; }
    public IRelayCommand SaveCommand { get; }

    public MainViewModel()
    {
        _fileName = string.Empty;
        _output = string.Empty;

        _interpreter = new();
        _interpreter.WriteEvent += WriteCallback;
        _interpreter.ReadLineEvent += ReadCallback;

        BuildCommand = new RelayCommand<string>(DoBuild);
        InterpretCommand = new RelayCommand<string>(DoInterpret);
        LoadCommand = new RelayCommand(DoLoad);
        SaveCommand = new RelayCommand(DoSave);
    }

    private void WriteCallback(object sender, string message) => ConsoleOutput += message;
    private string ReadCallback(object sender)
    {
        InputWindow inputWindow = new();
        inputWindow.ShowDialog();
        return "TODO";
    }

    private void DoBuild(string code)
    {
        _interpreter.Build(code);
        ConsoleOutput += "\n";
    }

    private void DoInterpret(string code) 
    {
        _interpreter.Interpret(code);
        ConsoleOutput += "\n";
    }

    private void DoLoad()
    {
        new FileWindow(true).Show();
    }

    private void DoSave()
    {
        new FileWindow(false).Show();
    }
}
