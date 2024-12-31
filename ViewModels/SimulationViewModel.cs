using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using Avalonia.Platform;
using Avalonia.Media.Imaging;

namespace ECOSYS.ViewModels;

public partial class SimulationViewModel : ObservableObject
{
    public const int TicksPerSecond = 10;
    private readonly DispatcherTimer _timer;

    [ObservableProperty]
    private long tickActuel;

    // Utilisation d'ObservableCollection pour notifier l'UI
    public ObservableCollection<FormeDeVie> Entites { get; } = new();
    public ZoneDeSemis? ZoneSemis { get; private set; } // Zone de semis associée aux plantes

    public int Width { get; } = 800;  // Largeur de la simulation
    public int Height { get; } = 450; // Hauteur de la simulation

    public SimulationViewModel()
    {
        // Configuration du timer
        _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(1000 / TicksPerSecond) };
        _timer.Tick += (sender, args) => DoTick();

        InitialiserSimulation();
    }

    private void InitialiserSimulation()
    {
        // Crée une zone de semis
        ZoneSemis = new ZoneDeSemis(new Point(100, 100), 150); 

        // Ajout des entités initiales
        Entites.Add(new Plante(new Point(30, 30), ZoneSemis)); 
        Entites.Add(new Carnivore(new Point(100, Height  / 2), "M")); 
        Entites.Add(new Carnivore(new Point(Width / 2, Height / 4), "F")); 
        Entites.Add(new Herbivore(new Point(300, 300), "F")); 
        Entites.Add(new Herbivore(new Point(100, 100), "M")); 
    }

    private void DoTick()
    {
        // Vérifie si la simulation doit s'arrêter
        if (SimulationTerminee())
        {
            ArreterSimulation();
            return;
        }

        ExecuterCycle();
        TickActuel++;
    }

    public void DemarrerSimulation() => _timer.IsEnabled = true;

    public void ArreterSimulation() => _timer.IsEnabled = false;

    private bool SimulationTerminee()
    {
        // La simulation continue tant qu'il reste au moins une entité vivante ou transformable
        return !Entites.Any(e => e.EstVivant || e is Viande || e is DechetOrganique);
    }

    private void ExecuterCycle()
    {
        var aSupprimer = new List<FormeDeVie>();
        var aAjouter = new List<FormeDeVie>();

        foreach (var entite in Entites.ToList())
        {
            entite.ConsommerEnergie();

            // Gestion des entités mortes
            if (!entite.EstVivant)
            {
                entite.Mourir(); // Change d'état et l'image
                aSupprimer.Add(entite);

                if (entite is Animal animal)
                {
                    aAjouter.Add(new Viande(animal.Position, 50)); // Les animaux morts deviennent de la viande
                }
                else if (entite is Plante plante)
                {
                    aAjouter.Add(new DechetOrganique(plante.Position, 50)); // Les plantes mortes deviennent des déchets organiques
                }

                continue;
            }

            // Gestion des comportements spécifiques
            if (entite is Animal animalEntite)
            {
                animalEntite.Deplacer();

                // Gestion de la reproduction
                var partenaire = Entites
                    .OfType<Animal>()
                    .FirstOrDefault(a => a != animalEntite && animalEntite.SeReproduire(a) != null);

                if (partenaire != null)
                {
                    var descendant = animalEntite.SeReproduire(partenaire);
                    if (descendant != null)
                    {
                        aAjouter.Add(descendant);
                        
                    }
                }
            }
            else if (entite is Plante planteEntite)
            {
                // Limite le nombre total de plantes à 10
                if (Entites.OfType<Plante>().Count() < 10)
                {
                    planteEntite.SePropager(aAjouter, Width, Height); // Ajout de nouvelles plantes
                }
            }
            else if (entite is Viande viande)
            {
                viande.SeDecomposer();
                if (viande.EstDecompose)
                {
                    aSupprimer.Add(viande);
                    aAjouter.Add(viande.GenererDechetOrganique());
                }
            }
            else if (entite is DechetOrganique dechet)
            {
                dechet.SeDecomposer();
                if (dechet.EstDecompose)
                {
                    aSupprimer.Add(dechet);
                }
            }
        }

        // Suppression des entités mortes
        foreach (var entite in aSupprimer)
        {
            Entites.Remove(entite);
        }

        // Ajout des nouvelles entités
        foreach (var entite in aAjouter)
        {
            Entites.Add(entite);
        }
    }
}


