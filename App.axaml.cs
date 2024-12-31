
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using ECOSYS.ViewModels;
using ECOSYS.Views;

namespace ECOSYS;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var simulation = new SimulationViewModel();
            desktop.MainWindow = new MainWindow
            {
                DataContext = simulation,
            };

            // Démarrer la simulation
            simulation.DemarrerSimulation();
        }

        base.OnFrameworkInitializationCompleted();
    }
}
