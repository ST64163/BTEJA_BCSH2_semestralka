
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ICSharpCode.AvalonEdit.Document;
using IdeSK.View;
using InterpreterSK;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace IdeSK.ViewModel;

public class MainViewModel : ObservableObject
{
    private (Task, CancellationTokenSource)? currentInterpreterTask;
    private readonly Interpreter interpreter;
    private string? fileName;

    private string _output;
    private TextDocument _code;

    public string ConsoleOutput
    {
        get => _output;
        set
        {
            _output = value;
            OnPropertyChanged();
        }
    }

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

    public IRelayCommand<string> BuildCommand { get; }
    public IRelayCommand<string> InterpretCommand { get; }
    public IRelayCommand LoadCommand { get; }
    public IRelayCommand SaveCommand { get; }
    public IRelayCommand SaveAsCommand { get; }

    public MainViewModel()
    {
        _output = string.Empty;
        _code = new();

        fileName = null;
        currentInterpreterTask = null;

        interpreter = new();
        interpreter.WriteEvent += WriteCallback;
        interpreter.ReadLineEvent += ReadCallback;

        BuildCommand = new RelayCommand<string>(DoBuild);
        InterpretCommand = new RelayCommand<string>(DoInterpret);
        LoadCommand = new RelayCommand(DoLoad);
        SaveCommand = new RelayCommand(DoSave);
        SaveAsCommand = new RelayCommand(DoSaveAs);
    }

    private void WriteCallback(object sender, string message) => ConsoleOutput += message;
    private string ReadCallback(object sender)
    {
        InputWindow inputWindow = new();
        if (inputWindow.ShowDialog() == true)
        {
            string input = "inputWindow.input"; // TODO
            ConsoleOutput += input + "\n";
            return input;
        }
        ConsoleOutput += "\n";
        return "";
    }

    private void DoBuild(string code) => RunInterpreterTask(code, false);

    private void DoInterpret(string code) => RunInterpreterTask(code, true);

    private void RunInterpreterTask(string _, bool interpret)
    {
        string code = CodeText; // avalon editor
        ClearInterpreterTask();
        CancellationTokenSource cts = new();
        Task task = Task.Factory.StartNew(() =>
        {
            if (interpret)
                interpreter.Interpret(code);
            else
                interpreter.Build(code);
            ConsoleOutput += "\n";
        }, cts.Token);
        currentInterpreterTask = (task, cts);
    }

    private void ClearInterpreterTask()
    {
        if (currentInterpreterTask != null) 
        {
            (Task task, CancellationTokenSource cts) = currentInterpreterTask.Value;
            bool isCompleted = task.IsCompleted;
            cts.Cancel();
            task.Dispose();
            cts.Dispose();
            currentInterpreterTask = null;
            if (!isCompleted)
                ConsoleOutput += "Stopped!\n";
        }
    }

    private void DoLoad()
    {
        OpenFileDialog dialog = new() { FileName = fileName ?? "", DefaultExt = "txt" };
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
        SaveFileDialog dialog = new() { FileName = fileName ?? "", DefaultExt = "txt" };
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
                invalid = false;
            }
        }
        catch (System.Exception) { }
        if (invalid)
            MessageBox.Show("Cannot save code to the entered file", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
