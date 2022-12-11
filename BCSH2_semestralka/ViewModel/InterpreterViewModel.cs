
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InterpreterSK;

namespace IdeSK.ViewModel;

public class InterpreterViewModel : ObservableObject
{

    private readonly Interpreter interpreter;
    private string output;

    public string ConsoleOutput
    {
        get => output;
        set
        {
            output = value;
            OnPropertyChanged();
        }
    }

    public IRelayCommand<string> BuildCommand { get; }
    public IRelayCommand<string> InterpretCommand { get; }

    public InterpreterViewModel()
    {
        output = string.Empty;
        interpreter = new();
        interpreter.WriteEvent += WriteCallback;
        interpreter.ReadLineEvent += ReadCallback;
        BuildCommand = new RelayCommand<string>(DoBuild);
        InterpretCommand = new RelayCommand<string>(DoInterpret);
    }

    private void WriteCallback(object sender, string message) => ConsoleOutput += message;
    private string ReadCallback(object sender) => "TODO"; //TODO: binding for input

    private void DoBuild(string code)
    {
        interpreter.Build(code);
    }

    private void DoInterpret(string code) 
    {
        interpreter.Interpret(code);
    } 

}
