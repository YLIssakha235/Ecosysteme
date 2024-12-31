using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECOSYS.ViewModels;

public partial class Plante : FormeDeVie
{
    private const int EnergiePourPropagation = 30; // Énergie nécessaire pour se propager
    private const int LimitePropagation = 10; // Maximum 10 plantes

    public int CompteurPropagation { get; private set; } = 0; // Compteur de propagations
    public ZoneDeSemis? ZoneDeSemis { get; set; }

    public Plante(Point location, ZoneDeSemis? zoneDeSemis = null) : base(location, null)
    {
        ZoneDeSemis = zoneDeSemis;

        var assetUri = new Uri("avares://ECOSYS/Assets/Plante.png");
        ImageSource = new Bitmap(AssetLoader.Open(assetUri));
    }

    public void SePropager(List<FormeDeVie> entites, int width, int height)
    {
        // Vérifie s'il y a déjà 10 plantes dans la simulation
        if (entites.OfType<Plante>().Count() >= 10)
        {
            return; // Ne propage pas si le nombre de plantes atteint la limite
        }

        if (ReserveEnergie < EnergiePourPropagation || CompteurPropagation >= LimitePropagation)
        {
            return; // Pas assez d'énergie ou limite atteinte
        }

        var random = new Random();
        int essais = 200; // Nombre maximal d'essais pour trouver une position libre

        while (essais > 0)
        {
            var nouvellePosition = new Point(
                Math.Clamp(Position.X + random.Next(0, 251), 10, width - 1), // Génère une position aléatoire plus éloignée
                Math.Clamp(Position.Y + random.Next(0, 251), 0, height - 1)
            );

            if (ZoneDeSemis != null && ZoneDeSemis.Contient(this) && ZoneDeSemis.EstPositionDisponible(nouvellePosition))
            {
                var nouvellePlante = new Plante(nouvellePosition, ZoneDeSemis)
                {
                    ImageSource = new Bitmap(AssetLoader.Open(new Uri("avares://ECOSYS/Assets/Plante.png")))
                };

                entites.Add(nouvellePlante);
                CompteurPropagation++;
                ReserveEnergie -= EnergiePourPropagation; // Réduit l'énergie après propagation
                break; 
            }

            essais--; // Réduit le nombre d'essais restants
        }
    }

    public override void Mourir()
    {
        ReserveEnergie = 0;
        PointsDeVie = 0;
        ImageSource = new Bitmap(AssetLoader.Open(new Uri("avares://ECOSYS/Assets/Déchet.png")));
    }
}
