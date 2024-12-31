using System;
using Avalonia;


namespace ECOSYS.ViewModels;

public partial class ZoneDeContact : Zone
{
    public ZoneDeContact(Point centre, int rayon) : base(centre, rayon) { }

    public void DetecterContact(FormeDeVie formeDeVie)
    {
        if (Contient(formeDeVie)){}
    }
}

