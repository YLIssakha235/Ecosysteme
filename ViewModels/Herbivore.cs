using System;
using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ECOSYS.ViewModels;

public partial class Herbivore : Animal
{
    public Herbivore(Point location, string sexe = "M") : base(location, null, sexe)
    {
        ImageSource = new Bitmap(AssetLoader.Open(new Uri("avares://ECOSYS/Assets/Herbivore.png")));
    }

    public void Manger(Plante plante)
    {
        if (plante.EstVivant && SawOpponent(plante))
        {
            plante.Mourir();
            ReserveEnergie += 30; // Gain d'énergie après consommation de la plante
        }
    }

    public override void Mourir()
    {
        PointsDeVie = 0;
        ReserveEnergie = 0;
        ImageSource = new Bitmap(AssetLoader.Open(new Uri("avares://ECOSYS/Assets/Déchet.png")));
    }

    protected override Animal CreerDescendant(Point position)
    {
        var random = new Random();
        string sexeDescendant = random.Next(0, 2) == 0 ? "M" : "F";
        return new Herbivore(position, sexeDescendant);
    }
}
