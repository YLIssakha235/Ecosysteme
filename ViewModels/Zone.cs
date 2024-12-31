using System;
using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ECOSYS.ViewModels;

public partial class Zone : ObservableObject
{
    [ObservableProperty]
    private Point centre;

    [ObservableProperty]
    private int rayon;

    public Zone(Point centre, int rayon)
    {
        this.Centre = centre;
        this.Rayon = rayon;
    }

    public bool Contient(FormeDeVie formeDeVie)
    {
        var distance = Math.Sqrt(Math.Pow(Centre.X - formeDeVie.Position.X, 2) + Math.Pow(Centre.Y - formeDeVie.Position.Y, 2));
        return distance <= Rayon;
    }

    public void InteragirAvec(FormeDeVie formeDeVie)
    {
        
    }
}