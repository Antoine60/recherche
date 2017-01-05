using System;
using System.Collections.Generic;

public class Container
{
    List<Bloc> m_blocs = new List<Bloc>();
    const int m_width = 50;
    const int m_depth = 50;

    public Container()
    {
    }

    public void AddBloc(Bloc bloc)
    {
        List<Point> potentialPoints = new List<Point>();
        if (m_blocs.Count == 0)
        {
            bloc.setPosition(0, 0, 0);
            m_blocs.Add(bloc);
            return;
        }
        foreach (Bloc bl in m_blocs)
        {
            //Si plaçable sur le coin en bas à droite
            if (isPlacable(bl.getBottomRightPoint(), bloc))
                potentialPoints.Add(bl.getBottomRightPoint());
            //si plaçable sur le coin en haut à droite
            if (isPlacable(bl.getUpperLeftPoint(), bloc))
                potentialPoints.Add(bl.getUpperLeftPoint());
            //si placable sur le point en bas, au fond à gauche
            if (isPlacable(bl.getBackLeftPoint(), bloc))
                potentialPoints.Add(bl.getBackLeftPoint());
        }
        m_blocs.Add(PositionHelper.Meilleurposition(potentialPoints, bloc, m_blocs));
    }

    public bool isPlacable(Point p, Bloc bloc)
    {
        foreach (Bloc b in m_blocs)
        {
            if (((((b.X + b.Largeur > p.X) && (b.X + b.Largeur < p.X + bloc.Largeur))
                || ((b.X >= p.X) && (b.X < p.X + bloc.Largeur))
                || ((b.X < p.X) && (b.X + b.Largeur >= p.X + bloc.Largeur)))
                && ((p.Y >= b.Y) && (p.Y < b.Y + b.Hauteur)
                    || ((p.Y + bloc.Hauteur > b.Y) && (p.Y + bloc.Hauteur <= b.Y + b.Hauteur))
                    || ((p.Y <= b.Y) && (p.Y + bloc.Hauteur >= b.Y + b.Hauteur)))
                    && ((p.Z >= b.Z) && (p.Z < b.Z + b.Profondeur)
                        || ((p.Z + bloc.Profondeur > b.Z) && (p.Z + bloc.Profondeur <= b.Z + b.Profondeur))
                        || ((p.Z <= b.Z) && (p.Z + bloc.Profondeur >= b.Z + b.Profondeur))))
                || (p.X + bloc.Largeur > m_width)
                || (p.Z + bloc.Profondeur > m_depth))
            {
                return false;
            }
        }
        return true;
    }

    public int Width
    {
        get { return m_width; }
    }

    public int depth
    {
        get { return m_depth; }
    }

    public List<Bloc> Blocs
    {
        get { return m_blocs; }
        set { m_blocs = value; }
    }
}


