using BTEJA_BCSH2_semestralka;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ICSharpCode.AvalonEdit.Document;
using InterpreterSK;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;
using MessageBox = System.Windows.Forms.MessageBox;

namespace IdeSK.ViewModel;

public class MainViewModel : ObservableObject
{
    private Task? currentInterpreterTask;
    private readonly Interpreter interpreter;
    private string? fileName;

    private string _output;
    private TextDocument _code;
    private string _input;

    public string ConsoleOutput
    {
        get => _output;
        set
        {
            _output = value;
            OnPropertyChanged();
        }
    }

    public string ConsoleInput
    {
        get => _input;
        set
        {
            _input = value;
            OnPropertyChanged();
        }
    }

    private bool IsInsertClicked { get; set; } = false;

    public TextDocument Code
    {
        get => _code;
        set
        {
            _code = value;
            OnPropertyChanged();
        }
    }

    public string CodeText
    {
        get => _code.Text;
        set
        {
            _code.Text = value;
            OnPropertyChanged();
        }
    }

    public IRelayCommand BuildCommand { get; }
    public IRelayCommand InterpretCommand { get; }
    public IRelayCommand LoadCommand { get; }
    public IRelayCommand SaveCommand { get; }
    public IRelayCommand SaveAsCommand { get; }
    public IRelayCommand InsertCommand { get; }

    public MainViewModel()
    {
        _output = string.Empty;
        _input = string.Empty;
        _code = new();

        fileName = null;
        currentInterpreterTask = null;

        interpreter = new();
        interpreter.WriteEvent += WriteCallback;
        interpreter.ReadLineEvent += ReadCallback;

        BuildCommand = new RelayCommand(DoBuild);
        InterpretCommand = new RelayCommand(DoInterpret);
        LoadCommand = new RelayCommand(DoLoad);
        SaveCommand = new RelayCommand(DoSave);
        SaveAsCommand = new RelayCommand(DoSaveAs);
        InsertCommand = new RelayCommand(DoInsert);
    }

    // MODEL - INTERPRETER CALLBACKS

    private void WriteCallback(object sender, string message)
        => ConsoleOutput += message;

    private string ReadCallback(object sender)
    {
        App.Current.Dispatcher.Invoke(EnableInput);
        ConsoleInput = string.Empty;
        IsInsertClicked = false;
        while (!IsInsertClicked)
            Thread.Sleep(250);
        ConsoleOutput += ConsoleInput + "\n";
        App.Current.Dispatcher.Invoke(DisableInput);
        return ConsoleInput;
    }

    // VIEW - RELAY COMMANDS 

    private void DoInsert() => IsInsertClicked = true;

    private void DoBuild() => RunInterpreterTask(false);

    private void DoInterpret() => RunInterpreterTask(true);

    private void RunInterpreterTask(bool interpret)
    {
        string code = CodeText;
        Task? oldTask = currentInterpreterTask;
        if (oldTask != null && oldTask.Status != TaskStatus.Running)
            oldTask.Dispose();
        currentInterpreterTask = Task.Factory.StartNew(() =>
        {
            App.Current.Dispatcher.Invoke(DisableInterpreting);
            if (interpret)
                interpreter.Interpret(code);
            else
                interpreter.Build(code);
            ConsoleOutput += "\n";
            App.Current.Dispatcher.Invoke(EnableInterpreting);
        });
    }

    private void DoLoad()
    {
        OpenFileDialog dialog = new() { Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*" };
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            try
            {
                string fileName = dialog.FileName;
                string code = File.ReadAllText(fileName);
                CodeText = code;
                this.fileName = fileName;
            }
            catch (System.Exception)
            {
                MessageBox.Show("Cannot load/read the entered file", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private void DoSave()
    {
        if (fileName == null)
            DoSaveAs();
        else
            SaveCodeToFile(fileName);
    }

    private void DoSaveAs()
    {
        SaveFileDialog dialog = new() { Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*" };
        if (dialog.ShowDialog() == DialogResult.OK)
            SaveCodeToFile(dialog.FileName);
    }

    private void SaveCodeToFile(string filename)
    {
        bool invalid = true;
        try
        {
            if (filename != null)
            {
                File.WriteAllText(filename, CodeText);
                fileName = filename;
                invalid = false;
            }
        }
        catch (System.Exception) { }
        if (invalid)
            MessageBox.Show("Cannot save code to the entered file", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }


    // VIEW - WINDOW MANIPULATION

    private MainWindow GetMainWindow()
        => App.Current.Windows.OfType<MainWindow>().First();

    private void EnableInput()
        => GetMainWindow().SetInputState(true);

    private void DisableInput()
        => GetMainWindow().SetInputState(false);

    private void EnableInterpreting()
        => GetMainWindow().SetInterpretState(true);

    private void DisableInterpreting()
        => GetMainWindow().SetInterpretState(false);
}
