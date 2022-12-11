using IdeSK.ViewModel;
using InterpreterSK;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace BTEJA_BCSH2_semestralka;

public partial class App
{
    public new static App Current => (App)Application.Current;
    
    public IServiceProvider Services { get; }

    public InterpreterViewModel InterpreterMV => Services.GetService<InterpreterViewModel>() 
        ?? throw new NullReferenceException();

    public App()
    {
        Services = ConfigureServices();
    }

    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        services.AddSingleton<Interpreter>();
        services.AddSingleton<InterpreterViewModel>();

        return services.BuildServiceProvider();
    }

}
