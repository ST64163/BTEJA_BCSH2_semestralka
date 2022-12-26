
using BTEJA_BCSH2_semestralka;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ICSharpCode.AvalonEdit.Document;
using IdeSK.View;
using InterpreterSK;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;
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

    private void WriteCallback(object sender, string message) 
        => App.Current.Dispatcher.Invoke(() =>
        {
            lock (ConsoleOutput)
                ConsoleOutput += message;
        });

    private string ReadCallback(object sender)
        => App.Current.Dispatcher.Invoke(GetInputFromDialog).Result;

    private async Task<string> GetInputFromDialog()
    {
        Task<string> task = Task<string>.Factory.StartNew(() => 
        {
            InputDialog inputDialog = new(); // { Owner = App.Current.MainWindow };
            return (inputDialog.ShowDialog() == true) ? inputDialog.Input : "";
        });
        string input = await task.ConfigureAwait(false);
        lock (ConsoleOutput)
            ConsoleOutput += input + "\n";
        return input;
    }

    private void DoBuild(string code) => RunInterpreterTask(false);

    private void DoInterpret(string code) => RunInterpreterTask(true);

    private void RunInterpreterTask(bool interpret)
    {
        ClearInterpreterTask();
        CancellationTokenSource cts = new();
        string code = CodeText;
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
}
