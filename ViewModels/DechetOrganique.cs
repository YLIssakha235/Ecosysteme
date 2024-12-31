using System;
using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ECOSYS.ViewModels;

public partial class DechetOrganique : FormeDeVie
{
    [ObservableProperty]
    private int tempsDeDecomposition; 

    [ObservableProperty]
    private Bitmap currentImage; 

    public bool EstDecompose => TempsDeDecomposition <= 0; 
    // Constructeur
    public DechetOrganique(Point initialPosition, int tempsDeDecomposition) : base(initialPosition)
    {
        this.TempsDeDecomposition = tempsDeDecomposition;
        CurrentImage = new Bitmap(AssetLoader.Open(new Uri("avares://ECOSYS/Assets/Déchet.png")));
        PointsDeVie = 0; // Les déchets organiques n'ont pas de points de vie
        ReserveEnergie = 0; // Pas d'énergie restante
    }

    // Méthode pour réduire le temps de décomposition
    public void SeDecomposer()
    {
        if (!EstDecompose)
        {
            TempsDeDecomposition--;

    

            // Changer l'image lorsque complètement décomposé
            if (EstDecompose)
            {
                CurrentImage = new Bitmap(AssetLoader.Open(new Uri("avares://ECOSYS/Assets/Déchet.png"))); // Exemple d'image pour compost
               
            }
        }
    }

    // Méthode appelée à la création d'un déchet organique
    public static DechetOrganique GenererDechet(FormeDeVie formeDeVie)
    {
       
        return new DechetOrganique(formeDeVie.Position, 50); // Temps de décomposition par défaut : 50
    }

    // Méthode override (inutile pour un déchet, mais incluse pour respecter FormeDeVie)
    public override void Mourir()
    {
        ReserveEnergie = 0;
        PointsDeVie = 0;
        ImageSource = new Bitmap(AssetLoader.Open(new Uri("avares://ECOSYS/Assets/Déchet.png")));
       
    }
}
