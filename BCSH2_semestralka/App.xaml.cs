using IdeSK.ViewModel;
using InterpreterSK;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace BTEJA_BCSH2_semestralka;

public partial class App : Application
{
    public new static App Current => (App)Application.Current;
    
    public IServiceProvider Services { get; }

    public MainViewModel MainMV => Services.GetService<MainViewModel>() 
        ?? throw new NullReferenceException();

    public App()
    {
        Services = ConfigureServices();
    }

    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        services.AddSingleton<Interpreter>();
        services.AddSingleton<MainViewModel>();

        return services.BuildServiceProvider();
    }

}
