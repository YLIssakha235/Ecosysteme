using System;
using Avalonia;

namespace ECOSYS.ViewModels;

// gère l'absortion des plantes
public class ZoneDeRacine : Zone
{
    public ZoneDeRacine(Point centre, int rayon) : base(centre, rayon) { }

    public void AbsorberNutriments(Plante plante)
    {
        if (plante == null)
        {
            return;
        }

        if (Contient(plante))
        {
            // cette partie augmente l'energie de plante
            plante.ReserveEnergie += 1; 
        }
    }
}


