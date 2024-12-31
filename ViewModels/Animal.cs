using System;
using Avalonia;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ECOSYS.ViewModels;

public abstract partial class Animal : FormeDeVie
{
    [ObservableProperty]
    private Point velocity;

    public double Vision { get; set; } = 100; // Distance de vision
    public double Speed { get; set; } = 1.0;  // Vitesse de déplacement

    protected Animal(Point location, Bitmap? image = null, string sexe = "M") : base(location, image, sexe)
    {
        Velocity = new Point(1.0, 0.0);
    }

    public virtual void Deplacer()
    {
        if (EstVivant)
        {
            Position = new Point(Position.X + Velocity.X, Position.Y + Velocity.Y);
            ConsommerEnergie();
        }
    }

    public bool SawOpponent(FormeDeVie autre)
    {
        var distance = Math.Sqrt(Math.Pow(Position.X - autre.Position.X, 2) + Math.Pow(Position.Y - autre.Position.Y, 2));
        return distance <= Vision;
    }

    // Méthode de reproduction
    public Animal? SeReproduire(Animal autre)
    {
        // Vérifie les conditions de reproduction
        if (autre == null || autre.Sexe == Sexe || !EstVivant || !autre.EstVivant || !SawOpponent(autre))
        {
            return null; // Les animaux doivent être de sexes opposés, vivants, et à portée
        }

        // Génère une position pour le descendant
        var random = new Random();
        var nouvellePosition = new Point(
            Math.Clamp(Position.X + random.Next(-10, 11), 0, 800), // Clamp pour éviter de sortir de la zone
            Math.Clamp(Position.Y + random.Next(-10, 11), 0, 450)
        );

        // Crée le descendant
        return CreerDescendant(nouvellePosition);
    }

    // Méthode abstraite pour créer un descendant spécifique à l'espèce
    protected abstract Animal CreerDescendant(Point position);
}
