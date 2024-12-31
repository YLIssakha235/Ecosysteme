using Avalonia;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ECOSYS.ViewModels;

public abstract partial class FormeDeVie : ObservableObject
{
    [ObservableProperty]
    private Point position;

    [ObservableProperty]
    private int pointsDeVie;

    [ObservableProperty]
    private int reserveEnergie;

    [ObservableProperty]
    private Bitmap? imageSource;

    // Ajout de l'attribut Sexe
    [ObservableProperty]
    private string sexe; // "M" pour mâle, "F" pour femelle

    protected FormeDeVie(Point location, Bitmap? image = null, string sexe = "M")
    {
        Position = location;
        PointsDeVie = 200;
        ReserveEnergie = 150;
        ImageSource = image;
        Sexe = sexe; // Définit le sexe par défaut
    }

    public bool EstVivant => PointsDeVie > 0;

    public virtual void ConsommerEnergie()
    {
        if (ReserveEnergie > 0)
        {
            ReserveEnergie--;
        }
        else
        {
            ReserveEnergie = 0; 
            PointsDeVie--;
           
            if (PointsDeVie <= 0)
            {
                PointsDeVie = 0; 
                Mourir();
            }
        }
    }

    public abstract void Mourir();
}
