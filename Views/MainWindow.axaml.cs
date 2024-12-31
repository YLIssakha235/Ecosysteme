using Avalonia.Controls;
using ECOSYS.ViewModels;
using System;


namespace ECOSYS.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        try
        {
            // Initialiser le ViewModel
            var viewModel = new SimulationViewModel();
            DataContext = viewModel;

            // Démarrer la simulation
            viewModel.DemarrerSimulation();
        }
        catch (Exception ex)
        {
            // Capture et journalisation des erreurs
            Console.WriteLine($"Erreur lors de l'initialisation de MainWindow : {ex.Message}");
            throw; // Relancer l'exception pour une meilleure traçabilité
        }
    }

    protected override void OnClosed(EventArgs e)
    {
        base.OnClosed(e);

        try
        {
            // Arrêter la simulation en fermant la fenêtre
            if (DataContext is SimulationViewModel simulationViewModel)
            {
                simulationViewModel.ArreterSimulation();
            }
        }
        catch (Exception ex)
        {
            // Capture et journalisation des erreurs lors de l'arrêt
            Console.WriteLine($"Erreur lors de l'arrêt de la simulation : {ex.Message}");
        }
    }
}