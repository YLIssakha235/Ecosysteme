using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace ECOSYS.ViewModels;

public partial class Viande : FormeDeVie
{
    [ObservableProperty]
    private int dureeDeComposition;

    public bool EstDecompose => DureeDeComposition <= 0;

    public  Viande(Point location, int dureeDeComposition = 10) : base(location, new Bitmap("avares://ECOSYS/Assets/Viande.png"))
    {
        this.DureeDeComposition = dureeDeComposition;
    }

    public void SeDecomposer()
    {
        if (DureeDeComposition > 0)
        {
            DureeDeComposition--;
            
        }

        if (EstDecompose)
        {
            Mourir();
        }
    }

    public DechetOrganique GenererDechetOrganique()
    {
        return new DechetOrganique(Position, 30); 
    }

    public override void Mourir()
    {
        PointsDeVie = 0;
        ReserveEnergie =0;
        ImageSource = new Bitmap(AssetLoader.Open(new Uri("avares://ECOSYS/Assets/Déchet.png")));

    }
}
