using System;
using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ECOSYS.ViewModels;

public partial class Carnivore : Animal
{
    public Carnivore(Point location, string sexe = "M") : base(location, null, sexe)
    {
        ImageSource = new Bitmap(AssetLoader.Open(new Uri("avares://ECOSYS/Assets/Carnivore.png")));
    }

    public void Chasser(Animal proie)
    {
        if (proie.EstVivant && SawOpponent(proie))
        {
            var direction = new Point(proie.Position.X - Position.X, proie.Position.Y - Position.Y);
            var magnitude = Math.Sqrt(direction.X * direction.X + direction.Y * direction.Y);

            if (magnitude > 0)
            {
                Velocity = new Point(direction.X / magnitude * Speed, direction.Y / magnitude * Speed);
                Deplacer();

                if (magnitude < 5) // Distance de contact
                {
                    Console.WriteLine(" Tu es mort ");
                    proie.PointsDeVie = 0;
                    proie.ReserveEnergie = 0;
                    proie.Mourir();
                    ReserveEnergie += 50; // Gain d'énergie après avoir mangé la proie
                }
            }
        }
    }

    public override void Mourir()
    {
        ReserveEnergie = 0;
        PointsDeVie = 0;
        ImageSource = new Bitmap(AssetLoader.Open(new Uri("avares://ECOSYS/Assets/Viande.png")));
    }

    protected override Animal CreerDescendant(Point position)
    {
        var random = new Random();
        string sexeDescendant = random.Next(0, 2) == 0 ? "M" : "F";
        return new Carnivore(position, sexeDescendant);
    }
}
