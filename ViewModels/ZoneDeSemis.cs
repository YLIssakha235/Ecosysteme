using System.Collections.Generic;
using System;
using Avalonia;

namespace ECOSYS.ViewModels;

public partial class ZoneDeSemis : Zone
{
    public List<Point> PositionsOccupees { get; } = new();

    public ZoneDeSemis(Point centre, int rayon) : base(centre, rayon) { }

    public bool EstPositionDisponible(Point position)
    {
        foreach (var pos in PositionsOccupees)
        {
            if (Math.Abs(pos.X - position.X) < 5 && Math.Abs(pos.Y - position.Y) < 5)
            {
                return false; // Position trop proche d'une autre plante
            }
        }

        PositionsOccupees.Add(position);
        return true;
    }

    public void GererSemis(Plante plante, List<FormeDeVie> entites, Point nouvellePosition)
    {
        if (Contient(plante) && plante.EstVivant && EstPositionDisponible(nouvellePosition))
        {
            entites.Add(new Plante(nouvellePosition));
        }
    }

    public bool Contient(Plante plante)
    {
        var distance = Math.Sqrt(Math.Pow(Centre.X - plante.Position.X, 2) + Math.Pow(Centre.Y - plante.Position.Y, 2));
        return distance <= Rayon; // Vérifie que la plante est dans la zone de semis
    }
}
