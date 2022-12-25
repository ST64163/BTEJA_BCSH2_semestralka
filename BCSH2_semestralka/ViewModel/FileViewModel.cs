
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IdeSK.View;
using System;

namespace IdeSK.ViewModel;

public class FileViewModel : ObservableObject
{
    public FileWindow? Current { get; set; }
    public bool LoadMode { get; set; }

    public IRelayCommand<string> ExecuteCommand { get; }

    public FileViewModel()
    {
        LoadMode = true;
        ExecuteCommand = new RelayCommand<string>(DoExecute);
    }

    private void DoExecute(string fileName)
    { 
        bool success = LoadMode ? TryLoadFile(fileName) : TrySaveFile(fileName);
        if (Current == null)
            throw new NullReferenceException();
        // TODO: exit the window
    }

    private bool TrySaveFile(string fileName) 
    {
        // TODO: save file
        return true;
    }

    private bool TryLoadFile(string fileName) 
    {
        // TODO: load file
        return true;
    }
}
