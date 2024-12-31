using System;
using Avalonia;

namespace ECOSYS.ViewModels;


public partial class ZoneDeVision : Zone
{
    public ZoneDeVision(Point centre, int rayon) : base(centre, rayon) { }

    public void DetecterDansZone(FormeDeVie formeDeVie)
    {
        if (Contient(formeDeVie))
        {
            
        }
    }
}
